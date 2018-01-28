import React from "react";
import Styles from "./style.css";

const Mission = props => ( // eslint-disable-line no-unused-vars
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
	</div>
);

export default Mission;
