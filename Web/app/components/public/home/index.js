/****************************************************************************
 * 
 *->Name: Home.js 
 *->Purpose: React Component displaying the Home page.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars

import Description from "./components/description";
import Charity from "./components/charities";
import LatestNews from "./components/latestnews";
import Footer from "./components/footer";
import Loading from "../../loading";

import Styles from "./style.css";

export default class Home extends React.Component { // eslint-disable-line no-unused-vars
	constructor(props) {
		super(props);
	}
	render() {
		return (
			<div>
				<Description/>
				<Charity/>
				<LatestNews/>
				<Footer/>
			</div>
		);
	}
}