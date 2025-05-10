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
kubectl delete kongplugin cors-plugin --ignore-not-found=true
kubectl delete kongplugin request-transformer-plugin --ignore-not-found=true

# Aguardar a limpeza
echo "Aguardando limpeza dos recursos..."
sleep 5

# Aplicar as configurações do Kubernetes na ordem correta
echo "Aplicando configurações do Kubernetes..."

# 1. Primeiro os plugins do Kong
echo "Aplicando plugins do Kong..."
kubectl apply -f kubernetes/kong-plugins.yaml

# 2. Depois o deployment
echo "Aplicando deployment..."
kubectl apply -f kubernetes/ms-consumers-deployment.yaml

# 3. Em seguida o service
echo "Aplicando service..."
kubectl apply -f kubernetes/ms-consumers-service.yaml

# 4. Por último o ingress
echo "Aplicando ingress..."
kubectl apply -f kubernetes/ms-consumers-ingress.yaml

# Aguardar o deployment
echo "Aguardando o deployment..."
kubectl rollout status deployment/ms-consumers

# Verificar status do pod
echo "Verificando status do pod..."
kubectl get pods -l app=ms-consumers

# Mostrar os endpoints
echo "Endpoints disponíveis:"
minikube service list

# Aguardar o pod estar pronto
echo "Aguardando o pod estar pronto..."
kubectl wait --for=condition=ready pod -l app=ms-consumers --timeout=300s

# Mostrar status final
echo "Status final do deployment:"
kubectl get deployment ms-consumers
echo "Status final do pod:"
kubectl get pods -l app=ms-consumers 