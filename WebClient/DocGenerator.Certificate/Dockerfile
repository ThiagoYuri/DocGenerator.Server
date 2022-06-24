#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# installs NodeJS and NPM
WORKDIR /app
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash - && apt-get install -yq nodejs build-essential



FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DocGenerator.Certificate/DocGenerator.Certificate.csproj", "DocGenerator.Certificate/"]
RUN dotnet restore "DocGenerator.Certificate/DocGenerator.Certificate.csproj"
COPY . .
WORKDIR "/src/DocGenerator.Certificate"
RUN dotnet build "DocGenerator.Certificate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocGenerator.Certificate.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocGenerator.Certificate.dll"]