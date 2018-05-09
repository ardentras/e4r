#!/bin/bash

echo "getting charity data"
./charity-web-crawler.py
echo "sending to server"

curl -XPUT http://34.216.143.255:3002/api/try_activate_live_feed -H 'Content-Type: application/json' -d "$(cat charity_data)"
