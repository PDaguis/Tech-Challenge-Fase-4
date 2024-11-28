FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fase4.Contato.Consumer.Cadastrar/Fase4.Contato.Consumer.Cadastrar.csproj", "Fase4.Contato.Consumer.Cadastrar/"]
COPY ["Fase4.Contato.Infra/Fase4.Contato.Infra.csproj", "Fase4.Contato.Infra/"]
COPY ["Fase4.Contato.Shared/Fase4.Contato.Shared.csproj", "Fase4.Contato.Shared/"]
COPY ["Fase4.Contato.Core/Fase4.Contato.Core.csproj", "Fase4.Contato.Core/"]
RUN dotnet restore "Fase4.Contato.Consumer.Cadastrar/Fase4.Contato.Consumer.Cadastrar.csproj"
COPY . .
WORKDIR "/src/Fase4.Contato.Consumer.Cadastrar"
RUN dotnet build "Fase4.Contato.Consumer.Cadastrar.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fase4.Contato.Consumer.Cadastrar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fase4.Contato.Consumer.Cadastrar.dll"]
