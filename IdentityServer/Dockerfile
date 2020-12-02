FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ids4.csproj", "ids4/"]

RUN dotnet restore "ids4/ids4.csproj"
COPY . ids4/
WORKDIR "/src/ids4"
RUN dotnet build "ids4.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/ids4"
RUN dotnet publish "ids4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ids4.dll"]