FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RefArk.Station.csproj", "./"]

RUN dotnet restore "./RefArk.Station.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RefArk.Station.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RefArk.Station.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RefArk.Station.dll"]