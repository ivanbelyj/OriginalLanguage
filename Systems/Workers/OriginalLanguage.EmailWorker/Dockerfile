#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Workers/OriginalLanguage.EmailWorker/OriginalLanguage.EmailWorker.csproj", "Systems/Workers/OriginalLanguage.EmailWorker/"]
COPY ["Services/OriginalLanguage.Services.EmailSender/OriginalLanguage.Services.EmailSender.csproj", "Services/OriginalLanguage.Services.EmailSender/"]
COPY ["Services/OriginalLanguage.Services.RabbitMq/OriginalLanguage.Services.RabbitMq.csproj", "Services/OriginalLanguage.Services.RabbitMq/"]
COPY ["Shared/OriginalLanguage.Consts/OriginalLanguage.Consts.csproj", "Shared/OriginalLanguage.Consts/"]
COPY ["Shared/OriginalLanguage.Settings/OriginalLanguage.Settings.csproj", "Shared/OriginalLanguage.Settings/"]
RUN dotnet restore "Systems/Workers/OriginalLanguage.EmailWorker/OriginalLanguage.EmailWorker.csproj"
COPY . .
WORKDIR "/src/Systems/Workers/OriginalLanguage.EmailWorker"
RUN dotnet build "OriginalLanguage.EmailWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OriginalLanguage.EmailWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OriginalLanguage.EmailWorker.dll"]