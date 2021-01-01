#!/bin/sh

if [ -z "$(which dotnet)" ]; then
	echo "Error! Cannot find dotnet!"
	exit -1
fi

if [
	! -d "./TylerMart.Client/" ||
	! -d "./TylerMart.Client/" ||
	! -d "./TylerMart.Client/" ||
	! -d "./TylerMart.Client/" ||
	! -f "./TylerMart.sln"
]; then
	echo "Error! This script is not being run from the project directory!"
	exit -1
fi

rm -rf "TylerMart.Client/bin/" "TylerMart.Client/obj/"
rm -rf "TylerMart.Domain/bin/" "TylerMart.Domain/obj/"
rm -rf "TylerMart.Storage/bin/" "TylerMart.Storage/obj/"
rm -rf "TylerMart.UnitTest/bin/" "TylerMart.UnitTest/obj/"
