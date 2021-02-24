FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RefArk.Customer.csproj", "./"]

RUN dotnet restore "./RefArk.Customer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RefArk.Customer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RefArk.Customer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RefArk.Customer.dll"]