import React from "react";
import Styles from "./style.css";

function scroll(left) {
	const latest = document.getElementsByClassName(Styles.latestmodules)[0];
	if (left) {
		latest.scrollLeft -= 350;
	}
	else {
		latest.scrollLeft += 350;
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
							<img src="" alt="event2" width='270px' height='184x'/>
							<p>United way is asking
								everyone to donate
								their unwanted foods
								to the poor.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="" alt="event2" width='270px' height='184x'/>
							<p>American Red Cross
								is inviting everyone
								to attend their blood
								giving fair.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="" alt="event2" width='270px' height='184x'/>
							<p>Direct Relief Disaster 
								Fair to raise funds for
								the recent hurricane
								attack.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="" alt="event2" width='270px' height='184x'/>
							<p>This is Jacky Chan
								because Jacky Chan.
								because he kicks butt.
								Don’t question this.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="" alt="event2" width='270px' height='184x'/>
							<p>Salving has no season.
								Donate your money
								now to people
								who needs it.</p>
							<span className='h-learn-btn'>LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="" alt="event2" width='270px' height='184x'/>
							<p>Direct Relief Disaster 
								Fair to raise funds for
								the recent hurricane
								attack.</p>
							<span className='h-learn-btn' >LEARN MORE</span>
						</div>
						<div className={Styles.latestmodule}>
							<img src="" alt="event2" width='270px' height='184x'/>
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
