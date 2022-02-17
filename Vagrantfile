# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|

    config.vm.define :clienteUbuntu do |clienteUbuntu|
        clienteUbuntu.vm.box = "bento/ubuntu-20.04"
        clienteUbuntu.vm.network :private_network, ip: "192.168.100.2"
        clienteUbuntu.vm.provision "shell", path: "client.sh"
        clienteUbuntu.vm.hostname = "clienteUbuntu"
    end
    config.vm.define :servidorUbuntu do |servidorUbuntu|
        servidorUbuntu.vm.box = "bento/ubuntu-20.04"
        servidorUbuntu.vm.network :private_network, ip: "192.168.100.3"
        config.vm.provision "shell", path: "script.sh"
        servidorUbuntu.vm.hostname = "servidorUbuntu"
    end
end