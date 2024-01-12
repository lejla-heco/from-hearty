FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /my-backend

COPY . ./
RUN dotnet restore
RUN dotnet publish -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /my-backend
COPY --from=build-env /my-backend/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebAPI.dll"]