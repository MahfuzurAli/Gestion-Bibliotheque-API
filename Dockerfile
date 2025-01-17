# Utiliser l'image de base officielle de .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Définir le répertoire de travail
WORKDIR /src

# Copier le fichier csproj et restaurer les dépendances
COPY ["./GestionBibliothequeAPI.csproj", "./"]
RUN dotnet restore "./GestionBibliothequeAPI.csproj"

# Copier le reste des fichiers de l'application et publier l'application
COPY . .
WORKDIR "/src/."
RUN dotnet build "GestionBibliothequeAPI.csproj" -c Release -o /app/build
RUN dotnet publish "GestionBibliothequeAPI.csproj" -c Release -o /app/publish

# Générer l'image de l'application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .

# Commande de lancement de l'application
ENTRYPOINT ["dotnet", "GestionBibliothequeAPI.dll"]