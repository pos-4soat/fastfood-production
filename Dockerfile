FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_HTTP_PORTS=80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "src/fastfood-production.API/fastfood-production.API.csproj"
RUN dotnet build "src/fastfood-production.API/fastfood-production.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/fastfood-production.API/fastfood-production.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "fastfood-production.API.dll"]