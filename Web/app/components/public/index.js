/****************************************************************************
 * 
 *->Name: App.js 
 *->Purpose: React Component root of routers and Navagation component.
 *
*****************************************************************************/
import React from "react"; // eslint-disable-line no-unused-vars
import {
	BrowserRouter,
	Route// eslint-disable-line no-unused-vars
} from "react-router-dom";
import {connect} from "react-redux";
import Navagations from "./navagations"; // eslint-disable-line no-unused-vars
import Routes from "./routes";
import Styles from "./style.css";

const App = props => ( // eslint-disable-line no-unused-vars
	<BrowserRouter>
		<div className={Styles.container}>
			<div>
				<Navagations/>
			</div>
			{Routes.map((elem, index)=>(
				<Route key={index} exact={elem.exact} path={elem.path} component={elem.component} states={props.states}/>
			))}
		</div>
	</BrowserRouter>  
);

export default connect(
	(state) => ({states: state.state})
)(App);