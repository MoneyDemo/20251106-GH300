# SimpleWebRazor - .NET 9 Razor Pages 範例專案

這是一個基於 .NET 9 Razor Pages 的範例專案，參考了原始的 .NET 6 MVC 專案 (SimpleWeb) 進行轉換。

## 專案特點

- **框架**: ASP.NET Core 9.0 Razor Pages
- **語言**: C# with nullable reference types enabled
- **儲存方式**: 支援本地檔案儲存和 Azure Blob Storage
- **監控**: 整合 Application Insights
- **健康檢查**: 提供 `/health` 端點

## 專案結構

```
SimpleWebRazor/
├── Models/                    # 資料模型
│   ├── ErrorViewModel.cs     # 錯誤頁面模型
│   └── StorageType.cs        # 儲存類型列舉
├── Pages/                    # Razor Pages
│   ├── Index.cshtml          # 首頁
│   ├── Index.cshtml.cs       # 首頁程式碼
│   ├── Upload.cshtml         # 上傳頁面
│   ├── Upload.cshtml.cs      # 上傳頁面程式碼
│   ├── Privacy.cshtml        # 隱私政策頁面
│   ├── Privacy.cshtml.cs     # 隱私政策程式碼
│   ├── Error.cshtml          # 錯誤頁面
│   ├── Error.cshtml.cs       # 錯誤頁面程式碼
│   └── Shared/              # 共用檢視
│       └── _Layout.cshtml   # 版面配置
├── wwwroot/                 # 靜態檔案
├── Program.cs               # 應用程式進入點
├── appsettings.json         # 應用程式設定
└── Dockerfile              # Docker 容器設定

```

## 與原 MVC 專案的主要差異

### 架構差異

1. **MVC (SimpleWeb)**: 使用 Controllers + Views 模式
   - Controllers/HomeController.cs
   - Views/Home/Index.cshtml

2. **Razor Pages (SimpleWebRazor)**: 使用 Page Model 模式
   - Pages/Index.cshtml + Index.cshtml.cs

### 程式碼變更

1. **Program.cs**: 簡化了配置，不再使用 Startup.cs
   - MVC: `webBuilder.UseStartup<Startup>()`
   - Razor Pages: 直接在 Program.cs 中配置服務和中介軟體

2. **路由**:
   - MVC: 使用 Controller/Action 路由模式
   - Razor Pages: 使用基於檔案的路由 (約定優於配置)

3. **頁面模型**:
   - MVC: ViewBag 傳遞資料
   - Razor Pages: 強型別的 PageModel 屬性

## 功能說明

### 首頁 (/)
顯示：
- 當前時間
- 網站設定（從 appsettings.json 讀取）
- 使用者資訊（從 HTTP Header 讀取）
- 主機名稱

### 上傳頁面 (/Upload)
- 支援檔案上傳
- 可設定使用本地儲存或 Azure Blob Storage
- 上傳後自動更新 appsettings.json

### 隱私政策頁面 (/Privacy)
- 顯示隱私政策說明

### 健康檢查端點 (/health)
- 回傳 JSON 格式的健康狀態
- 用於容器編排和監控

## 執行專案

### 本地開發

```bash
cd src/SimpleWebRazor
dotnet run
```

預設會在 `http://localhost:5000` 啟動 (或使用 launchSettings.json 中的設定)

### Docker 執行

```bash
cd src/SimpleWebRazor
docker build -t simpleweb-razor .
docker run -p 8080:80 simpleweb-razor
```

## 設定說明

### appsettings.json

```json
{
  "AppSettings": {
    "SiteName": "Local site"  // 網站名稱
  },
  "Storage": {
    "Type": 1,  // 1=Local, 2=Azure
    "Azure": {
      "ConnectionString": "..."  // Azure Storage 連接字串
    },
    "FileName": "..."  // 目前的檔案名稱
  }
}
```

## 相依套件

- **Ci.Extensions**: 擴充功能庫
- **Microsoft.ApplicationInsights.AspNetCore**: Application Insights SDK
- **Microsoft.AspNetCore.Diagnostics.HealthChecks**: 健康檢查
- **Newtonsoft.Json**: JSON 處理
- **TwentyTwenty.Storage**: 統一的儲存抽象層
  - TwentyTwenty.Storage.Azure: Azure Blob Storage 支援
  - TwentyTwenty.Storage.Local: 本地檔案系統支援

## 注意事項

1. **儲存設定**: 預設使用本地儲存 (Type = 1)，如需使用 Azure Storage，請設定 Type = 2 並提供有效的連接字串
2. **Application Insights**: 需要設定有效的連接字串才能啟用遙測功能
3. **上傳功能**: 上傳的檔案會儲存在 wwwroot/uploads/ 目錄下（本地模式）

## 轉換心得

從 .NET 6 MVC 轉換到 .NET 9 Razor Pages 的主要優勢：

1. **簡化的結構**: Page Model 模式更直觀，減少了 Controller 和 View 之間的分離
2. **更好的組織**: 每個頁面的邏輯和視圖放在一起，更容易維護
3. **現代化的 API**: .NET 9 提供了更好的效能和新功能
4. **簡化的設定**: 不需要 Startup.cs，Program.cs 更簡潔

## 作者

Money Yu

## 授權

與原專案相同
