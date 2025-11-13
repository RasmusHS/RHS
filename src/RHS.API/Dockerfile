FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5099:80
EXPOSE 7258:443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RHS.API/RHS.API.csproj", "src/RHS.API/"]
COPY ["src/RHS.Application/RHS.Application.csproj", "src/RHS.Application/"]
COPY ["src/RHS.Domain/RHS.Domain.csproj", "src/RHS.Domain/"]
COPY ["src/RHS.Infrastructure/RHS.Infrastructure.csproj", "src/RHS.Infrastructure/"]
COPY ["src/RHS.Persistence/RHS.Persistence.csproj", "src/RHS.Persistence/"]
RUN dotnet restore "src/RHS.API/RHS.API.csproj"
COPY . .
WORKDIR "/src/src/RHS.API"
RUN dotnet build "./RHS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RHS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RHS.API.dll"]
