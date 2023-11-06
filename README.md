# PirateQuester

## Running Pirate quester
To run Piratequester on Windows with the .net SDK (7 or higher) installed to develop run
```
dotnet watch
```
To run PirateQuester without the .net SDK installed run the docker container

You do however need docker compose installed
```
docker compose up -d --build
```
If you don't need to rebuild omit the --build flag

With the docker container up you can access the site on localhost:5000
