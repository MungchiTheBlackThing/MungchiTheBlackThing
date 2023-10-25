
FROM node:alpine

RUN mkdir -p /Docker

WORKDIR /Docker

COPY package*.json /Docker

RUN npm install

COPY . /Docker

EXPOSE 8080

CMD [ "npm","run","start" ]
