#!/bin/bash

sudo sudo snap install lxd
sudo gpasswd -a vagrant lxd
certificado=$(</vagrant/cluster.crt)
cat <<TEST> /vagrant/bS1preseed.yaml
config: {}
networks: []
storage_pools: []
profiles: []
projects: []
cluster:
  server_name: backServer1
  enabled: true
  member_config:
  - entity: storage-pool
    name: local
    key: source
    value: ""
    description: '"source" property for storage pool "local"'
  cluster_address: 192.168.100.2:8443
  cluster_certificate: |
$certificado
  server_address: 192.168.100.3:8443
  cluster_password: "1234"
  cluster_certificate_path: ""
TEST

sudo cat /vagrant/bS1preseed.yaml | lxd init --preseed

echo "Creando contenedores"
sudo lxc launch ubuntu:20.04 web1 --target backServer1

sleep 10

sudo lxc launch ubuntu:20.04 backup1 --target backServer1

sleep 10

echo "instalando apache en el contenedor"
sudo lxc exec web1 -- apt update && apt upgrade -y
sudo lxc exec web1 -- apt install apache2 -y
sudo lxc exec web1 -- systemctl enable apache2
sudo lxc exec backup1 -- apt update && apt upgrade -y
sudo lxc exec backup1 -- apt install apache2 -y
sudo lxc exec backup1 -- systemctl enable apache2

echo "Personalizando sitio web1"
sudo lxc file push /vagrant/web1/index.html web1/var/www/html/index.html

echo "Personalizando sitio backup1"
sudo lxc file push /vagrant/bweb1/index.html backup1/var/www/html/index.html


sudo lxc exec web1 -- systemctl restart apache2
sudo lxc exec backup1 -- systemctl restart apache2