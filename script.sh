#!/bin/bash

echo "configurando el resolv.conf con cat"
cat <<TEST> /etc/resolv.conf
nameserver 8.8.8.8
TEST

echo "instalando un servidor vsftpd"
sudo apt-get install vsftpd -y

echo “Modificando vsftpd.conf con sed”
sed -i 's/#write_enable=YES/write_enable=YES/g' /etc/vsftpd.conf

echo “Creando un nuevo usuario”
sudo useradd usuario1
echo "usuario1:1234" | chpasswd

echo “Cambiando mensaje de bienvenida”
sed -i 's/#ftpd_banner=Welcome to blahar FTP service./ftpd_banner=Bienvenido/g' >> /etc/vsftpd.conf

echo “Restringiendo acceso a anonimos”
sed -i 's/anonymous_enable=YES/anonymous_enable=NO/g' /etc/vsftpd.conf

echo "configurando ip forwarding con echo"
sudo echo "net.ipv4.ip_forward = 1" >> /etc/sysctl.conf