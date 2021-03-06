#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/SprintActive/SprintActive.API/SprintActive.API.csproj", "src/SprintActive/SprintActive.API/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
RUN dotnet restore "src/SprintActive/SprintActive.API/SprintActive.API.csproj"
COPY . .
WORKDIR "/src/src/SprintActive/SprintActive.API"
RUN dotnet build "SprintActive.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SprintActive.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SprintActive.API.dll"]