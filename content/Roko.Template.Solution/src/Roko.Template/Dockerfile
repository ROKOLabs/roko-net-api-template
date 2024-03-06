FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Roko.Template/Roko.Template.csproj", "src/Roko.Template/"]
COPY ["src/Application/Roko.Template.Application/Roko.Template.Application.csproj", "src/Application/Roko.Template.Application/"]
COPY ["src/Blocks/Roko.Template.Blocks.Common.Exceptions/Roko.Template.Blocks.Common.Exceptions.csproj", "src/Blocks/Roko.Template.Blocks.Common.Exceptions/"]
COPY ["src/Blocks/Roko.Template.Blocks.Common.Kernel/Roko.Template.Blocks.Common.Kernel.csproj", "src/Blocks/Roko.Template.Blocks.Common.Kernel/"]
COPY ["src/Application/Roko.Template.Application.Contracts/Roko.Template.Application.Contracts.csproj", "src/Application/Roko.Template.Application.Contracts/"]
COPY ["src/Domain/Roko.Template.Domain/Roko.Template.Domain.csproj", "src/Domain/Roko.Template.Domain/"]
COPY ["src/Infrastructure/Roko.Template.Infrastructure.Db.MyDb/Roko.Template.Infrastructure.Db.MyDb.csproj", "src/Infrastructure/Roko.Template.Infrastructure.Db.MyDb/"]
COPY ["src/Presentation/Roko.Template.Presentation.Api/Roko.Template.Presentation.Api.csproj", "src/Presentation/Roko.Template.Presentation.Api/"]
RUN dotnet restore "src/Roko.Template/Roko.Template.csproj"
COPY . .
WORKDIR "/src/src/Roko.Template"
RUN dotnet build "Roko.Template.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Roko.Template.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roko.Template.dll"]
