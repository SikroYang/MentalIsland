FROM node:16.17.0-alpine
RUN sed -i "s/dl-cdn.alpinelinux.org/mirrors.ustc.edu.cn/g" /etc/apk/repositories \
    && apk add --no-cache make gcc g++ python3

WORKDIR /MentalIsland
COPY ./Vue.Lidao .
RUN yarn

EXPOSE 8000
ENTRYPOINT ["yarn", "dev"]