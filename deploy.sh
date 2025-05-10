#!/bin/bash

# Verificar status do Minikube
echo "Verificando status do Minikube..."
minikube status

# Configurar o ambiente Docker para o Minikube
echo "Configurando ambiente Docker para o Minikube..."
eval $(minikube docker-env)

# Construir a imagem Docker diretamente no ambiente do Minikube
echo "Construindo a imagem Docker no ambiente do Minikube..."
docker build -t ms-consumers:latest -f docker/Dockerfile .

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

# Aplicar as configurações do Kubernetes usando kustomize
echo "Aplicando configurações do Kubernetes..."
kubectl apply -k k8s/overlays/dev

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

# Mostrar o status do deployment
kubectl get deployment ms-consumers
kubectl get pods -l app=ms-consumers 