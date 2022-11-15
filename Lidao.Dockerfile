FROM node:16.17.0-alpine

WORKDIR /MentalIsland
COPY ./Vue.Lidao/package.json .
COPY ./Vue.Lidao/.yarnrc .
RUN yarn

WORKDIR /MentalIsland
COPY ./Vue.Lidao .

ENV NODE_ENV=production
RUN yarn build

EXPOSE 8000
ENTRYPOINT ["yarn", "start"]