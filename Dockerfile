FROM node:carbon

RUN mkdir -p /opt/app
WORKDIR /opt/app

COPY app/package*.json /opt/app
RUN npm i

COPY app /opt/app

RUN echo "deb http://ftp.debian.org/debian jessie-backports main" >> /etc/apt/sources.list.d/sources.list
RUN apt-get update
RUN apt-get install -y -t jessie-backports certbot

CMD ["npm", "start"]
