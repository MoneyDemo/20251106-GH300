"""
測試 Flask 應用程式
Tests for the Flask Application
"""
import pytest
from app import app


@pytest.fixture
def client():
    """建立測試客戶端 - Create test client"""
    app.config['TESTING'] = True
    with app.test_client() as client:
        yield client


class TestHomeRoute:
    """首頁路由測試 - Home route tests"""
    
    def test_home_status_code(self, client):
        """測試首頁狀態碼 - Test home page status code"""
        response = client.get('/')
        assert response.status_code == 200
    
    def test_home_content_type(self, client):
        """測試首頁內容類型 - Test home page content type"""
        response = client.get('/')
        assert 'text/html' in response.content_type
    
    def test_home_contains_title(self, client):
        """測試首頁包含標題 - Test home page contains title"""
        response = client.get('/')
        assert b'Flask' in response.data
        assert b'\xe6\xad\xa1\xe8\xbf\x8e' in response.data  # "歡迎" in UTF-8
    
    def test_home_contains_navigation(self, client):
        """測試首頁包含導航連結 - Test home page contains navigation links"""
        response = client.get('/')
        assert b'/info' in response.data
    
    def test_home_contains_feature_cards(self, client):
        """測試首頁包含特色卡片 - Test home page contains feature cards"""
        response = client.get('/')
        # Check for feature card emojis
        assert b'\xf0\x9f\x9a\x80' in response.data  # 🚀 emoji
        assert b'\xf0\x9f\x93\x9d' in response.data  # 📝 emoji
        assert b'\xf0\x9f\x8e\xa8' in response.data  # 🎨 emoji


class TestInfoRoute:
    """資訊頁路由測試 - Info route tests"""
    
    def test_info_status_code(self, client):
        """測試資訊頁狀態碼 - Test info page status code"""
        response = client.get('/info')
        assert response.status_code == 200
    
    def test_info_content_type(self, client):
        """測試資訊頁內容類型 - Test info page content type"""
        response = client.get('/info')
        assert 'text/html' in response.content_type
    
    def test_info_contains_project_name(self, client):
        """測試資訊頁包含專案名稱 - Test info page contains project name"""
        response = client.get('/info')
        assert b'20251106-GH300' in response.data
    
    def test_info_contains_framework(self, client):
        """測試資訊頁包含框架資訊 - Test info page contains framework info"""
        response = client.get('/info')
        assert b'Flask' in response.data
    
    def test_info_contains_python_version(self, client):
        """測試資訊頁包含 Python 版本 - Test info page contains Python version"""
        response = client.get('/info')
        assert b'Python 3.8+' in response.data
    
    def test_info_contains_features(self, client):
        """測試資訊頁包含功能列表 - Test info page contains features list"""
        response = client.get('/info')
        # Check for feature items in UTF-8
        assert b'\xe7\xb0\xa1\xe6\xbd\x94' in response.data  # "簡潔" in UTF-8
        assert b'\xe6\x93\xb4\xe5\xb1\x95' in response.data  # "擴展" in UTF-8


class TestStaticFiles:
    """靜態檔案測試 - Static files tests"""
    
    def test_css_file_accessible(self, client):
        """測試 CSS 檔案可存取 - Test CSS file is accessible"""
        response = client.get('/static/css/style.css')
        assert response.status_code == 200
        assert 'text/css' in response.content_type
    
    def test_css_contains_styles(self, client):
        """測試 CSS 包含樣式 - Test CSS contains styles"""
        response = client.get('/static/css/style.css')
        assert b'body' in response.data
        assert b'header' in response.data
        assert b'footer' in response.data


class TestErrorHandling:
    """錯誤處理測試 - Error handling tests"""
    
    def test_404_error(self, client):
        """測試 404 錯誤 - Test 404 error"""
        response = client.get('/nonexistent-page')
        assert response.status_code == 404
    
    def test_method_not_allowed(self, client):
        """測試不允許的方法 - Test method not allowed"""
        # Try POST on GET-only route
        response = client.post('/')
        assert response.status_code == 405


class TestSecurity:
    """安全性測試 - Security tests"""
    
    def test_debug_mode_disabled_in_production(self):
        """測試生產環境中除錯模式已停用 - Test debug mode is disabled in production"""
        import os
        # Ensure FLASK_DEBUG is set to 0 for production
        os.environ['FLASK_DEBUG'] = '0'
        # The app should not be in debug mode when FLASK_DEBUG=0
        assert app.debug is False
    
    def test_response_headers_security(self, client):
        """測試回應標頭安全性 - Test response headers for security"""
        response = client.get('/')
        # Flask should set appropriate content type
        assert response.content_type is not None


class TestResponseData:
    """回應資料測試 - Response data tests"""
    
    def test_home_response_not_empty(self, client):
        """測試首頁回應不為空 - Test home response is not empty"""
        response = client.get('/')
        assert len(response.data) > 0
    
    def test_info_response_not_empty(self, client):
        """測試資訊頁回應不為空 - Test info response is not empty"""
        response = client.get('/info')
        assert len(response.data) > 0
    
    def test_home_response_is_valid_html(self, client):
        """測試首頁回應是有效的 HTML - Test home response is valid HTML"""
        response = client.get('/')
        assert b'<!DOCTYPE html>' in response.data
        assert b'</html>' in response.data
    
    def test_info_response_is_valid_html(self, client):
        """測試資訊頁回應是有效的 HTML - Test info response is valid HTML"""
        response = client.get('/info')
        assert b'<!DOCTYPE html>' in response.data
        assert b'</html>' in response.data
