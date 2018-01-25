import React from "react";
import {Parallex, ParallexTwo, ParallexThree} from "./parallex"; // eslint-disable-line no-unused-vars
import Styles from "./style.css";
import { Link } from "react-router-dom"; // eslint-disable-line no-unused-vars


function slide() {
	const pOne = document.getElementsByClassName(Styles.parallex)[0];
	const pTwo = document.getElementsByClassName(Styles.parallextwo)[0];
	const pThree = document.getElementsByClassName(Styles.parallexthree)[0];
	pOne.style.opacity = 1;
	pTwo.style.opacity = 0;
	pThree.style.opacity = 0;
	const dot = document.getElementsByClassName("fa-circle")[0];
	const dottwo = document.getElementsByClassName("fa-circle")[1];
	const dotthree = document.getElementsByClassName("fa-circle")[2];
	dot.id = Styles.pactive;
	dottwo.id = "";
	dotthree.id = "";
}

function slideTwo() {
	const pOne = document.getElementsByClassName(Styles.parallex)[0];
	const pTwo = document.getElementsByClassName(Styles.parallextwo)[0];
	const pThree = document.getElementsByClassName(Styles.parallexthree)[0];
	pOne.style.opacity = 0;
	pTwo.style.opacity = 1;
	pThree.style.opacity = 0;
	const dot = document.getElementsByClassName("fa-circle")[0];
	const dottwo = document.getElementsByClassName("fa-circle")[1];
	const dotthree = document.getElementsByClassName("fa-circle")[2];
	dot.id = "";
	dottwo.id = Styles.pactive;
	dotthree.id = "";
}

function slideThree() {
	const pOne = document.getElementsByClassName(Styles.parallex)[0];
	const pTwo = document.getElementsByClassName(Styles.parallextwo)[0];
	const pThree = document.getElementsByClassName(Styles.parallexthree)[0];
	pOne.style.opacity = 0;
	pTwo.style.opacity = 0;
	pThree.style.opacity = 1;
	const dot = document.getElementsByClassName("fa-circle")[0];
	const dottwo = document.getElementsByClassName("fa-circle")[1];
	const dotthree = document.getElementsByClassName("fa-circle")[2];
	dot.id = "";
	dottwo.id = "";
	dotthree.id = Styles.pactive;
}

const JoinBtn = props =>  // eslint-disable-line no-unused-vars
	<Link className={Styles.jbtn} to="/login">
		<span>JOIN US</span>
	</Link>

const Landing = props =>  // eslint-disable-line no-unused-vars
	<div className={Styles.landing}>
		<Parallex/>
		<ParallexTwo/>
		<ParallexThree/>
		<div className={Styles.description}>
			<span>BE A BETTER PERSON.</span>
			<span className={Styles.project}>A project to help learning and those in need of charity.</span>
			<JoinBtn/>
			<div className={Styles.dot}>
				<i id={Styles.pactive} className="fa fa-circle" aria-hidden="true" onClick={slide}></i>
				<i className="fa fa-circle" aria-hidden="true" onClick={slideTwo}></i>
				<i className="fa fa-circle" aria-hidden="true" onClick={slideThree}></i>
			</div>
		</div>
	</div>;

export default Landing;