# Desafio Técnico Desenvolvedor Júnior LuizaLabs

> No diretório ~/docs/ você encontra o arquivo pdf com o enunciado do problema proposto.

## 💻 Pré-requisitos

### Ferramentas para rodar o projeto
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
### Ferramentas de Dev
- [Git](https://git-scm.com/)
- [DotNet sdk](https://dotnet.microsoft.com/download)

### Arquivo .env
- Certifique-se de que existe um arquivo .env na raiz do projeto, contendo as seguinte variáveis:

```BASH
POSTGRES_DB=favorite-products-db
POSTGRES_USER=favorite-products-admin
POSTGRES_PASSWORD=favorite-products-admin-strong-password
POSTGRES_PORT=5432

ASPNETCORE_ENVIRONMENT=Development
API_PORT_HTTPS=5001
API_PORT_HTTP=5000
```

## 🚀 Rodando o projeto

Para rodar a web-api e testar via HTTP request basta rodar:

```bash
    #Navegue até a pasta do projeto
    cd ~/Download/FavoriteProducts
    
    #Suba o banco de dados e a web api no Docker
    docker-compose up -d
```
Quando os containers subirem acesse a URL http://localhost:${API_PORT_HTTP} ou https://localhost:${API_PORT_HTTPS}