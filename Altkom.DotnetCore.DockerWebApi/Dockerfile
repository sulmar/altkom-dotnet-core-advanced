#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Altkom.DotnetCore.DockerWebApi/Altkom.DotnetCore.DockerWebApi.csproj", "Altkom.DotnetCore.DockerWebApi/"]
RUN dotnet restore "Altkom.DotnetCore.DockerWebApi/Altkom.DotnetCore.DockerWebApi.csproj"
COPY . .
WORKDIR "/src/Altkom.DotnetCore.DockerWebApi"
RUN dotnet build "Altkom.DotnetCore.DockerWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Altkom.DotnetCore.DockerWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Altkom.DotnetCore.DockerWebApi.dll"]