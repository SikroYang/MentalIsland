FROM node:16.17.0-alpine

WORKDIR /MentalIsland
COPY ./Vue.Lidao.New/package.json .
# COPY ./Vue.Lidao.New/.yarnrc .
RUN npm i

WORKDIR /MentalIsland
COPY ./Vue.Lidao.New .

ENV NODE_ENV=production
RUN npm run build

EXPOSE 8100
ENTRYPOINT ["npm","run", "start"]