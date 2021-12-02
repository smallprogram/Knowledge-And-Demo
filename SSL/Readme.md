自建证书

https://github.com/FiloSottile/mkcert

mkcert -pkcs12 somedomain.net

~~配置IIS pfx证书，系统安装git，在git目录有openssl~~
~~C:\Program Files\Git\usr\bin~~

~~将其添加如系统环境变量(个人)，可以直接在CMD中使用openssl~~

~~openssl pkcs12 -export -out 192.159.93.130.pfx -in .\192.159.93.130.pem -inkey .\192.159.93.130-key.pem~~

~~然后输入密码，生成pfx证书~~
