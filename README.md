# SimpleWeb

A comprehensive demo ASP.NET Core web application showcasing modern cloud-native development practices, infrastructure as code, and CI/CD pipelines.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Building and Running](#building-and-running)
- [Docker Support](#docker-support)
- [Infrastructure as Code](#infrastructure-as-code)
- [Kubernetes Deployment](#kubernetes-deployment)
- [CI/CD Pipelines](#cicd-pipelines)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [Contributing](#contributing)

## 🎯 Overview

SimpleWeb is a demo ASP.NET Core 6.0 MVC application designed to demonstrate best practices in modern web application development, cloud deployment, and DevOps automation. The project includes examples of:

- Cloud-native application architecture
- Multiple storage provider implementations (Azure Blob Storage and Local)
- Infrastructure provisioning using both Bicep and Terraform
- Container orchestration with Kubernetes
- Comprehensive CI/CD pipelines for Azure DevOps and GitHub Actions
- Application monitoring with Azure Application Insights
- Health checks and diagnostics

## ✨ Features

- **File Upload System**: Upload and manage files with support for multiple storage backends
- **Multi-Storage Support**: 
  - Azure Blob Storage integration
  - Local file system storage
  - Configurable storage provider selection
- **Authentication**: Azure App Service authentication integration
- **Monitoring**: Application Insights telemetry and diagnostics
- **Health Checks**: Built-in health check endpoints
- **Responsive UI**: Modern MVC-based user interface
- **Docker Ready**: Containerized application with multi-stage Dockerfile
- **Cloud Deployable**: Ready for deployment to Azure App Service and Azure Kubernetes Service

## 🛠 Technology Stack

- **Framework**: .NET 6.0
- **Web Framework**: ASP.NET Core MVC
- **Storage**: TwentyTwenty.Storage (Azure & Local providers)
- **Monitoring**: Azure Application Insights
- **Configuration**: ASP.NET Core Configuration with User Secrets
- **Containerization**: Docker
- **Orchestration**: Kubernetes
- **Infrastructure**: Azure Bicep, Terraform
- **CI/CD**: Azure DevOps Pipelines, GitHub Actions
- **Testing**: xUnit, Selenium (UI Tests)

### Key NuGet Packages

- `Ci.Extensions` (6.0.3)
- `Microsoft.ApplicationInsights.AspNetCore` (2.21.0)
- `Microsoft.AspNetCore.Diagnostics.HealthChecks` (2.2.0)
- `Newtonsoft.Json` (13.0.2)
- `TwentyTwenty.Storage` (2.20.0)
- `TwentyTwenty.Storage.Azure` (2.20.0)
- `TwentyTwenty.Storage.Local` (2.20.0)

## 📦 Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerization)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) (for Azure deployments)
- [Terraform](https://www.terraform.io/downloads) (for infrastructure provisioning)
- [kubectl](https://kubernetes.io/docs/tasks/tools/) (for Kubernetes deployments)
- An Azure subscription (for cloud deployments)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/lettucebo/20251106-GH300.git
cd 20251106-GH300
```

### 2. Restore Dependencies

```bash
cd src
dotnet restore SimpleWeb.sln
```

### 3. Configure User Secrets

For local development with Azure Storage, configure user secrets:

```bash
cd SimpleWeb
dotnet user-secrets init
dotnet user-secrets set "Storage:Azure:ConnectionString" "YOUR_AZURE_STORAGE_CONNECTION_STRING"
```

See [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets) for more information.

### 4. Run the Application

```bash
dotnet run --project src/SimpleWeb/SimpleWeb.csproj
```

The application will be available at `http://localhost:80`.

## ⚙️ Configuration

### Storage Configuration

The application supports two storage types configured in `appsettings.json`:

#### Local Storage (Default for Development)

```json
{
  "Storage": {
    "Type": 0,  // 0 = Local, 1 = Azure
    "FileName": "default.jpg"
  }
}
```

#### Azure Blob Storage

```json
{
  "Storage": {
    "Type": 1,  // 1 = Azure Blob Storage
    "Azure": {
      "ConnectionString": "user-secrets"  // Loaded from user secrets
    },
    "FileName": "default.jpg"
  }
}
```

### Application Insights

Configure Application Insights connection string in `appsettings.json`:

```json
{
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=YOUR_KEY;IngestionEndpoint=YOUR_ENDPOINT"
  }
}
```

Or set via environment variable:

```bash
APPINSIGHTS_CONNECTIONSTRING="InstrumentationKey=YOUR_KEY"
```

## 🏗 Building and Running

### Build the Solution

```bash
dotnet build src/SimpleWeb.sln --configuration Release
```

### Run Tests

```bash
dotnet test src/SimpleWeb.sln --no-restore --verbosity normal
```

### Build with GitHub Actions

The project includes a GitHub Actions workflow (`.github/workflows/dotnet.yml`) that automatically:
- Restores dependencies
- Builds the solution
- Runs all tests

## 🐳 Docker Support

### Build Docker Image

```bash
cd src/SimpleWeb
docker build -t simpleweb:latest .
```

### Run Container

```bash
docker run -p 8080:80 simpleweb:latest
```

Access the application at `http://localhost:8080`.

### Multi-Stage Dockerfile

The project uses a multi-stage Dockerfile for optimized image size:
- **Base**: .NET 6.0 runtime
- **Build**: .NET 6.0 SDK for building
- **Publish**: Published artifacts
- **Final**: Minimal runtime image with application

## 🏗️ Infrastructure as Code

### Azure Bicep

Deploy infrastructure using Azure Bicep:

```bash
cd bicep

# Create resource group
az group create --name Demo0217 --location eastasia

# Deploy infrastructure
az deployment group create \
  --resource-group Demo0217 \
  --template-file main.bicep \
  --parameters @parameters.json \
  --parameters password='YourSecurePassword123!'
```

**Resources Created:**
- Virtual Network and Subnet
- Storage Account
- Public IP Address
- Network Interface
- Windows Virtual Machine (2019 Datacenter)

### Terraform

Deploy infrastructure using Terraform:

```bash
cd tf

# Initialize Terraform
terraform init

# Plan deployment
terraform plan

# Apply configuration
terraform apply
```

**Resources Created:**
- Resource Group (with timestamp-based naming)
- App Service Plan (Linux, Standard S1)
- App Service (.NET Core 6.0)

## ☸️ Kubernetes Deployment

### Deploy to AKS

```bash
# Apply Kubernetes manifests
kubectl apply -f manifests/deployment.yml
kubectl apply -f manifests/service.yml

# Verify deployment
kubectl get deployments
kubectl get services
kubectl get pods
```

### Kubernetes Resources

- **Deployment**: `simpleweb` with 1 replica
- **Service**: LoadBalancer exposing port 80
- **Container Image**: Pulled from Azure Container Registry

### Update Container Image

```bash
kubectl set image deployment/simpleweb simpleweb=demo0903.azurecr.io/simpleweb:v2
```

## 🔄 CI/CD Pipelines

### Azure DevOps Pipelines

The `ci/` directory contains various Azure DevOps pipeline configurations:

1. **01.build.yml**: Basic build and test pipeline
2. **02.packagescan.yml**: Package vulnerability scanning
3. **03.sonarcloud.yml**: Code quality analysis with SonarCloud
4. **04.publish.artifacts.yml**: Build and publish artifacts
5. **05.multistagerelease.yml**: Multi-stage deployment pipeline
6. **06.dockerseperate.yml**: Docker build in separate stages
7. **07.dockerbuildandpush.yml**: Build and push Docker images
8. **08.aks.yml**: Deploy to Azure Kubernetes Service
9. **09.terraform.release.yml**: Terraform infrastructure deployment
10. **10.bicep.yml**: Bicep infrastructure deployment

### GitHub Actions

**Workflow**: `.github/workflows/dotnet.yml`

Triggers on every push and performs:
- Checkout code
- Setup .NET 6.0 SDK
- Restore NuGet packages
- Build solution in Release configuration
- Run all unit and integration tests

## 📁 Project Structure

```
20251106-GH300/
├── .github/
│   └── workflows/
│       └── dotnet.yml              # GitHub Actions workflow
├── bicep/
│   ├── main.bicep                  # Bicep infrastructure template
│   └── parameters.json             # Bicep parameters
├── ci/
│   ├── 01.build.yml                # Azure DevOps build pipeline
│   ├── 02.packagescan.yml          # Package scanning
│   ├── 03.sonarcloud.yml           # Code quality pipeline
│   ├── 04.publish.artifacts.yml    # Artifact publishing
│   ├── 05.multistagerelease.yml    # Multi-stage release
│   ├── 06.dockerseperate.yml       # Docker build pipeline
│   ├── 07.dockerbuildandpush.yml   # Docker push pipeline
│   ├── 08.aks.yml                  # AKS deployment
│   ├── 09.terraform.release.yml    # Terraform deployment
│   └── 10.bicep.yml                # Bicep deployment
├── manifests/
│   ├── deployment.yml              # Kubernetes Deployment
│   └── service.yml                 # Kubernetes Service
├── scripts/
│   └── TestifyZeroDowntime.ps1     # Zero downtime testing script
├── src/
│   ├── SimpleWeb/                  # Main web application
│   │   ├── Controllers/            # MVC Controllers
│   │   ├── Models/                 # Data models
│   │   ├── Views/                  # Razor views
│   │   ├── wwwroot/                # Static files
│   │   ├── Dockerfile              # Container definition
│   │   ├── Program.cs              # Application entry point
│   │   ├── Startup.cs              # Configuration and services
│   │   └── appsettings.json        # Application configuration
│   ├── SimpleWeb.UnitTest/         # Unit tests
│   ├── SimpleWeb.UITest/           # UI/Integration tests
│   └── SimpleWeb.sln               # Solution file
├── tf/
│   └── infra.tf                    # Terraform infrastructure
└── README.md                       # This file
```

## 🧪 Testing

### Unit Tests

Located in `src/SimpleWeb.UnitTest/`:

```bash
dotnet test src/SimpleWeb.UnitTest/SimpleWeb.UnitTest.csproj
```

### UI Tests

Selenium-based UI tests in `src/SimpleWeb.UITest/`:

```bash
dotnet test src/SimpleWeb.UITest/SimpleWeb.UITest.csproj
```

### Zero Downtime Testing

PowerShell script to verify zero-downtime deployments:

```powershell
.\scripts\TestifyZeroDowntime.ps1
```

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Guidelines

- Follow existing code style and conventions
- Write unit tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR
- Keep commits atomic and well-described

## 📄 License

This is a demo project for educational and demonstration purposes.

## 🔗 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Azure Bicep Documentation](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/)
- [Terraform Azure Provider](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)
- [Kubernetes Documentation](https://kubernetes.io/docs/)
- [Azure DevOps Pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Safe storage of app secrets in development](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

## 📧 Support

For questions or issues, please open an issue in the GitHub repository.