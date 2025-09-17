# 🎮 Finix API
[![OpenAPI](https://img.shields.io/badge/OpenAPI-Swagger-blue)](http://localhost:8080/swagger-ui.html)

A FINIX-API é uma aplicação backend desenvolvida em .NET para gerenciamento de jogadores e ranking de pontuações. O sistema permite cadastrar jogadores, atualizar suas informações e gerar um ranking automático baseado nos pontos acumulados.
## ✅ Funcionalidades

| Recurso        | Endpoint            | Método | Descrição                                                                 |
|----------------|---------------------|--------|---------------------------------------------------------------------------|
| Criar jogador  | `/jogador`          | POST   | Registra um novo jogador com nome, email, telefone e endereço            |
| Listar jogadores | `/jogador`        | GET    | Retorna todos os jogadores cadastrados                                   |
| Buscar jogador por ID | `/jogador/{id}` | GET | Retorna os dados de um jogador específico                                |
| Atualizar jogador | `/jogador/{id}`   | PUT    | Atualiza nome e telefone de um jogador                                   |
| Deletar jogador  | `/jogador/{id}`    | DELETE | Remove um jogador do sistema                                             |
| Ranking         | `/ranking`         | GET    | Retorna uma lista ranqueada dos jogadores por pontuação                  |


## 🧱 Arquitetura

A aplicação segue os princípios da arquitetura REST, com separação clara entre camadas de domínio, serviço e controlador.
### 📌 Diagrama
![Diagrama](/img/diagrama.png)


