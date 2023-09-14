#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/OriginalLanguage.Api/OriginalLanguage.Api.csproj", "Systems/Api/OriginalLanguage.Api/"]
COPY ["Services/OriginalLanguage.Services.Settings/OriginalLanguage.Services.Settings.csproj", "Services/OriginalLanguage.Services.Settings/"]
COPY ["Shared/OriginalLanguage.Common/OriginalLanguage.Common.csproj", "Shared/OriginalLanguage.Common/"]
COPY ["Shared/OriginalLanguage.Settings/OriginalLanguage.Settings.csproj", "Shared/OriginalLanguage.Settings/"]
COPY ["Systems/Api/OriginalLanguage.Api.Settings/OriginalLanguage.Api.Settings.csproj", "Systems/Api/OriginalLanguage.Api.Settings/"]
RUN dotnet restore "Systems/Api/OriginalLanguage.Api/OriginalLanguage.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/OriginalLanguage.Api"
RUN dotnet build "OriginalLanguage.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OriginalLanguage.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OriginalLanguage.Api.dll"]