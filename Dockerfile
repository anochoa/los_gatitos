FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . ./
# RUN dotnet restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build /app .

EXPOSE 80 5195 7066

ENV ASPNETCORE_URLS=http://+:5195;https://+:7066

ENTRYPOINT ["dotnet", "DevFreela.API.dll"]