#!/bin/sh

if [ -z "$(which docker)" ]; then
	echo "Error! Cannot find docker!"
	exit -1
fi

docker container run -dit --rm --name tylermart-database -p 1433:1433 -e 'ACCEPT_EULA=y' -e 'SA_PASSWORD=Password12345' mcr.microsoft.com/mssql/server:latest
