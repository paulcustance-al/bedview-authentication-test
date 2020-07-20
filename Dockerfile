FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.4-bionic AS base
RUN export DEBIAN_FRONTEND=noninteractive && \
    apt-get update && \
    apt-get install krb5-user samba sssd \
                    sssd-tools libnss-sss \
                    libpam-sss ntp ntpdate realmd adcli -y
COPY ./bootstrap.sh /bootstrap.sh
RUN chmod +x /bootstrap.sh
CMD ["/bootstrap.sh"]
