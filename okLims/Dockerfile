FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS build
WORKDIR /src
COPY ["okLims/okLims.csproj", "okLims/"]

RUN dotnet restore "okLims/okLims.csproj"
COPY . .
WORKDIR "/src/okLims"
RUN dotnet build "okLims.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "okLims.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "okLims.dll"]