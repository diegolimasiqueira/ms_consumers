#!/bin/bash

# Criar pasta kubernetes se não existir
echo "Criando pasta kubernetes..."
mkdir -p kubernetes

# Verificar status do Minikube
echo "Verificando status do Minikube..."
minikube status

# Configurar o ambiente Docker para o Minikube
echo "Configurando ambiente Docker para o Minikube..."
eval $(minikube docker-env)

# Construir a imagem Docker diretamente no ambiente do Minikube
echo "Construindo a imagem Docker no ambiente do Minikube..."
docker build -t ms-consumers:latest .

# Verificar se a imagem foi carregada
echo "Verificando se a imagem foi carregada..."
docker images | grep ms-consumers

# Limpar recursos existentes
echo "Limpando recursos existentes..."
kubectl delete deployment ms-consumers --ignore-not-found=true
kubectl delete pod -l app=ms-consumers --ignore-not-found=true
kubectl delete service ms-consumers-service --ignore-not-found=true
kubectl delete ingress ms-consumers-ingress --ignore-not-found=true

# Aguardar a limpeza
echo "Aguardando limpeza dos recursos..."
sleep 5

# Criar arquivos do Kubernetes
echo "Criando arquivos do Kubernetes..."

# Deployment
cat > kubernetes/ms-consumers-deployment.yaml << EOF
apiVersion: apps/v1
kind: Deployment
metadata:
  name: ms-consumers
  namespace: default
spec:
  replicas: 1
  selector:
    matchLabels:
      app: ms-consumers
  template:
    metadata:
      labels:
        app: ms-consumers
    spec:
      containers:
      - name: ms-consumers
        image: ms-consumers:latest
        imagePullPolicy: Never
        ports:
        - containerPort: 80
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
        - name: ASPNETCORE_URLS
          value: "http://+:80"
        resources:
          limits:
            cpu: "500m"
            memory: "512Mi"
          requests:
            cpu: "250m"
            memory: "256Mi"
        readinessProbe:
          httpGet:
            path: /health
            port: 80
          initialDelaySeconds: 5
          periodSeconds: 10
        livenessProbe:
          httpGet:
            path: /health
            port: 80
          initialDelaySeconds: 15
          periodSeconds: 20
EOF

# Service
cat > kubernetes/ms-consumers-service.yaml << EOF
apiVersion: v1
kind: Service
metadata:
  name: ms-consumers-service
  namespace: default
spec:
  selector:
    app: ms-consumers
  ports:
    - protocol: TCP
      port: 80
      targetPort: 80
  type: ClusterIP
EOF

# Ingress
cat > kubernetes/ms-consumers-ingress.yaml << EOF
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: ms-consumers-ingress
  namespace: default
  annotations:
    kubernetes.io/ingress.class: kong
    konghq.com/preserve-host: "true"
    konghq.com/strip-path: "false"
    konghq.com/protocols: "http,https"
    konghq.com/plugins: "cors-plugin,request-transformer-plugin"
    konghq.com/override: "true"
spec:
  rules:
  - host: ms-consumers.local
    http:
      paths:
      - path: /
        pathType: Prefix
        backend:
          service:
            name: ms-consumers-service
            port:
              number: 80
EOF

# CORS Plugin
cat > kubernetes/cors-plugin.yaml << EOF
apiVersion: configuration.konghq.com/v1
kind: KongPlugin
metadata:
  name: cors-plugin
  namespace: default
plugin: cors
config:
  origins: ["*"]
  methods: ["GET", "POST", "PUT", "DELETE", "OPTIONS"]
  headers: ["Accept", "Accept-Version", "Content-Length", "Content-MD5", "Content-Type", "Date", "Authorization", "X-Requested-With"]
  credentials: true
  max_age: 3600
EOF

# Request Transformer Plugin
cat > kubernetes/request-transformer-plugin.yaml << EOF
apiVersion: configuration.konghq.com/v1
kind: KongPlugin
metadata:
  name: request-transformer-plugin
  namespace: default
plugin: request-transformer
config:
  add:
    headers:
    - "X-Forwarded-Proto: http"
    - "X-Forwarded-Host: ms-consumers.local"
    - "X-Forwarded-Port: 32167"
    - "Host: ms-consumers.local"
  replace:
    headers:
    - "Host: ms-consumers.local"
EOF

# Aplicar as configurações do Kubernetes
echo "Aplicando configurações do Kubernetes..."
kubectl apply -f kubernetes/ms-consumers-deployment.yaml
kubectl apply -f kubernetes/ms-consumers-service.yaml
kubectl apply -f kubernetes/ms-consumers-ingress.yaml
kubectl apply -f kubernetes/cors-plugin.yaml
kubectl apply -f kubernetes/request-transformer-plugin.yaml

# Aguardar o deployment
echo "Aguardando o deployment..."
kubectl rollout status deployment/ms-consumers

# Verificar status do pod
echo "Verificando status do pod..."
kubectl get pods -l app=ms-consumers

# Mostrar os endpoints
echo "Endpoints disponíveis:"
minikube service list 