FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.4-bionic AS base
WORKDIR /app
COPY --from=build-env /app/out .
COPY ./ubuntu-server-2.HTTP.keytab /app/ubuntu-server-2.HTTP.keytab
RUN export DEBIAN_FRONTEND=noninteractive && \
    apt-get update && \
    apt-get install krb5-user samba sssd \
                    sssd-tools libnss-sss \
                    libpam-sss ntp ntpdate realmd adcli -y
COPY ./bootstrap.sh ./bootstrap.sh
RUN chmod +x ./bootstrap.sh
ENTRYPOINT ["./bootstrap.sh", "dotnet", "test-windows-authentication.dll"]
