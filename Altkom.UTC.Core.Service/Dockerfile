FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Altkom.UTC.Core.Service/Altkom.UTC.Core.Service.csproj", "Altkom.UTC.Core.Service/"]
COPY ["Altkom.UTC.Core.FakeServices/Altkom.UTC.Core.FakeServices.csproj", "Altkom.UTC.Core.FakeServices/"]
COPY ["Altkom.UTC.Core.IServices/Altkom.UTC.Core.IServices.csproj", "Altkom.UTC.Core.IServices/"]
COPY ["Altkom.UTC.Core.Models/Altkom.UTC.Core.Models.csproj", "Altkom.UTC.Core.Models/"]
RUN dotnet restore "Altkom.UTC.Core.Service/Altkom.UTC.Core.Service.csproj"
COPY . .
WORKDIR "/src/Altkom.UTC.Core.Service"
RUN dotnet build "Altkom.UTC.Core.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Altkom.UTC.Core.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Altkom.UTC.Core.Service.dll"]