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
import { bindActionCreators } from "redux";
import {handlerPersist} from "../../redux/actions/auth";
import Navagations from "./navagations"; // eslint-disable-line no-unused-vars
import Routes from "./routes";
import PrivateRoute from "./routes/private";
import Private from "../private";
import Styles from "./style.css";

class App extends React.Component {
	constructor(props) {
		super(props);
	}
	async componentWillMount() {
		if (!this.props.states.IS_AUTH) {
			this.props.handlerPersist();
		}
	}
	render() {
		return (
			<BrowserRouter>
				<div className={Styles.container}>
					<div>
						<Navagations IS_AUTH={this.props.states.IS_AUTH} UID={this.props.uid}/>
					</div>
					{Routes.map((elem, index)=>(
						<Route key={index} exact={elem.exact} path={elem.path} component={elem.component}/>
					))}
					<PrivateRoute path="/dashboard" states={this.props.states} component={Private}/>
				</div>
			</BrowserRouter> 
		)
	}
}

export default connect(
	(state) => ({states: state.state, uid: state.user.uid}),
	(dispatch) => bindActionCreators({handlerPersist}, dispatch)
)(App);