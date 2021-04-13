# TylerMart (Revature Project 1)

To setup database:
- Either run `docker-compose up -d` or `database-init.sh`.
- Wait for a few seconds for the database to initialize.
- Update the database by running `dotnet-ef database update -p TylerMart.Storage/ -s TylerMart.Client/`.

After that, run `dotnet run -p TylerMart.Client/` and have fun.
