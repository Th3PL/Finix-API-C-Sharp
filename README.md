# 🎮 Finix API
[![OpenAPI](https://img.shields.io/badge/OpenAPI-Swagger-blue)](http://localhost:8080/swagger-ui.html)

A FINIX-API é uma aplicação backend desenvolvida em .NET para gerenciamento de jogadores e ranking de pontuações. O sistema permite cadastrar jogadores, atualizar suas informações e gerar um ranking automático baseado nos pontos acumulados.
## ✅ Funcionalidades

| Recurso        | Endpoint        | Método | Descrição                                                                 |
|----------------|-----------------|--------|---------------------------------------------------------------------------|
| Criar jogador  | `finix/jogador` | POST   | Registra um novo jogador com nome, email, telefone e endereço            |
| Listar jogadores | `finix/jogador`      | GET    | Retorna todos os jogadores cadastrados                                   |
| Buscar jogador por ID | `finix/jogador/{id}` | GET | Retorna os dados de um jogador específico                                |
| Atualizar jogador | `finix/jogador/{id}` | PUT    | Atualiza nome e telefone de um jogador                                   |
| Deletar jogador  | `finix/jogador/{id}` | DELETE | Remove um jogador do sistema                                             |
| Ranking         | `finix/ranking`      | GET    | Retorna uma lista ranqueada dos jogadores por pontuação                  |


## 🧱 Arquitetura

A aplicação segue os princípios da arquitetura REST, com separação clara entre camadas de domínio, serviço e controlador.
### 📌 Diagrama
![Diagrama](/img/diagrama.png)
### 🔁 Diagrama de sequência
![Diagrama-sequencia](/img/diagrama-sequencia.png)

## ⚙️ Demonstração no Postman

### Post ➕
![POST](/img/post.png)

### GET 👥
![GET](/img/get.png)

### GET/{id} 👤
![GET2](/img/get2.png)

### PUT ✏️
![PUT](/img/put.png)

### DELETE 🗑️
![DELETE](/img/delete.png)

### GET Ranking 🏆
![Ranking](/img/ranking.png)

## 👥 Créditos

| Nome                                | RM       |
|-------------------------------------|----------|
| ⚡ João Pedro Borsato Cruz           | RM550294 |
| 💫 Maria Fernanda Vieira de Camargo | RM97956  |
| 🚀 Pedro Lucas de Andrade Nunes     | RM550366 |
| 🌟 Sofia Amorim Coutinho            | RM552534 |