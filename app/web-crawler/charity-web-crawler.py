#!/usr/bin/python3

from bs4 import BeautifulSoup
from bs4 import SoupStrainer
import requests, time
from datetime import date, timedelta

BASE_URI = "http://www.charitywatch.org"

def get_charities(article_uri, out_file):
    curr_charities = []
    cause = ""
    soup = str(BeautifulSoup(requests.get(article_uri, headers=user_agent).text, "html.parser").prettify("ascii")).replace('\\n', "")

    if (soup.find("<h2>      Related Charities     </h2>     <table class=\"small\" width=\"100%\">") != -1):
        charities_table = soup.split("<h2>      Related Charities     </h2>     <table class=\"small\" width=\"100%\">")[1].split("</table>")[0].strip(' \t\n\r')
        links_list = charities_table.split("<a href")

        for i in range(1,len(links_list)):
            curr_charities.append(links_list[i].split(">")[1].split("</a")[0].strip(' \t\n\r').replace("\\", "").replace("amp;", ""))

        if (len(curr_charities) > 0):
            out_file.write(str(curr_charities).replace("\'", "\""))

        if (soup.find("<div class=\"blog_title\">       <h2>") != -1):
            cause = soup.split("<div class=\"blog_title\">       <h2>")[1].split("</h2>")[0].strip(' \t\n\r')
            out_file.write(", \"cause\": \"" + cause + "\"")

charities = []
user_agent = {'User-Agent': 'gnu-linux:com.e4r.charity-web-crawler:v0.1'}
soup = str(BeautifulSoup(requests.get(BASE_URI + "/charitywatch-hot-topics", headers=user_agent).text, "html.parser").prettify("ascii"))

old_date_div = 0
this_year = date.today() - timedelta(days=30)

out_file = open('charity_data', 'w')
out_file.write("{\"charities\": ")

date_div = old_date_div + soup[old_date_div:].find("class=\"blog_published\"")
old_date_div = date_div + 10

the_date = soup[date_div:].split(">")[1].replace("\\n", "").replace("</div", "").replace("Posted on", "").strip(' \t\n\r')
post_date = date.fromtimestamp(time.mktime(time.strptime(the_date, "%B %d, %Y")))

if (post_date > this_year):
    readmore_div = date_div + soup[date_div:].find("class=\"button small blue\"")
    article_uri = BASE_URI + soup[readmore_div:].split("href=\"")[1].split("\">")[0].strip(' \t\n\r')

    get_charities(article_uri, out_file)
else:
    out_file.write("[], \"cause\": \"\"")

out_file.write("}")
out_file.close()
