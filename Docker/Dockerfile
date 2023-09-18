FROM node:12-alpine

RUN mkdir -p /Docker

WORKDIR /Docker

COPY package*.json /Docker

RUN npm install

COPY . /Docker

EXPOSE 8080

CMD [ "node", "server.js" ]
