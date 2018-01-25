/****************************************************************************
 * 
 *->Name: Home.js 
 *->Purpose: React Component displaying the Home page.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import Landing from "./components/landing"; // eslint-disable-line no-unused-vars
import Latest from "./components/latest";// eslint-disable-line no-unused-vars
import Footer from "./components/footer";// eslint-disable-line no-unused-vars
import Styles from "./style.css";
import childStyle from "./components/style.css";

const Mission = props =>  // eslint-disable-line no-unused-vars
	<div className={Styles.mission}>
		<div className={[Styles.solve, Styles.mcells].join(" ")}>
			<h1>Solve</h1>
			<p>
				Solve a variety of questions
				in the fields of math, history
				and science.
			</p>
		</div>
		<div className={[Styles.donate, Styles.mcells].join(" ")}>
			<h1>Donate</h1>
			<p>
				By solving questions, we will
				donate all the revenue generated
				from ads to a selected charity of
				your choice.
			</p>
		</div>
		<div className={[Styles.learn, Styles.mcells].join(" ")}>
			<h1>Learn</h1>
			<p>
				Even when you fail to solve a
				question, we will provide
				a how-to to help you learn
				the proper ways to solve it.
			</p>
		</div>
	</div>;

const SupportedCharities = props => // eslint-disable-line no-unused-vars
	<div className={Styles.supported}>
		<h1 className={Styles.helpingtitle}>Helping millions of those in need.</h1>
		<div className={Styles.charities}>
			<a className={Styles.charity} href="http://www.redcross.org" target="_blank">American Red Cross</a>
			<a className={Styles.charity} href="https://www.unitedway.org" target="_blank">United Way</a>
			<a className={Styles.charity} href="https://www.directrelief.org" target="_blank">Direct Relief</a>
			<a className={Styles.charity} href="http://www.salvationarmyusa.org/usn/" target="_blank">The Salvation Army</a>
			<a className={Styles.charity} href="https://www.woundedwarriorproject.org" target="_blank">Wounded Warrior Project</a>
			<a className={Styles.charity} href="http://www.feedingamerica.org" target="_blank">Feeding America</a>
			<a className={Styles.charity} href="http://www.lls.org" target="_blank">Leukemia &amp; Lymphoma Society</a>
			<a className={Styles.charity} href="https://www.taskforce.org" target="_blank">The Task Force for Global Health</a>
		</div>
	</div>;


export default class Home extends React.Component { // eslint-disable-line no-unused-vars
	constructor(props) {
		super(props);
		this.charityRotate = null;
		this.maxScrollLeft = false;
		this.slideCount = 0;
		this.autoSlide = null;
		this.animate = this.animate.bind(this);
	}
	componentDidMount() {
		this.autoSlide = setInterval(this.animate, 15*1000);
	}
	componentWillUnmount() {
		window.clearInterval(this.autoSlide);
	}
	animate() {
		const slide = document.getElementsByClassName(childStyle.parallex)[0];
		const slidetwo = document.getElementsByClassName(childStyle.parallextwo)[0];
		const slidethree = document.getElementsByClassName(childStyle.parallexthree)[0];
		const dot = document.getElementsByClassName("fa-circle")[0];
		const dottwo = document.getElementsByClassName("fa-circle")[1];
		const dotthree = document.getElementsByClassName("fa-circle")[2];
		if (this.slideCount === 0) {
			slide.style.opacity = 1;
			slidetwo.style.opacity = 0;
			slidethree.style.opacity = 0;
			dot.id = childStyle.pactive;
			dottwo.id = "";
			dotthree.id = "";
		}
		else if (this.slideCount === 1) {
			slide.style.opacity = 0;
			slidetwo.style.opacity = 1;
			slidethree.style.opacity = 0;
			dot.id = "";
			dottwo.id = childStyle.pactive;
			dotthree.id = "";
		}
		else {
			slide.style.opacity = 0;
			slidetwo.style.opacity = 0;
			slidethree.style.opacity = 1;
			dot.id = "";
			dottwo.id = "";
			dotthree.id = childStyle.pactive;
		}
		++this.slideCount;
		if (this.slideCount >= 3) {
			this.slideCount = 0;
		}
	}
	render() {
		return (
			<div>
				<div className={Styles.clear}></div>
				<Landing/>
				<Mission/>
				<SupportedCharities/>
				<Latest/>
				<Footer/>
			</div>
		);
	}
}