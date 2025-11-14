# Python Flask 範例網站

這是一個使用 Python Flask 框架建立的簡單範例網站，包含首頁和資訊展示頁面。

## 專案結構

```
python-webapp/
├── app.py                 # Flask 應用程式主檔案
├── test_app.py            # 測試檔案
├── requirements.txt       # Python 套件依賴清單
├── templates/            # HTML 模板目錄
│   ├── base.html         # 基礎模板（包含導航列和頁尾）
│   ├── index.html        # 首頁模板
│   └── info.html         # 資訊頁模板
└── static/               # 靜態檔案目錄
    └── css/
        └── style.css     # 網站樣式表
```

## 功能特色

- ✅ 使用 Flask 輕量級框架
- ✅ 包含首頁與資訊展示頁
- ✅ 響應式設計，支援行動裝置
- ✅ 清晰的專案結構
- ✅ 中文介面
- ✅ 簡潔美觀的 UI 設計
- ✅ 完整的測試套件 (21 個測試，77% 程式碼覆蓋率)

## 環境需求

- Python 3.8 或更高版本
- pip (Python 套件管理工具)

## 安裝步驟

### 1. 確認 Python 版本

```bash
python3 --version
```

### 2. 安裝相依套件

在 `python-webapp` 目錄下執行：

```bash
cd python-webapp
pip3 install -r requirements.txt
```

或使用虛擬環境（建議）：

```bash
# 建立虛擬環境
python3 -m venv venv

# 啟用虛擬環境
# Linux/Mac:
source venv/bin/activate
# Windows:
venv\Scripts\activate

# 安裝套件
pip install -r requirements.txt
```

## 啟動網站

### 開發模式啟動

```bash
python3 app.py
```

啟動後，在瀏覽器中開啟 [http://localhost:5000](http://localhost:5000) 即可查看網站。

預設會啟用除錯模式（debug mode）。若要停用除錯模式，可設定環境變數：

```bash
export FLASK_DEBUG=0
python3 app.py
```

### 使用 Flask CLI 啟動

```bash
# 設定 Flask 應用程式
export FLASK_APP=app.py
export FLASK_ENV=development

# 啟動伺服器
flask run
```

## 頁面說明

### 首頁 (`/`)
- 展示網站歡迎訊息
- 顯示專案主要特色
- 提供導航連結

### 資訊頁 (`/info`)
- 展示專案詳細資訊
- 包含技術架構說明
- 列出專案特色功能

## 測試

### 執行測試

在 `python-webapp` 目錄下執行：

```bash
# 執行所有測試
python3 -m pytest test_app.py -v

# 執行測試並顯示程式碼覆蓋率
python3 -m pytest test_app.py --cov=app --cov-report=term-missing

# 執行測試並產生 HTML 覆蓋率報告
python3 -m pytest test_app.py --cov=app --cov-report=html
```

### 測試內容

測試套件包含 21 個測試，涵蓋以下方面：

- **首頁路由測試**：測試首頁的狀態碼、內容類型、標題和導航
- **資訊頁路由測試**：測試資訊頁的內容和顯示
- **靜態檔案測試**：測試 CSS 檔案的存取
- **錯誤處理測試**：測試 404 錯誤和不允許的方法
- **安全性測試**：測試除錯模式設定和回應標頭
- **回應資料測試**：測試 HTML 回應的有效性

測試覆蓋率達到 **77%**，主要未涵蓋的是應用程式啟動程式碼。

## 擴展建議

此範例網站架構清晰，方便後續擴展，可以考慮：

1. **資料庫整合**：加入 SQLAlchemy 連接資料庫
2. **用戶認證**：使用 Flask-Login 實作登入功能
3. **API 端點**：建立 RESTful API
4. **表單處理**：使用 Flask-WTF 處理表單
5. **部署**：部署到 Heroku、AWS 或其他雲端平台

## 開發注意事項

- 開發模式會自動重新載入程式碼變更
- 靜態檔案放置於 `static/` 目錄
- HTML 模板放置於 `templates/` 目錄
- 生產環境請勿使用 `debug=True`

## 技術架構

- **後端框架**：Flask 3.0.0
- **前端技術**：HTML5, CSS3
- **Python 版本**：3.8+
- **測試框架**：pytest 8.3.3, pytest-cov 5.0.0

## 授權

此專案為範例程式碼，可自由使用與修改。

## 參考資源

- [Flask 官方文件](https://flask.palletsprojects.com/)
- [Jinja2 模板引擎](https://jinja.palletsprojects.com/)
- [Flask 快速入門](https://flask.palletsprojects.com/quickstart/)
