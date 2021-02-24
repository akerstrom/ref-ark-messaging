FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RefArk.Product.csproj", "./"]

RUN dotnet restore "./RefArk.Product.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RefArk.Product.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RefArk.Product.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RefArk.Product.dll"]