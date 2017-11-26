FROM node:carbon

RUN mkdir -p /opt/app
WORKDIR /opt/app

COPY app/package*.json /opt/app
RUN npm i

COPY app /opt/app

CMD ["npm", "start"]
