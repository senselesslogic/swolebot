# swolebot

## discordclient

### Building the discord client docker image

In the root directory,

```docker image build -t discordclient -f Dockerfile.discord .```

### Running the discord client

#### Docker

```docker container run --rm -it --env SWOLEBOT_DISCORDTOKEN=<discordtoken> discordclient```

#### Locally

You'll need to setup the environment variables to connect to discord properly.

```export SWOLEBOT_DISCORDTOKEN=<discordtoken>```

If you want to use a `.env` file, here is a one liner to setup environment variables.

```export $(cat .env | xargs)```

Then you can just run the application with `dotnet run`.
