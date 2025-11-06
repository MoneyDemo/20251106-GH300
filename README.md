# SimpleWeb

一個全面展示現代雲原生開發實踐、基礎設施即程式碼（IaC）和 CI/CD 流程的 ASP.NET Core 示範應用程式。

## 📋 目錄

- [概述](#概述)
- [功能特色](#功能特色)
- [技術堆疊](#技術堆疊)
- [先決條件](#先決條件)
- [快速開始](#快速開始)
- [設定說明](#設定說明)
- [建置與執行](#建置與執行)
- [Docker 支援](#docker-支援)
- [基礎設施即程式碼](#基礎設施即程式碼)
- [Kubernetes 部署](#kubernetes-部署)
- [CI/CD 流程](#cicd-流程)
- [專案結構](#專案結構)
- [測試](#測試)
- [貢獻指南](#貢獻指南)

## 🎯 概述

SimpleWeb 是一個 ASP.NET Core 6.0 MVC 示範應用程式，旨在展示現代 Web 應用程式開發、雲端部署和 DevOps 自動化的最佳實踐。本專案包含以下範例：

- 雲原生應用程式架構
- 多種儲存提供者實作（Azure Blob Storage 和本地端）
- 使用 Bicep 和 Terraform 進行基礎設施佈建
- 使用 Kubernetes 進行容器編排
- Azure DevOps 和 GitHub Actions 的完整 CI/CD 流程
- Azure Application Insights 應用程式監控
- 健康檢查和診斷功能

## ✨ 功能特色

- **檔案上傳系統**：支援多種儲存後端的檔案上傳和管理功能
- **多儲存支援**： 
  - Azure Blob Storage 整合
  - 本地檔案系統儲存
  - 可配置的儲存提供者選擇
- **身份驗證**：Azure App Service 身份驗證整合
- **監控**：Application Insights 遙測和診斷
- **健康檢查**：內建健康檢查端點
- **響應式 UI**：現代化的 MVC 使用者介面
- **Docker 就緒**：具有多階段建置的容器化應用程式
- **雲端部署**：可部署至 Azure App Service 和 Azure Kubernetes Service

## 🛠 技術堆疊

- **框架**：.NET 6.0
- **Web 框架**：ASP.NET Core MVC
- **儲存**：TwentyTwenty.Storage（Azure 和本地提供者）
- **監控**：Azure Application Insights
- **設定**：ASP.NET Core Configuration with User Secrets
- **容器化**：Docker
- **編排**：Kubernetes
- **基礎設施**：Azure Bicep、Terraform
- **CI/CD**：Azure DevOps Pipelines、GitHub Actions
- **測試**：xUnit、Selenium（UI 測試）

### 主要 NuGet 套件

- `Ci.Extensions` (6.0.3)
- `Microsoft.ApplicationInsights.AspNetCore` (2.21.0)
- `Microsoft.AspNetCore.Diagnostics.HealthChecks` (2.2.0)
- `Newtonsoft.Json` (13.0.2)
- `TwentyTwenty.Storage` (2.20.0)
- `TwentyTwenty.Storage.Azure` (2.20.0)
- `TwentyTwenty.Storage.Local` (2.20.0)

## 📦 先決條件

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) 或更新版本
- [Docker Desktop](https://www.docker.com/products/docker-desktop)（用於容器化）
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)（用於 Azure 部署）
- [Terraform](https://www.terraform.io/downloads)（用於基礎設施佈建）
- [kubectl](https://kubernetes.io/docs/tasks/tools/)（用於 Kubernetes 部署）
- Azure 訂閱（用於雲端部署）

## 🚀 快速開始

### 1. 複製儲存庫

```bash
git clone https://github.com/lettucebo/20251106-GH300.git
cd 20251106-GH300
```

### 2. 還原相依套件

```bash
cd src
dotnet restore SimpleWeb.sln
```

### 3. 設定使用者密碼

若要在本地開發環境使用 Azure Storage，請設定使用者密碼：

```bash
cd SimpleWeb
dotnet user-secrets init
dotnet user-secrets set "Storage:Azure:ConnectionString" "YOUR_AZURE_STORAGE_CONNECTION_STRING"
```

詳細資訊請參閱 [ASP.NET Core 開發中安全儲存應用程式密碼](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)。

### 4. 執行應用程式

```bash
dotnet run --project src/SimpleWeb/SimpleWeb.csproj
```

應用程式將在 `http://localhost:80` 提供服務。

## ⚙️ 設定說明

### 儲存設定

應用程式支援兩種在 `appsettings.json` 中設定的儲存類型：

#### 本地儲存（開發環境預設）

```json
{
  "Storage": {
    "Type": 0,  // 0 = 本地, 1 = Azure
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
      "ConnectionString": "user-secrets"  // 從使用者密碼載入
    },
    "FileName": "default.jpg"
  }
}
```

### Application Insights

在 `appsettings.json` 中設定 Application Insights 連接字串：

```json
{
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=YOUR_KEY;IngestionEndpoint=YOUR_ENDPOINT"
  }
}
```

或透過環境變數設定：

```bash
APPINSIGHTS_CONNECTIONSTRING="InstrumentationKey=YOUR_KEY"
```

## 🏗 建置與執行

### 建置方案

```bash
dotnet build src/SimpleWeb.sln --configuration Release
```

### 執行測試

```bash
dotnet test src/SimpleWeb.sln --no-restore --verbosity normal
```

### 使用 GitHub Actions 建置

專案包含 GitHub Actions 工作流程（`.github/workflows/dotnet.yml`），會自動執行：
- 還原相依套件
- 建置方案
- 執行所有測試

## 🐳 Docker 支援

### 建置 Docker 映像

```bash
cd src/SimpleWeb
docker build -t simpleweb:latest .
```

### 執行容器

```bash
docker run -p 8080:80 simpleweb:latest
```

在 `http://localhost:8080` 存取應用程式。

### 多階段 Dockerfile

專案使用多階段 Dockerfile 以優化映像大小：
- **Base**：.NET 6.0 執行環境
- **Build**：.NET 6.0 SDK 用於建置
- **Publish**：發布的成品
- **Final**：包含應用程式的最小執行環境映像

## 🏗️ 基礎設施即程式碼

### Azure Bicep

使用 Azure Bicep 部署基礎設施：

```bash
cd bicep

# 建立資源群組
az group create --name Demo0217 --location eastasia

# 部署基礎設施
az deployment group create \
  --resource-group Demo0217 \
  --template-file main.bicep \
  --parameters @parameters.json \
  --parameters password='YourSecurePassword123!'
```

**建立的資源：**
- 虛擬網路和子網路
- 儲存體帳戶
- 公用 IP 位址
- 網路介面
- Windows 虛擬機器（2019 Datacenter）

### Terraform

使用 Terraform 部署基礎設施：

```bash
cd tf

# 初始化 Terraform
terraform init

# 規劃部署
terraform plan

# 套用設定
terraform apply
```

**建立的資源：**
- 資源群組（使用時間戳記命名）
- App Service 方案（Linux、Standard S1）
- App Service（.NET Core 6.0）

## ☸️ Kubernetes 部署

### 部署至 AKS

```bash
# 套用 Kubernetes 資訊清單
kubectl apply -f manifests/deployment.yml
kubectl apply -f manifests/service.yml

# 驗證部署
kubectl get deployments
kubectl get services
kubectl get pods
```

### Kubernetes 資源

- **Deployment**：`simpleweb`，1 個複本
- **Service**：LoadBalancer，公開連接埠 80
- **Container Image**：從 Azure Container Registry 提取

### 更新容器映像

```bash
kubectl set image deployment/simpleweb simpleweb=demo0903.azurecr.io/simpleweb:v2
```

## 🔄 CI/CD 流程

### Azure DevOps Pipelines

`ci/` 目錄包含各種 Azure DevOps 流程設定：

1. **01.build.yml**：基本建置和測試流程
2. **02.packagescan.yml**：套件漏洞掃描
3. **03.sonarcloud.yml**：使用 SonarCloud 進行程式碼品質分析
4. **04.publish.artifacts.yml**：建置和發布成品
5. **05.multistagerelease.yml**：多階段部署流程
6. **06.dockerseperate.yml**：分階段的 Docker 建置
7. **07.dockerbuildandpush.yml**：建置和推送 Docker 映像
8. **08.aks.yml**：部署至 Azure Kubernetes Service
9. **09.terraform.release.yml**：Terraform 基礎設施部署
10. **10.bicep.yml**：Bicep 基礎設施部署

### GitHub Actions

**工作流程**：`.github/workflows/dotnet.yml`

在每次推送時觸發並執行：
- 檢出程式碼
- 設定 .NET 6.0 SDK
- 還原 NuGet 套件
- 以 Release 組態建置方案
- 執行所有單元和整合測試

## 📁 專案結構

```
20251106-GH300/
├── .github/
│   └── workflows/
│       └── dotnet.yml              # GitHub Actions 工作流程
├── bicep/
│   ├── main.bicep                  # Bicep 基礎設施範本
│   └── parameters.json             # Bicep 參數
├── ci/
│   ├── 01.build.yml                # Azure DevOps 建置流程
│   ├── 02.packagescan.yml          # 套件掃描
│   ├── 03.sonarcloud.yml           # 程式碼品質流程
│   ├── 04.publish.artifacts.yml    # 成品發布
│   ├── 05.multistagerelease.yml    # 多階段發布
│   ├── 06.dockerseperate.yml       # Docker 建置流程
│   ├── 07.dockerbuildandpush.yml   # Docker 推送流程
│   ├── 08.aks.yml                  # AKS 部署
│   ├── 09.terraform.release.yml    # Terraform 部署
│   └── 10.bicep.yml                # Bicep 部署
├── manifests/
│   ├── deployment.yml              # Kubernetes Deployment
│   └── service.yml                 # Kubernetes Service
├── scripts/
│   └── TestifyZeroDowntime.ps1     # 零停機測試腳本
├── src/
│   ├── SimpleWeb/                  # 主要 Web 應用程式
│   │   ├── Controllers/            # MVC 控制器
│   │   ├── Models/                 # 資料模型
│   │   ├── Views/                  # Razor 視圖
│   │   ├── wwwroot/                # 靜態檔案
│   │   ├── Dockerfile              # 容器定義
│   │   ├── Program.cs              # 應用程式進入點
│   │   ├── Startup.cs              # 設定和服務
│   │   └── appsettings.json        # 應用程式設定
│   ├── SimpleWeb.UnitTest/         # 單元測試
│   ├── SimpleWeb.UITest/           # UI/整合測試
│   └── SimpleWeb.sln               # 方案檔
├── tf/
│   └── infra.tf                    # Terraform 基礎設施
└── README.md                       # 本檔案
```

## 🧪 測試

### 單元測試

位於 `src/SimpleWeb.UnitTest/`：

```bash
dotnet test src/SimpleWeb.UnitTest/SimpleWeb.UnitTest.csproj
```

### UI 測試

位於 `src/SimpleWeb.UITest/` 的 Selenium 基礎 UI 測試：

```bash
dotnet test src/SimpleWeb.UITest/SimpleWeb.UITest.csproj
```

### 零停機測試

用於驗證零停機部署的 PowerShell 腳本：

```powershell
.\scripts\TestifyZeroDowntime.ps1
```

## 🤝 貢獻指南

歡迎貢獻！請遵循以下準則：

1. Fork 此儲存庫
2. 建立功能分支（`git checkout -b feature/amazing-feature`）
3. 提交您的變更（`git commit -m 'Add some amazing feature'`）
4. 推送至分支（`git push origin feature/amazing-feature`）
5. 開啟 Pull Request

### 開發準則

- 遵循現有的程式碼風格和慣例
- 為新功能撰寫單元測試
- 視需要更新文件
- 在提交 PR 前確保所有測試通過
- 保持提交的原子性和良好描述

## 📄 授權

這是一個用於教育和示範目的的示範專案。

## 🔗 資源

- [ASP.NET Core 文件](https://docs.microsoft.com/en-us/aspnet/core/)
- [Azure Bicep 文件](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/)
- [Terraform Azure Provider](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)
- [Kubernetes 文件](https://kubernetes.io/docs/)
- [Azure DevOps Pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/)
- [GitHub Actions 文件](https://docs.github.com/en/actions)
- [ASP.NET Core 開發中安全儲存應用程式密碼](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

## 📧 支援

若有問題或意見，請在 GitHub 儲存庫中開啟 issue。