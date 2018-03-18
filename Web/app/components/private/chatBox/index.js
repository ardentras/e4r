import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import Style from "./style.css";
import ContStyle from "../style.css";

class ChatBox extends React.Component {
	constructor(props) {
		super(props);
		this.sendMessage = this.sendMessage.bind(this);
		this.displayMessage = this.displayMessage.bind(this);
	}
	componentDidMount() {
		const chatbox = document.getElementsByClassName(Style.chatbox)[0];
		const content = document.getElementsByClassName(ContStyle.contents)[0];
		chatbox.style.maxHeight = content.clientHeight + "px";
	}
	displayMessage(text, from) {
		const messages = document.getElementById(Style.messages);
		const msgcontainer = document.createElement("div");
		const textcontainer = document.createElement("div");
		const name = document.createElement("span");
		const nametext = document.createTextNode(from + ": ");
		const newText = document.createTextNode(text);

		textcontainer.classList.add(Style.textcontainer);
		if (from === this.props.user.user_data.username) {
			msgcontainer.classList.add(Style.selfmsgcontainer);
		}
		else if (from === "Server") {
			msgcontainer.classList.add(Style.servermsgcontainer);
		}
		else if (from === "System") {
			msgcontainer.classList.add(Style.systemmsgcontainer);
		}
		else {
			msgcontainer.classList.add(Style.msgcontainer);
		}
		name.classList.add(Style.name);

		name.appendChild(nametext);
		textcontainer.appendChild(newText);

		msgcontainer.appendChild(name);
		msgcontainer.appendChild(textcontainer);

		messages.appendChild(msgcontainer);
		if (messages.scrollHeight > messages.clientHeight) {
			messages.scrollTop = messages.scrollHeight;
		}
	}
	sendMessage(event) {
		event.preventDefault();
		if (event.target.msg.value && event.target.msg.value !== "") {
			if (this.props.states.SOCKET) {
				this.props.states.SOCKET.emit("send-message", {name: this.props.user.user_data.username, msg: event.target.msg.value});
				this.displayMessage(event.target.msg.value, this.props.user.user_data.username);
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
	(dispatch) => bindActionCreators({ }, dispatch)
)(ChatBox);
