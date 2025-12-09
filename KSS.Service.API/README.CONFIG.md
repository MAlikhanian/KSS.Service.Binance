# Configuration Setup

This project uses configuration files to manage settings. **Never commit actual API keys or secrets to the repository.**

## Setup Instructions

### 1. Copy Example Files

Copy the example configuration files to create your actual configuration:

```bash
# Windows PowerShell
Copy-Item appsettings.json.example appsettings.json
Copy-Item appsettings.Development.json.example appsettings.Development.json

# Linux/Mac
cp appsettings.json.example appsettings.json
cp appsettings.Development.json.example appsettings.Development.json
```

### 2. Update Configuration Files

Edit `appsettings.json` and `appsettings.Development.json` with your actual API credentials:

```json
{
  "Exchange": {
    "ApiKey": "your-actual-api-key",
    "ApiSecret": "your-actual-api-secret"
  }
}
```

### 3. Alternative: Use User Secrets (Recommended for Development)

For local development, you can use .NET User Secrets instead of editing files:

```bash
dotnet user-secrets set "Exchange:ApiKey" "your-api-key"
dotnet user-secrets set "Exchange:ApiSecret" "your-api-secret"
```

User Secrets are stored outside the project directory and are automatically ignored by git.

### 4. Production Configuration

For production environments:

- **Azure App Service**: Use Application Settings in Azure Portal
- **Docker**: Use environment variables or mounted secrets
- **Kubernetes**: Use ConfigMaps and Secrets
- **Other**: Use environment variables or secure configuration management

## Configuration Priority

Configuration is loaded in this order (later values override earlier ones):

1. `appsettings.json`
2. `appsettings.{Environment}.json` (e.g., `appsettings.Development.json`)
3. User Secrets (Development only)
4. Environment Variables
5. Command-line arguments

## Security Notes

- ✅ **DO** commit `appsettings.json.example` files
- ✅ **DO** use User Secrets for local development
- ✅ **DO** use environment variables or secure vaults for production
- ❌ **DON'T** commit actual API keys or secrets
- ❌ **DON'T** share secrets in chat or email

