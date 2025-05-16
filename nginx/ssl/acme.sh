git clone --depth 1 https://gh.sikro.store/github.com/acmesh-official/acme.sh.git
cd ./acme.sh
./acme.sh --install -m jindexyang@gmail.com

acme.sh --set-default-ca --server letsencrypt

acme.sh --issue -d www.lidaoisland.com --webroot /usr/local/openresty/nginx/html/

acme.sh --issue -d www.zincsspace.com --webroot /usr/local/openresty/nginx/html/

acme.sh --install-cert -d www.lidaoisland.com \
--key-file       /usr/local/openresty/nginx/conf/cert/www.lidaoisland.com.key  \
--fullchain-file /usr/local/openresty/nginx/conf/cert/www.lidaoisland.com.pem \
--reloadcmd     "openresty -s reload"

acme.sh --install-cert -d www.zincsspace.com \
--key-file       /usr/local/openresty/nginx/conf/cert/www.zincsspace.com.key  \
--fullchain-file /usr/local/openresty/nginx/conf/cert/www.zincsspace.com.pem \
--reloadcmd     "openresty -s reload"