FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/ProjectTaskManagement.API/ProjectTaskManagement.API.csproj", "src/ProjectTaskManagement.API/"]
COPY ["src/ProjectTaskManagement.Application/ProjectTaskManagement.Application.csproj", "src/ProjectTaskManagement.Application/"]
COPY ["src/ProjectTaskManagement.Infrastructure/ProjectTaskManagement.Infrastructure.csproj", "src/ProjectTaskManagement.Infrastructure/"]
COPY ["src/ProjectTaskManagement.Domain/ProjectTaskManagement.Domain.csproj", "src/ProjectTaskManagement.Domain/"]

RUN dotnet restore "src/ProjectTaskManagement.API/ProjectTaskManagement.API.csproj"

COPY . .
WORKDIR "/src/src/ProjectTaskManagement.API"
RUN dotnet publish "ProjectTaskManagement.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "ProjectTaskManagement.API.dll"]
