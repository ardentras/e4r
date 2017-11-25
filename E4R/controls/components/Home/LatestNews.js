import React from 'react';

export default function LatestNews(props) {
    return (
        <div className="latest">
        <h1 id="latest-title"><span>LATEST</span>News</h1>
        <div className="latest-news">
            <div className="news box1">
                <h1 className="news-content">Hurrican Kevin</h1>
                <h5 className="news-content">Klamath Falls,OR</h5>
                <p className="news-content">
                    A recent strike from a horrible whining hurricane has hit 
                    whatever blah blah blah blah but thats not blah blah…
                </p>
                <span>LEARN MORE</span>
            </div >
            <div className="news box2">
                <h1 className="news-content">Tsunami Sushi</h1>
                <h5 className="news-content">Los Angeles,CA</h5>
                <p className="news-content">
                    There are a recent sushi outbreak in which causing people to eat
                    extra and that is causing fishes to die too much… 
                </p>
                <span>LEARN MORE</span>
            </div >
            <div className="news box3">
               <h1 className="news-content">Overly Toasted Bread</h1>
               <h5 className="news-content">Klamath Falls,OR</h5>
                <p className="news-content">
                    My bread has been burnt during toasting of this morning,
                    we shall not allow my bread to be overly toasted…
                </p>
                <span>LEARN MORE</span>
            </div >
            <div className="news box4">
                <h1 className="news-content">Cold Weather</h1>
                <h5 className="news-content">Klamath Falls,GLOBAL</h5>
                <p className="news-content">
                    Since winter is coming, we are experiencing a rapid decrease
                    in temperature and that is making me stay home…
                </p>
                <span>LEARN MORE</span>
            </div >
        </div>
    </div>
    );
}