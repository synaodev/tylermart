#!/bin/sh

if [ -z "$(which docker)" ]; then
	echo "Error! Can't find docker!"
	exit -1
fi

docker container run -dit --rm --name tylermart -p 1433:1433 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password12345' mcr.microsoft.com/mssql/server:latest
