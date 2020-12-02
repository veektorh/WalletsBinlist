FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["DemoApp/DemoApp.csproj", "DemoApp/"]

RUN dotnet restore "DemoApp/DemoApp.csproj"
COPY DemoApp/ DemoApp/
WORKDIR "/src/DemoApp"
RUN dotnet build "DemoApp.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR /src
COPY ["DemoAppTest/DemoAppTest.csproj", "DemoAppTest/"]
COPY DemoAppTest/ DemoAppTest/
WORKDIR "/src/DemoAppTest"
RUN dotnet test

FROM build AS publish
WORKDIR "/src/DemoApp"
RUN dotnet publish "DemoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoApp.dll"]