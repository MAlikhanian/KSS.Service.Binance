# KSS.Service.Binance

A .NET-based RESTful API service for managing cryptocurrency futures orders on Binance exchange. Built with Clean Architecture principles, this service provides an exchange-agnostic design that can be easily extended to support other cryptocurrency exchanges.

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns across four main layers:

- **API Layer** - Controllers, HTTP endpoints, and API configuration
- **Application Layer** - Business logic, CQRS commands/queries, DTOs, and validation
- **Domain Layer** - Core business entities, enums, and repository interfaces
- **Infrastructure Layer** - External services (Binance.Net), mappings, and data persistence

### Architecture Flow

```
Binance API → Infrastructure Mapper → Domain Entity → Application Logic → DTO Mapper → API Response
```

## ✨ Features

- **Futures Order Management**
  - Create market and limit orders (buy/sell)
  - Get order details by ID or client order ID
  - Get all orders for a symbol
  - Cancel single or multiple orders
  - Cancel all open orders for a symbol
  - Modify existing orders
  - Batch operations support

- **Clean Architecture**
  - Domain-driven design with rich domain models
  - CQRS pattern using MediatR
  - Exchange-agnostic design for easy extension
  - Dependency injection throughout

- **API Documentation**
  - Swagger/OpenAPI integration
  - Interactive API documentation available at root URL when running the application
  - All current endpoints are documented in Swagger UI

## 🚀 Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- Binance API credentials (API Key and Secret)
- (Optional) Docker for containerized deployment

## 📦 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/KSS.Service.Binance.git
   cd KSS.Service.Binance
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the application**
   - Copy `KSS.Service.API/appsettings.json.example` to `KSS.Service.API/appsettings.json`
   - Copy `KSS.Service.API/appsettings.Development.json.example` to `KSS.Service.API/appsettings.Development.json`
   - Update the configuration files with your Binance API credentials

## ⚙️ Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Server": {
    "HttpPorts": "8080",
    "HttpsPorts": "8081"
  },
  "Exchange": {
    "ApiKey": "YOUR_API_KEY_HERE",
    "ApiSecret": "YOUR_API_SECRET_HERE"
  }
}
```

### Environment Variables (Alternative)

You can also use environment variables or User Secrets for sensitive data:

```bash
# Using User Secrets (recommended for development)
dotnet user-secrets set "Exchange:ApiKey" "your-api-key"
dotnet user-secrets set "Exchange:ApiSecret" "your-api-secret"
```

## 🏃 Running the Application

### Development Mode

```bash
cd KSS.Service.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:8080`
- HTTPS: `https://localhost:8081`
- Swagger UI: `http://localhost:8080` or `https://localhost:8081` (for current API documentation)

### Production Mode

```bash
dotnet publish -c Release
cd KSS.Service.API/bin/Release/net10.0/publish
dotnet KSS.Service.API.dll
```

## 🐳 Docker

### Build Docker Image

```bash
docker build -t kss-service-binance -f KSS.Service.API/Dockerfile .
```

### Run Container

```bash
docker run -p 8080:8080 -p 8081:8081 \
  -e Exchange__ApiKey="your-api-key" \
  -e Exchange__ApiSecret="your-api-secret" \
  kss-service-binance
```

## 📁 Project Structure

```
KSS.Service.Binance/
├── KSS.Service.API/              # API Layer
│   ├── Controllers/              # API Controllers
│   ├── Program.cs                # Application entry point
│   └── appsettings.json          # Configuration
│
├── KSS.Service.Application/      # Application Layer
│   ├── Features/                 # CQRS Features
│   │   └── FuturesOrder/
│   │       ├── Commands/         # Command handlers
│   │       └── Queries/          # Query handlers
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Interfaces/               # Service interfaces
│   └── Mappings/                  # Domain to DTO mappers
│
├── KSS.Service.Domain/           # Domain Layer
│   ├── Entities/                 # Domain entities
│   ├── Enums/                    # Domain enums
│   └── Interface/                # Repository interfaces
│
└── KSS.Service.Infrastructure/  # Infrastructure Layer
    ├── ExternalServices/         # Binance.Net integration
    └── Mappings/                  # External API to Domain mappers
```

## 🛠️ Technologies Used

- **.NET 10.0** - Framework
- **ASP.NET Core** - Web API framework
- **MediatR** - CQRS pattern implementation
- **FluentValidation** - Request validation
- **Binance.Net** - Binance API client library
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI documentation
- **Docker** - Containerization support

## 🧪 Development

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Code Structure

- **CQRS Pattern**: Commands and Queries are separated using MediatR
- **Clean Architecture**: Each layer has clear responsibilities
- **Domain-Driven Design**: Rich domain models with business logic
- **Dependency Injection**: All dependencies are injected via interfaces

### Adding a New Exchange

To add support for a new exchange:

1. Create a new mapper in `KSS.Service.Infrastructure/Mappings/` (e.g., `BybitToDomainMapper.cs`)
2. Implement `IFuturesOrderService` with the new exchange client
3. Register the new service in `KSS.Service.Infrastructure/DependencyInjection.cs`

The domain layer remains unchanged, ensuring exchange-agnostic business logic.

## 📝 Understanding the Codebase

### Layer Responsibilities

#### Domain Layer (`KSS.Service.Domain`)
- **Purpose**: Core business logic and entities
- **Contains**: 
  - Domain entities (e.g., `FuturesOrder`) with business rules
  - Domain enums (OrderStatus, OrderSide, OrderType, TimeInForce)
  - Repository interfaces (contracts only, no implementation)
- **Key Feature**: No dependencies on other layers - pure business logic

#### Application Layer (`KSS.Service.Application`)
- **Purpose**: Application-specific business logic and orchestration
- **Contains**:
  - CQRS Commands and Queries (using MediatR)
  - Command/Query Handlers
  - DTOs (Data Transfer Objects)
  - Service interfaces
  - Mappers (Domain → DTO)
  - FluentValidation validators
- **Key Feature**: Depends only on Domain layer

#### Infrastructure Layer (`KSS.Service.Infrastructure`)
- **Purpose**: External integrations and technical implementations
- **Contains**:
  - External service implementations (Binance.Net integration)
  - Mappers (External API → Domain)
  - Database contexts and repositories (if needed)
- **Key Feature**: Implements interfaces defined in Application/Domain layers

#### API Layer (`KSS.Service.API`)
- **Purpose**: HTTP endpoints and API configuration
- **Contains**:
  - Controllers
  - Program.cs (startup configuration)
  - Middleware
  - API-specific configurations
- **Key Feature**: Thin layer that delegates to Application layer via MediatR

### CQRS Pattern

The project uses **Command Query Responsibility Segregation (CQRS)** pattern:

- **Commands**: Operations that change state (Create, Update, Delete)
  - Located in: `KSS.Service.Application/Features/{Feature}/Commands/`
  - Structure: `{Command}.cs`, `{Command}Handler.cs`, `{Command}Validator.cs`, `{Command}Response.cs`

- **Queries**: Operations that read data (Get, GetAll)
  - Located in: `KSS.Service.Application/Features/{Feature}/Queries/`
  - Structure: `{Query}.cs`, `{Query}Handler.cs`, `{Query}Validator.cs`, `{Query}Response.cs`

### Adding a New Feature

1. **Create Domain Entity** (if needed)
   - Add entity in `KSS.Service.Domain/Entities/`
   - Add domain enums in `KSS.Service.Domain/Enums/`

2. **Create Application Feature**
   - Create folder: `KSS.Service.Application/Features/{FeatureName}/`
   - Add Commands/Queries with handlers and validators
   - Create DTOs in `KSS.Service.Application/DTOs/`
   - Create mapper in `KSS.Service.Application/Mappings/`

3. **Implement Infrastructure Service**
   - Implement service interface in `KSS.Service.Infrastructure/ExternalServices/`
   - Create mapper in `KSS.Service.Infrastructure/Mappings/`

4. **Create API Controller**
   - Add controller in `KSS.Service.API/Controllers/`
   - Use MediatR to send commands/queries

## 🔒 Security

- API keys and secrets should never be committed to version control
- Use User Secrets for local development
- Use environment variables or secure vaults in production
- The `appsettings.json` file is excluded from Git (see `.gitignore`)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## ⚠️ Disclaimer

This software is provided "as is" without warranty of any kind. Trading cryptocurrencies involves substantial risk. Use this software at your own risk. The authors are not responsible for any financial losses incurred from using this software.

## 📞 Support

For issues, questions, or contributions:
- **Email**: alikhanian.mohammad@gmail.com
- **GitHub Issues**: Open an issue on GitHub for bug reports and feature requests

---

**Note**: This project is designed to be exchange-agnostic. While currently implemented for Binance, the architecture allows for easy extension to other exchanges.
