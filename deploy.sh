#!/bin/bash

# Construir a imagem Docker
echo "Construindo a imagem Docker..."
docker build -t ms-consumers:latest .

# Carregar a imagem no Minikube
echo "Carregando a imagem no Minikube..."
minikube image load ms-consumers:latest

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

# Mostrar os endpoints
echo "Endpoints disponíveis:"
minikube service list 