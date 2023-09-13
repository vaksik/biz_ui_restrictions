ARG DOCKERREGISTRY

FROM ${DOCKERREGISTRY}/dotnet/sdk:6.0 AS publish
ARG PRIVATENUGET
COPY . .
RUN dotnet restore Service.Biz.UiRestrictions.sln -r linux-x64 -s ${PRIVATENUGET}
RUN dotnet test Service.Biz.UiRestrictions.sln -c Release --no-restore
RUN dotnet publish Service.Biz.UiRestrictions.Host/Service.Biz.UiRestrictions.Host.csproj -f net6.0 -r linux-x64 -c Release -o /app --no-restore /p:ShowLinkerSizeComparison=true

ARG DOCKERREGISTRY
FROM ${DOCKERREGISTRY}/dotnet/aspnet-runtime-system-drawing:6.0 as runtime

ARG GQL_SCHEMA_VERSION=unspecified
ENV GQL_SCHEMA_VERSION=$GQL_SCHEMA_VERSION

WORKDIR /app
EXPOSE 9099/tcp
COPY --from=publish /app ./

ENTRYPOINT ["./Service.Biz.UiRestrictions.Host"]
