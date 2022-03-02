#!/bin/bash

#sudo sudo snap install lxd
#sudo gpasswd -a vagrant lxd
#cat /vagrant/fspreseed.yaml | lxd init --preseed
#cat /vagrant/network.yaml | lxc network edit lxdfan0
#sudo cp /var/snap/lxd/common/lxd/cluster.crt /vagrant/cluster.crt


echo "Creando contenedor"
sudo lxc launch ubuntu:20.04 haproxy --target frontServer

sleep 10

echo "instalando haproxy en el contenedor"
sudo lxc exec haproxy -- apt update && apt upgrade -y
sudo lxc exec haproxy -- apt install haproxy -y
sudo lxc exec haproxy -- systemctl enable haproxy

echo "editando la configuracion de haproxy"
sudo lxc file push /vagrant/haproxy.cfg haproxy/etc/haproxy/haproxy.cfg

echo "creando pagina de error"
sudo lxc file push /vagrant/503.http haproxy/etc/haproxy/errors/503.http

echo "Recargando haproxy"
sudo lxc exec haproxy -- systemctl restart haproxy

echo "Redireccionamiento de puertos"
sudo lxc config device add haproxy puerto80 proxy listen=tcp:0.0.0.0:80 connect=tcp:127.0.0.1:80