# ChatGPTwithCurl
项目介绍：
ChatGPTAPI服务端仅支持TLS1.2/TLS1.3版本的ssl
C#.framework 4.6以下无法连接TLS1.2 ssl
程序部署后，服务器环境win10以下，server2016以下无法连接TLS1.2 ssl

本项目使用Curl.exe连接ChatGPTAPI，不存在无法创建SSL连接问题。
win10自带Curl.exe，其他系统可绿色安装。

使用方法：
1. 在web.config中增加以下配置  
    CurlPath ：curl.exe完整路径，未配置时取程序/bin/curl/bin/curl.exe  
    ChatGPT_Api_Url：api服务路径  
    ChatGPT_Api_Key：登录openai账号后创建Authorization_Key  
    Proxy_Host：代理服务器IP,大陆地区无法直接访问api  
    Proxy_Port：代理服务器端口号  
    Proxy_Username：代理登录账户  
    Proxy_Password：代理登录密码  
      
2. 在程序中发起提问  
   var MyChatGPT = new ChatGPT.API.Service();  
   var answer = MyChatGPT.Question2(question);  
