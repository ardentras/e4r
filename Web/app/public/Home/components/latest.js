import React from "react";
import Styles from "./style.css";

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
	const distance = Math.floor((latest.scrollWidth - latest.clientWidth) / 1);
	if (left) {
		sideScroll(latest, "left", 20, distance, 60);
	}
	else {
		sideScroll(latest, "right", 20, distance, 60);
	}
}

const latestNews = props => {
	return (
		<div>
			<div className={Styles.latest}>
				<h1>Recent news</h1>
				<div className={Styles.latestcontainer}>
					<i className="fa fa-arrow-circle-left" aria-hidden="true" onClick={scroll.bind(null,true)}></i>
					<div className={Styles.latestmodules}>
						<div className={Styles.latestmodule} id={Styles.e1}>
							<img src="/static/kids.jpg" alt="event2" width='270px' height='184x'/>
							<p>United way is asking
								everyone to donate
								their unwanted foods
								to the poor.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="/static/rcevent.jpg" alt="event2" width='270px' height='184x'/>
							<p>American Red Cross
								is inviting everyone
								to attend their blood
								giving fair.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="/static/redcross.jpg" alt="event2" width='270px' height='184x'/>
							<p>Direct Relief Disaster 
								Fair to raise funds for
								the recent hurricane
								attack.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="/static/jacky.jpg" alt="event2" width='270px' height='184x'/>
							<p>This is Jacky Chan
								because Jacky Chan.
								because he kicks butt.
								Don’t question this.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="/static/hope.jpg" alt="event2" width='270px' height='184x'/>
							<p>Salving has no season.
								Donate your money
								now to people
								who needs it.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="/static/better.png" alt="event2" width='270px' height='184x'/>
							<p>Direct Relief Disaster 
								Fair to raise funds for
								the recent hurricane
								attack.</p>
							<span className='h-learn-btn' >LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="/static/cat.ico" alt="event2" width='270px' height='184x'/>
							<p>Direct Relief Disaster 
								Fair to raise funds for
								the recent hurricane
								attack.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
					</div>
					<i className="fa fa-arrow-circle-right" aria-hidden="true" onClick={scroll.bind(null, false)}></i>
				</div>
			</div>
		</div>
	);
};

export default latestNews;
