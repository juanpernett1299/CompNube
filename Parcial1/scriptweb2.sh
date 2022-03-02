#!/bin/bash

#sudo sudo snap install lxd
#sudo gpasswd -a vagrant lxd
#certificado=$(</vagrant/cluster.crt)
#cat <<TEST> /vagrant/bS2preseed.yaml
#config: {}
#networks: []
#storage_pools: []
#profiles: []
#projects: []
#cluster:
#  server_name: backServer2
#  enabled: true
#  member_config:
#  - entity: storage-pool
#    name: local
#    key: source
#    value: ""
#    description: '"source" property for storage pool "local"'
#  cluster_address: 192.168.100.2:8443
#  cluster_certificate: |
#$certificado
#  server_address: 192.168.100.5:8443
#  cluster_password: "1234"
#  cluster_certificate_path: ""
#TEST

#sudo cat /vagrant/bS2preseed.yaml | lxd init --preseed

echo "Creando contenedor"
sudo lxc launch ubuntu:20.04 web2 --target backServer2

sleep 10

sudo lxc launch ubuntu:20.04 backup2 --target backServer2

sleep 10

echo "instalando apache en los contenedores"
sudo lxc exec web2 -- apt update && apt upgrade -y
sudo lxc exec web2 -- apt install apache2 -y
sudo lxc exec web2 -- systemctl enable apache2
sudo lxc exec backup2 -- apt update && apt upgrade -y
sudo lxc exec backup2 -- apt install apache2 -y
sudo lxc exec backup2 -- systemctl enable apache2

echo "Personalizando sitio web2"
sudo lxc file push /vagrant/web2/index.html web2/var/www/html/index.html

echo "Personalizando sitio backup2"
sudo lxc file push /vagrant/bweb2/index.html backup2/var/www/html/index.html


sudo lxc exec web2 -- systemctl restart apache2
sudo lxc exec backup2 -- systemctl restart apache2