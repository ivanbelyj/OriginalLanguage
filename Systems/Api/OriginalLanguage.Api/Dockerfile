#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/OriginalLanguage.Api/OriginalLanguage.Api.csproj", "Systems/Api/OriginalLanguage.Api/"]
COPY ["Data/Migrations/OriginalLanguage.Context.MigrationsMSSQL/OriginalLanguage.Context.MigrationsMSSQL.csproj", "Data/Migrations/OriginalLanguage.Context.MigrationsMSSQL/"]
COPY ["Data/OriginalLanguage.Context.Entities/OriginalLanguage.Context.Entities.csproj", "Data/OriginalLanguage.Context.Entities/"]
COPY ["Data/OriginalLanguage.Context/OriginalLanguage.Context.csproj", "Data/OriginalLanguage.Context/"]
COPY ["Services/OriginalLanguage.Services.UserAccount/OriginalLanguage.Services.UserAccount.csproj", "Services/OriginalLanguage.Services.UserAccount/"]
COPY ["Shared/OriginalLanguage.Common/OriginalLanguage.Common.csproj", "Shared/OriginalLanguage.Common/"]
COPY ["Shared/OriginalLanguage.Settings/OriginalLanguage.Settings.csproj", "Shared/OriginalLanguage.Settings/"]
COPY ["Data/Migrations/OriginalLanguage.Context.MigrationsPostgreSQL/OriginalLanguage.Context.MigrationsPostgreSQL.csproj", "Data/Migrations/OriginalLanguage.Context.MigrationsPostgreSQL/"]
COPY ["Services/OriginalLanguage.Services.Articles/OriginalLanguage.Services.Articles.csproj", "Services/OriginalLanguage.Services.Articles/"]
COPY ["Services/OriginalLanguage.Services.Cache/OriginalLanguage.Services.Cache.csproj", "Services/OriginalLanguage.Services.Cache/"]
COPY ["Services/OriginalLanguage.Services.Courses/OriginalLanguage.Services.Courses.csproj", "Services/OriginalLanguage.Services.Courses/"]
COPY ["Services/OriginalLanguage.Services.EmailSender/OriginalLanguage.Services.EmailSender.csproj", "Services/OriginalLanguage.Services.EmailSender/"]
COPY ["Services/OriginalLanguage.Services.Languages/OriginalLanguage.Services.Languages.csproj", "Services/OriginalLanguage.Services.Languages/"]
COPY ["Services/OriginalLanguage.Services.LessonProgresses/OriginalLanguage.Services.LessonProgresses.csproj", "Services/OriginalLanguage.Services.LessonProgresses/"]
COPY ["Services/OriginalLanguage.Services.LessonSamples/OriginalLanguage.Services.LessonSamples.csproj", "Services/OriginalLanguage.Services.LessonSamples/"]
COPY ["Services/OriginalLanguage.Services.Lessons/OriginalLanguage.Services.Lessons.csproj", "Services/OriginalLanguage.Services.Lessons/"]
COPY ["Services/OriginalLanguage.Services.RabbitMq/OriginalLanguage.Services.RabbitMq.csproj", "Services/OriginalLanguage.Services.RabbitMq/"]
COPY ["Shared/OriginalLanguage.Consts/OriginalLanguage.Consts.csproj", "Shared/OriginalLanguage.Consts/"]
COPY ["Services/OriginalLanguage.Services.Sentences/OriginalLanguage.Services.Sentences.csproj", "Services/OriginalLanguage.Services.Sentences/"]
COPY ["Services/OriginalLanguage.Services.Settings/OriginalLanguage.Services.Settings.csproj", "Services/OriginalLanguage.Services.Settings/"]
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