# Etapa 1: Build do Angular
FROM node:22.17 AS client-builder
WORKDIR /app
COPY client/ ./client/
WORKDIR /app/client
RUN npm install
RUN npm run build --prod

# Etapa 2: Build do ASP.NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia tudo (ajuste se necessário)
COPY . .

# Restaura e publica a API
RUN dotnet restore
RUN dotnet publish ./API/API.csproj -c Release -o /app/publish

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia o app ASP.NET publicado
COPY --from=build /app/publish .

# Copia o build do Angular para wwwroot
COPY --from=client-builder /app/client app/API/wwwroot

ENTRYPOINT ["dotnet", "API.dll"]