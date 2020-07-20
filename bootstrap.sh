#!/bin/sh

# Change the hosts file to include the full domain
echo "127.0.0.1 ubuntu-container.pcustancegmail.onmicrosoft.com ubuntu-container localhost" > ./temp_hosts
cat /etc/hosts | tail -n +2 >> ./temp_hosts
cat ./temp_hosts > /etc/hosts
rm ./temp_hosts

# Set correct timezone 
ln -fs /usr/share/zoneinfo/Europe/London /etc/localtime
dpkg-reconfigure --frontend noninteractive tzdata

# Set default realm and disable rdns in krb5.conf
echo "[libdefaults]" > ./temp_krb5
echo "        default_realm = PCUSTANCEGMAIL.ONMICROSOFT.COM" >> ./temp_krb5
echo "        rdns=false" >> ./temp_krb5
cat /etc/krb5.conf | tail -n +3 >> ./temp_krb5
cat ./temp_krb5 > /etc/krb5.conf

# Join the container to the domain
echo Twez23fa! | kinit msmith@PCUSTANCEGMAIL.ONMICROSOFT.COM
echo Twez23fa! | realm join --verbose PCUSTANCEGMAIL.ONMICROSOFT.COM -U 'msmith@PCUSTANCEGMAIL.ONMICROSOFT.COM' --install=/

exec "$@"