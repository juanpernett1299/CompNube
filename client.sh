#!/bin/bash
echo "configurando el resolv.conf con cat"
cat <<TEST> /etc/resolv.conf
nameserver 8.8.8.8
TEST

echo "instalando lxd"
sudo snap install lxd

echo "iniciando lxd"
sudo newgrp lxd
sudo lxd init --auto

sleep 10

echo "Creando contenedor"
sudo lxc launch ubuntu:20.04 web

sleep 10

echo "instalando apache en el contenedor"
sudo lxc exec web -- apt-get install apache2 -y

echo "Personalizando sitio web"
sudo touch index.html
sudo echo "<!DOCTYPE html>
<html>
<body>
<h1>Hola mundo</h1>
</body>
</html>" >> /vagrant/index.html
sudo lxc file push /vagrant/index.html web/var/www/html/index.html

echo "Redireccionamiento de puertos"
sudo lxc config device add web puerto80 proxy listen=tcp:192.168.100.2:5080 connect=tcp:127.0.0.1:80

sudo lxc exec web -- systemctl restart apache2
