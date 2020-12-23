#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["NetworkScanner.API.csproj", "./"]
COPY ["../NetworkScanner.SharedKernel/NetworkScanner.SharedKernel.csproj", "../NetworkScanner.SharedKernel/"]
COPY ["../NetworkScanner.Infrastructure/NetworkScanner.Infrastructure.csproj", "../NetworkScanner.Infrastructure/"]
COPY ["../NetworkScanner.Domain/NetworkScanner.Domain.csproj", "../NetworkScanner.Domain/"]
RUN dotnet restore "NetworkScanner.API.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "NetworkScanner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetworkScanner.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetworkScanner.API.dll"]
