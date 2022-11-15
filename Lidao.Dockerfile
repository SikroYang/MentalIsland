FROM node:16.17.0-alpine
RUN sed -i "s/dl-cdn.alpinelinux.org/mirrors.ustc.edu.cn/g" /etc/apk/repositories \
    && apk add --no-cache make gcc g++ python3

WORKDIR /MentalIsland
COPY ./Vue.Lidao .

ENV NODE_ENV=production
RUN yarn && yarn build

EXPOSE 8000
ENTRYPOINT ["yarn", "start"]