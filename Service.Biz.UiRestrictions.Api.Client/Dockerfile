FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY . .
ARG NUGET_URI
RUN dotnet restore Service.Biz.UiRestrictions.Api.Client/Service.Biz.UiRestrictions.Api.Client.csproj -s "${NUGET_URI}"

ARG VERSION
ARG NUGET_SERVER_APIKEY

RUN dotnet build Service.Biz.UiRestrictions.Api.Client/Service.Biz.UiRestrictions.Api.Client.csproj /p:Version=${VERSION} -c Release --no-restore && \
    dotnet pack Service.Biz.UiRestrictions.Api.Client/Service.Biz.UiRestrictions.Api.Client.csproj /p:Version=${VERSION} -c Release --no-build -o /artifacts && \
    dotnet nuget push /artifacts/**/*.nupkg --source "${NUGET_URI}" --api-key "${NUGET_SERVER_APIKEY}"
