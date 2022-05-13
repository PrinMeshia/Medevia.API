#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Medevia.API.UI/Medevia.API.UI.csproj", "Medevia.API.UI/"]
COPY ["Medevia.Core.Infrastructure/Medevia.Core.Infrastructure.csproj", "Medevia.Core.Infrastructure/"]
COPY ["Medevia.Core.Domain/Medevia.Core.Domain.csproj", "Medevia.Core.Domain/"]
COPY ["Medevia.Core.Framework/Medevia.Core.Framework.csproj", "Medevia.Core.Framework/"]
COPY ["Medevia.Core.Migration/Medevia.Core.Migrations.csproj", "Medevia.Core.Migration/"]
RUN dotnet restore "Medevia.API.UI/Medevia.API.UI.csproj"
COPY . .
WORKDIR "/src/Medevia.API.UI"
RUN dotnet build "Medevia.API.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medevia.API.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medevia.API.UI.dll","--environment=Staging"]
