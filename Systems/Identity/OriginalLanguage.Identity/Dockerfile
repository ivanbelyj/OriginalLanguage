#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Identity/OriginalLanguage.Identity/OriginalLanguage.Identity.csproj", "Systems/Identity/OriginalLanguage.Identity/"]
COPY ["Data/OriginalLanguage.Context/OriginalLanguage.Context.csproj", "Data/OriginalLanguage.Context/"]
COPY ["Services/OriginalLanguage.Services.UserAccount/OriginalLanguage.Services.UserAccount.csproj", "Services/OriginalLanguage.Services.UserAccount/"]
COPY ["Data/OriginalLanguage.Context.Entities/OriginalLanguage.Context.Entities.csproj", "Data/OriginalLanguage.Context.Entities/"]
COPY ["Shared/OriginalLanguage.Common/OriginalLanguage.Common.csproj", "Shared/OriginalLanguage.Common/"]
COPY ["Shared/OriginalLanguage.Settings/OriginalLanguage.Settings.csproj", "Shared/OriginalLanguage.Settings/"]
COPY ["Shared/OriginalLanguage.Consts/OriginalLanguage.Consts.csproj", "Shared/OriginalLanguage.Consts/"]
RUN dotnet restore "Systems/Identity/OriginalLanguage.Identity/OriginalLanguage.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/OriginalLanguage.Identity"
RUN dotnet build "OriginalLanguage.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OriginalLanguage.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OriginalLanguage.Identity.dll"]