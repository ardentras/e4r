import React from "react"; // eslint-disable-line no-unused-vars
import Styles from "./style.css";

const SupportedCharities = props => (// eslint-disable-line no-unused-vars
	<div className={Styles.supported}>
		<h1 className={Styles.stitle}>Helping millions of those in need.</h1>
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
	</div>
);

export default SupportedCharities;