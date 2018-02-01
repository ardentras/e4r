import React from "react";
import Styles from "./style.css";

const Testbanner = props => (
    <div className={Styles.testbanner}>
      <span className={Styles.bannertext}>The current website is for testing only...</span> 
      <img className={Styles.bannerimg} src="/static/cat.ico" alt="testImg"/>
	  </div>
);

export default Testbanner;