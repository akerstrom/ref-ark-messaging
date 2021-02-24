FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RefArk.Authentication.csproj", "./"]

RUN dotnet restore "./RefArk.Authentication.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RefArk.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RefArk.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RefArk.Authentication.dll"]