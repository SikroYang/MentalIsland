FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /MentalIsland

COPY ["MentalIsland.AllDependency/","MentalIsland.AllDependency/"]
COPY ["MentalIsland.Core/","MentalIsland.Core/"]
COPY ["MentalIsland.Migrations/","MentalIsland.Migrations/"]
COPY ["MentalIsland.Web/","MentalIsland.Web/"]
WORKDIR /MentalIsland/MentalIsland.Web
RUN dotnet publish -c Release -o publish/

FROM node:16.17.0-alpine as build
WORKDIR /MentalIsland

COPY ./Vue.BackEnd/package.json .
COPY ./Vue.BackEnd/.yarnrc .
RUN yarn

COPY ./Vue.BackEnd .
RUN yarn build

FROM base AS final
WORKDIR /MentalIsland
EXPOSE 5775

ENV ASPNETCORE_URLS=http://0.0.0.0:5775
ENV TZ=Asia/Shanghai

COPY --from=publish /MentalIsland/MentalIsland.Web/publish .
COPY --from=build /MentalIsland/BackEnd ./wwwroot/BackEnd
RUN cp -rf /usr/share/zoneinfo/${TZ} /etc/localtime && echo "${TZ}" > /etc/timezone
ENTRYPOINT ["dotnet", "MentalIsland.Web.dll"]