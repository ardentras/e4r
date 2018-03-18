import React from "react";
import { bindActionCreators } from "redux";
import { setMessage, displayMessage } from "../../../redux/actions/message";
import { connect } from "react-redux";
import Style from "./style.css";
import NavStyle from "../../public/navagations/style.css";
import ContStyle from "../style.css";
import FooterStyle from "../footer/style.css";

class ChatBox extends React.Component {
	constructor(props) {
		super(props);
		this.sendMessage = this.sendMessage.bind(this);
	}
	componentDidMount() {
		const messages = document.getElementById(Style.messages);
		const chatbox = document.getElementsByClassName(Style.chatbox)[0];
		const viewHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
		const navagator = document.getElementById(NavStyle.navcontainer);
		const inputfields = document.getElementsByClassName(Style.inputfields)[0];
		const footer = document.getElementsByClassName(FooterStyle.footer)[0];
		const availableHeight = (viewHeight - navagator.offsetHeight - footer.offsetHeight);
		messages.style.height = (availableHeight - inputfields.offsetHeight - 1) + "px";
		messages.style.maxHeight = (availableHeight - inputfields.offsetHeight - 1) + "px";
	}
	sendMessage(event) {
		event.preventDefault();
		if (event.target.msg.value && event.target.msg.value !== "") {
			if (this.props.states.SOCKET) {
				this.props.states.SOCKET.emit("send-message", {name: this.props.user.user_data.username, msg: event.target.msg.value});
				this.props.displayMessage(this.props.user.user_data.username,event.target.msg.value, this.props.user.user_data.username);
			}
			else {
				this.displayMessage("Cannot Connect to Server!", "System");
			}
			event.target.msg.value = "";
		}
	}
	render() {
		if (this.props.messages.message) {
			this.displayMessage(this.props.messages.message, this.props.messages.from);
			this.props.setMessage({name: undefined, msg: undefined});
		}
		return (
			<div className={Style.chatbox}>
				<div id={Style.messages}></div>
				<form className={Style.inputfields} action="javascript:void(0)" onSubmit={this.sendMessage}>
					<input id={Style.msg} type="text" name="msg"/>
					<input id={Style.sendbtn} type="submit" value="send"/>
				</form>
			</div>
		)
	}
}

export default connect(
	(state) => ({states: state.state, user: state.user.userobject, messages: state.messages}),
	(dispatch) => bindActionCreators({ setMessage, displayMessage}, dispatch)
)(ChatBox);
