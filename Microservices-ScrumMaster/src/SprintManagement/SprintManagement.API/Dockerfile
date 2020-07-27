#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/SprintManagement/SprintManagement.API/SprintManagement.API.csproj", "src/SprintManagement/SprintManagement.API/"]
COPY ["src/SprintManagement/SprintManagement.Infrastructure/SprintManagement.Infrastructure.csproj", "src/SprintManagement/SprintManagement.Infrastructure/"]
COPY ["src/SprintManagement/SprintManagement.Core/SprintManagement.Core.csproj", "src/SprintManagement/SprintManagement.Core/"]
COPY ["src/SprintManagement/SprintManagement.Application/SprintManagement.Application.csproj", "src/SprintManagement/SprintManagement.Application/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
RUN dotnet restore "src/SprintManagement/SprintManagement.API/SprintManagement.API.csproj"
COPY . .
WORKDIR "/src/src/SprintManagement/SprintManagement.API"
RUN dotnet build "SprintManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SprintManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SprintManagement.API.dll"]
