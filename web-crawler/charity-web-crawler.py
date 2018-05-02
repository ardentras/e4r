#!/usr/bin/python3

from bs4 import BeautifulSoup
from bs4 import SoupStrainer
import requests, time
from datetime import date, timedelta

BASE_URI = "http://www.charitywatch.org"

user_agent = {'User-Agent': 'gnu-linux:com.e4r.charity-web-crawler:v0.1'}
soup = str(BeautifulSoup(requests.get(BASE_URI + "/charitywatch-hot-topics", headers=user_agent).text,
                                        "html.parser").prettify("ascii"))

old_date_div = 0
post_date = date.today()
this_year = date.today() - timedelta(days=365)
print(this_year)
while (post_date > this_year):
    date_div = old_date_div + soup[old_date_div:].find("class=\"blog_published\"")
    old_date_div = date_div + 10

    the_date = soup[date_div:].split(">")[1].replace("\\n", "").replace("</div", "").replace("Posted on", "").strip(' \t\n\r')
    post_date = time.strptime(the_date, "%B %d, %Y")

    print(date)
    readmore_div = date_div + soup[date_div:].find("class=\"button small blue\"")
    article_uri = BASE_URI + soup[readmore_div:].split("href=\"")[1].split("\">")[0].strip(' \t\n\r')
