# Payment Processing Service

Este projeto implementa um sistema de processamento de pagamentos com base em microsserviços, utilizando tecnologias modernas como **.NET 6**, **MongoDB** e **Azure Service Bus**. O sistema é projetado para registrar, consultar e processar `PaymentIntent` com validações robustas e suporte para mensageria assíncrona.

---

## Arquitetura

O projeto é composto por dois serviços principais:

### **1. Presentation**
- **Responsabilidade:** Fornece uma API HTTP para registro e consulta de pagamentos.
- **Endpoints principais:**
  - `POST /api/payment-intent`: Registra um novo `PaymentIntent`.
  - `GET /api/payment-intent/{id}`: Consulta o status de um `PaymentIntent`.

### **2. Messaging**
- **Responsabilidade:** Consome mensagens da fila no **Azure Service Bus** e processa eventos relacionados a pagamentos.
- **Recursos principais:**
  - Reentrega automática de mensagens em caso de falhas.
  - Processamento assíncrono para alta eficiência.

---

## Tecnologias Utilizadas

- **.NET 6**: Framework principal para desenvolvimento dos serviços.
- **MongoDB**: Banco de dados NoSQL utilizado para armazenar os dados das `PaymentIntent`.
- **Azure Service Bus**: Utilizado para mensageria assíncrona.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **Swagger**: Documentação interativa para a API.
- **Docker**: Empacotamento e execução dos serviços.

---

## Pré-requisitos

Antes de iniciar, certifique-se de ter os seguintes itens instalados:

- **.NET 6 SDK**
- **Docker e Docker Compose** (para execução com contêineres)
- **MongoDB** (local ou hospedado)
- Configuração válida do **Azure Service Bus**

---

## Configuração

### Variáveis de Ambiente

Configure as seguintes variáveis de ambiente para os serviços:

| Variável                          | Descrição                                  |
|-----------------------------------|--------------------------------------------|
| `CosmosDbSettings:ConnectionString` | String de conexão para o MongoDB.          |
| `CosmosDbSettings:DatabaseName`     | Nome do banco de dados no MongoDB.         |
| `CosmosDbSettings:CollectionName`   | Nome da coleção no MongoDB.                |
| `ServiceBusSettings:ConnectionString` | String de conexão para o Azure Service Bus. |
| `ServiceBusSettings:QueueName`      | Nome da fila no Azure Service Bus.         |

### Configuração do `appsettings.json`

No ambiente de desenvolvimento, você pode configurar diretamente no arquivo `appsettings.json`:

```json
{
  "CosmosDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "PaymentDb",
    "CollectionName": "PaymentIntents"
  },
  "ServiceBusSettings": {
    "ConnectionString": "your-service-bus-connection-string",
    "QueueName": "payment-intent-queue"
  }
}
```

---

## Executando o Projeto

### Localmente

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/NB3zerra/tech_challenge_bsf.git
   ```

2. **Restaure as dependências**:
   ```bash
   dotnet restore
   ```

3. **Execute os serviços**:
   - Serviço de apresentação:
     ```bash
     cd src/PS.Presentation
     dotnet run
     ```
   - Serviço de mensageria:
     ```bash
     cd src/PS.Messaging
     dotnet run
     ```

4. **Acesse a API**:
   - [Swagger UI](http://localhost:5000/swagger)


## Testes

### Executando Testes Unitários

1. **Navegue até o projeto de testes**:
   ```bash
   cd src/PS.Tests
   ```

2. **Execute os testes**:
   ```bash
   dotnet test
   ```

---

## Contribuição

Contribuições são bem-vindas! Siga estas etapas para contribuir:

1. **Fork este repositório**.
2. **Crie um branch para sua feature ou correção**:
   ```bash
   git checkout -b minha-feature
   ```
3. **Faça o commit de suas mudanças**:
   ```bash
   git commit -m "Adicionei minha feature"
   ```
4. **Faça o push para o branch**:
   ```bash
   git push origin minha-feature
   ```
5. **Abra um Pull Request**.

---

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo `LICENSE` para mais detalhes.

---

Se precisar de mais ajustes ou quiser algo mais específico no README, é só avisar!