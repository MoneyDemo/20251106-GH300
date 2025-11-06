"""
簡單的 Flask 網站範例
Simple Flask Web Application Example
"""
from flask import Flask, render_template

app = Flask(__name__)


@app.route('/')
def home():
    """首頁 - Home Page"""
    return render_template('index.html')


@app.route('/info')
def info():
    """資訊頁 - Information Page"""
    repo_info = {
        'name': '20251106-GH300',
        'description': '這是一個示範專案，展示如何使用 Python Flask 建立簡單的網站。',
        'framework': 'Flask',
        'language': 'Python 3.8+',
        'features': [
            '簡潔的專案結構',
            '易於擴展與維護',
            '包含基本的 HTML/CSS 前端',
            '支援多個頁面路由'
        ]
    }
    return render_template('info.html', repo_info=repo_info)


if __name__ == '__main__':
    # 開發模式運行，啟用除錯功能
    # Run in development mode with debug enabled
    # WARNING: debug=True and host='0.0.0.0' should ONLY be used for development
    # For production, use a proper WSGI server (e.g., Gunicorn, uWSGI)
    # Set FLASK_DEBUG=0 environment variable to disable debug mode
    import os
    debug_mode = os.environ.get('FLASK_DEBUG', '1') == '1'
    app.run(debug=debug_mode, host='0.0.0.0', port=5000)
