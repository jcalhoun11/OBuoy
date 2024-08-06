FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
COPY ./OBuoy ./OBuoy

RUN dotnet restore ./OBuoy/OBuoy.sln 
RUN dotnet build ./OBuoy/OBuoy.csproj -c Release
RUN dotnet publish ./OBuoy/OBuoy.csproj -c Release -o /app/out --framework net8.0 --runtime linux-x64 --no-restore --self-contained

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
COPY --from=build /app/out .

EXPOSE 8000
ENV ASPNETCORE_URLS=http://*:8000
ENTRYPOINT ["dotnet", "OBuoy.dll"]