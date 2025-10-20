FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY Shared/Shared.csproj Shared/
COPY ExpenseTracker.Persistence/ExpenseTracker.Persistence.csproj ExpenseTracker.Persistence/
COPY ExpenseTracker.Domain/ExpenseTracker.Domain.csproj ExpenseTracker.Domain/
COPY ExpenseTracker.Application/ExpenseTracker.Application.csproj ExpenseTracker.Application/
COPY ExpenseTracker.Web/ExpenseTracker.Web.csproj ExpenseTracker.Web/
RUN dotnet restore ExpenseTracker.Web/ExpenseTracker.Web.csproj 
COPY . .
RUN dotnet publish ExpenseTracker.Web/ExpenseTracker.Web.csproj -c $BUILD_CONFIGURATION -o /publish /p:UseAppHost=false

FROM base AS final
COPY --from=build /publish ./publish
ENTRYPOINT ["dotnet", "publish/ExpenseTracker.Web.dll"]