FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY BookingServiceBackend.csproj ./
RUN dotnet restore BookingServiceBackend.csproj

COPY . ./
RUN dotnet publish BookingServiceBackend.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "BookingServiceBackend.dll"]
