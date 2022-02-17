#!/bin/bash
systemctl start apache2
curl 127.0.0.1 > /vagrant/resultado.txt