import React from "react";
import Styles from "./style.css";

const Footer = props => (// eslint-disable-line no-unused-vars
	<div className={Styles.footer}>
		<a href="https://www.facebook.com/e4rcommunity/" target="_blank" className={[Styles.facebook, Styles.social].join(" ")}>
            <i className="fa fa-facebook" aria-hidden="true"></i>
		</a>
        <a href="https://twitter.com/e4rtesting" target="_blank" className={[Styles.twitter, Styles.social].join(" ")}>
            <i className="fa fa-twitter" aria-hidden="true"></i>
        </a>
        <a href="https://github.com/ardentras/e4r" target="_blank" className={[Styles.github, Styles.social].join(" ")}>
            <i className="fa fa-github-alt" aria-hidden="true"></i>
        </a>
	</div>
);

export default Footer;