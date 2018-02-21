import React from "react";
import Styles from "./style.css";
import * as Temp from './temporarynews'; 

function sideScroll(element, direction, speed, distance, step) {
	let scrollAmount = 0;
	let slideTimer = setInterval(()=>{
		if (direction == "left") {
			element.scrollLeft -= step;
		}
		else {
			element.scrollLeft += step;
		}
		scrollAmount += step;
		if(scrollAmount >= distance) {
			window.clearInterval(slideTimer);
		}
	}, speed);
}

function scroll(left) {
	const latest = document.getElementsByClassName(Styles.latestmodules)[0];
	const distance = Math.floor(latest.clientWidth);
	if (left) {
		sideScroll(latest, "left", 10, distance, 10);
	}
	else {
		sideScroll(latest, "right", 10, distance, 10);
	}
}

const News = props => (
	<div className={Styles.newspad}>
	    <div className={Styles.news}>
			<p className={Styles.description}>{props.description}</p>
			<div className={Styles.newscontent}>
				<img className={Styles.newsimg} src={props.img} alt="X"/>
				<div className={Styles.origin}>
					<h4>American Red Cross</h4>
					<span><a href="http://www.redcross.org" target="_blank">+ Follow</a></span>
				</div>
			</div>
		</div>
	</div>
);

const LatestNews = props => (
	<div className={Styles.latestnews}>
		<h1>Recent News</h1>
		<div className={Styles.newscontainer}>
			<i className="fa fa-arrow-circle-left" aria-hidden="true" onClick={scroll.bind(null,true)}></i>
			<div className={Styles.latestmodules}>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
                <News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
				<News img={Temp.NEWS_IMG} description={Temp.NEWS_DESCRIPTION}/>
			</div>
			<i className="fa fa-arrow-circle-right" aria-hidden="true" onClick={scroll.bind(null, false)}></i>
		</div>
	</div>
);

export default LatestNews;
