FROM centos
MAINTAINER Juan David Pernett
RUN sed -i 's/mirrorlist/#mirrorlist/g' /etc/yum.repos.d/CentOS-Linux-* &&\
    sed -i 's|#baseurl=http://mirror.centos.org|baseurl=http://vault.centos.org|g' /etc/yum.repos.d/CentOS-Linux-*
RUN yum upgrade -y
RUN yum install vsftpd -y
RUN ["/bin/bash", "-c", "useradd -m usuario"]
RUN echo "usuario:1234" | chpasswd
EXPOSE 21
CMD ["/usr/sbin/vsftpd","-obackground=NO"]