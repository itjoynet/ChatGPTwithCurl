# ChatGPTwithCurl
项目介绍：
ChatGPTAPI服务端仅支持TLS1.2/TLS1.3版本的ssl
C#.framework 4.6以下无法连接TLS1.2 ssl
程序部署后，服务器环境win10以下，server2016以下无法连接TLS1.2 ssl

本项目使用Curl.exe连接ChatGPTAPI，不存在无法创建SSL连接问题。
win10自带Curl.exe，其他系统可绿色安装。

使用方法：
1. 在web.config中增加以下配置

    <add key="CurlPath" value="C:\\curl\\bin\\curl.exe"/>
    <!-- ChatGPT接口配置 -->
    <add key="ChatGPT_Api_Url" value="https://api.openai.com/v1/completions"/>
    <add key="ChatGPT_Api_Key" value="登录openai账号后创建Authorization_Key"/>
    <!-- 代理配置 -->
    <add key="Proxy_Host" value="1.1.1.1"/>
    <add key="Proxy_Port" value="8080"/>
    <add key="Proxy_Username" value=""/>
    <add key="Proxy_Password" value=""/>
    
2. 在程序中发起提问
