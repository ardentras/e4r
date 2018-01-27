import React from "react";
import Styles from "./style.css";
import { Link } from "react-router-dom";
import Mission from "./component";

const Description = props => (
    <div className={Styles.description}>
		<div className={Styles.welcome}>
			<div className={[Styles.feature, Styles.parallex].join(" ")}></div>
			<div className={[Styles.featuretwo, Styles.parallex].join(" ")}></div>
			<div className={[Styles.featurethree, Styles.parallex].join(" ")}></div>
			<div className={Styles.aboutus}>
				<span>BE A BETTER PERSON.</span>
				<span className={Styles.pdescription}>A project to help learning and those in need of charity.</span>
				<Link className={Styles.jlink} to="/login">
					<span className={Styles.jbtn}>JOIN US</span>
				</Link>
				<div className={Styles.pselectors}>
					<i id={Styles.psactive} className="fa fa-circle" aria-hidden="true" onClick={null}></i>
					<i className="fa fa-circle" aria-hidden="true" onClick={null}></i>
					<i className="fa fa-circle" aria-hidden="true" onClick={null}></i>
				</div>
			</div>
		</div>
		<Mission/>
    </div>
);

export default Description;
