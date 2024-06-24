FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./", "api/"]
RUN dotnet restore "api/Stereograph.TechnicalTest.Api.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "Stereograph.TechnicalTest.Api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Stereograph.TechnicalTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Stereograph.TechnicalTest.Api.dll" ]