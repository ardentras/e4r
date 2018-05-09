#!/usr/bin/python3

from bs4 import BeautifulSoup
from bs4 import SoupStrainer
import requests, time
from datetime import date, timedelta

BASE_URI = "http://www.charitywatch.org"

def get_charities(article_uri):
    curr_charities = []
    soup = str(BeautifulSoup(requests.get(article_uri, headers=user_agent).text, "html.parser").prettify("ascii")).replace('\\n', "")

    if (soup.find("<h2>      Related Charities     </h2>     <table class=\"small\" width=\"100%\">") != -1):
        charities_table = soup.split("<h2>      Related Charities     </h2>     <table class=\"small\" width=\"100%\">")[1].split("</table>")[0].strip(' \t\n\r')
        links_list = charities_table.split("<a href")

        for i in range(1,len(links_list)):
            curr_charities.append(links_list[i].split(">")[1].split("</a")[0].strip(' \t\n\r').replace("\\", "").replace("amp;", ""))

    return curr_charities

charities = []
user_agent = {'User-Agent': 'gnu-linux:com.e4r.charity-web-crawler:v0.1'}
soup = str(BeautifulSoup(requests.get(BASE_URI + "/charitywatch-hot-topics", headers=user_agent).text, "html.parser").prettify("ascii"))

old_date_div = 0
post_date = date.today()
this_year = date.today() - timedelta(days=365)

while (post_date > this_year):
    date_div = old_date_div + soup[old_date_div:].find("class=\"blog_published\"")
    old_date_div = date_div + 10

    the_date = soup[date_div:].split(">")[1].replace("\\n", "").replace("</div", "").replace("Posted on", "").strip(' \t\n\r')
    post_date = date.fromtimestamp(time.mktime(time.strptime(the_date, "%B %d, %Y")))

    readmore_div = date_div + soup[date_div:].find("class=\"button small blue\"")
    article_uri = BASE_URI + soup[readmore_div:].split("href=\"")[1].split("\">")[0].strip(' \t\n\r')

    charities = get_charities(article_uri)

    if (len(charities) > 0):
        print(charities)
    else:
        print(0)
