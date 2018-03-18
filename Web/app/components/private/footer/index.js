import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { showChat, showDash, setSocket } from "../../../redux/actions/state";
import { showHelp, hideHelp, getHelp } from "../../../redux/actions/questions";
import { initSocketHanlder, setTotalUser } from "../../../redux/actions/message";
import error from "../../../redux/errorCodes";
import Style from "./style.css";
import DashStyle from "../newDash/style.css";
import ChatStyle from "../chatBox/style.css";
import SettStyle from "../settings/style.css";
import io from "socket.io-client";

class Footer extends React.Component {
	constructor(props) {
		super(props);
		this.showHelp = this.showHelp.bind(this);
		this.showDash = this.showDash.bind(this);
		this.showChat = this.showChat.bind(this);
		this.getHelp = this.getHelp.bind(this);
	}
	componentWillUnmount() {
		const ChatBox = document.getElementsByClassName(ChatStyle.activechatbox)[0];
		const Dashboard = document.getElementsByClassName(DashStyle.activedashboard)[0];
		const helper = document.getElementById(Style.helptext);
		Dashboard ? Dashboard.className = DashStyle.dashboard : null;
		ChatBox ? ChatBox.className = ChatStyle.chatbox : null;
		helper ? helper.style.display = "none" : null;
		helper ? helper.style.opacity = 0 : null;
		this.props.hideHelp();
		this.props.showChat(false);
		this.props.showDash(false);
		if (this.props.states.SOCKET) {
			this.props.states.SOCKET.close();
			this.props.setSocket(null);
		}
	}
	showDash() {
		if (this.props.states.SHOW_DASH) {
			const Dashboard = document.getElementsByClassName(DashStyle.activedashboard)[0];
			Dashboard.className = DashStyle.dashboard;
		}
		else {
			const Dashboard = document.getElementsByClassName(DashStyle.dashboard)[0];
			Dashboard.className = DashStyle.activedashboard;
		}
		this.props.showDash(!this.props.states.SHOW_DASH);
	}
	showChat() {
		if (this.props.states.SHOW_CHAT) {
			const ChatBox = document.getElementsByClassName(ChatStyle.activechatbox)[0];
			ChatBox.className = ChatStyle.chatbox;
			if (this.props.states.SOCKET) {
				this.props.states.SOCKET.close();
				this.props.setSocket(null);
				this.props.setTotalUser(this.props.totalUser - 1);
			}
		}
		else {
			const connection = io("52.40.134.152:3002");
			const ChatBox = document.getElementsByClassName(ChatStyle.chatbox)[0];
			ChatBox.className = ChatStyle.activechatbox;
			if (!this.props.states.SOCKET) {
				this.props.setSocket(connection);
				this.props.initSocketHanlder(connection, this.props.uo.user_data.username);
			}
		}
		this.props.showChat(!this.props.states.SHOW_CHAT);
	}
	getHelp() {
		this.props.getHelp(this.props.questions.questions[this.props.questions.index].QuestionID);
	}
	showHelp() {
		if (!this.props.HELP || this.props.HELP === "") {
			if (!this.props.questions.fetching_help) {
				this.getHelp();
			}
		}
		const helper = document.getElementById(Style.helptext);
		const helpbtn = document.getElementById(Style.helpbtn);
		if (this.props.SHOW_HELP) {
			helpbtn.style.background = "white";
			helpbtn.style.color = "#333F4F";
			helper.style.display = "none";
			helper.style.opacity = 0;
			this.props.hideHelp();
		}
		else {
			helpbtn.style.background = "#1E90FF";
			helpbtn.style.color = "white";
			helper.style.display = "flex";
			helper.style.opacity = 1;
			this.props.showHelp();
		}
	}
	render() {
		if (this.props.SHOW_HELP) {
			const helper = document.getElementById(Style.helptext);
			helper.style.top = -(helper.clientHeight + 30) + "px";
		}
		return (
			<div className={Style.footer}>
				<span className={Style.menu} onClick={this.showDash}><i id={Style.menubtn} className="fa fa-bars"></i></span>
				<ul className={Style.other}>
					{this.props.states.SHOW_QUESTION && (<li onClick={this.showHelp} id={Style.helpbtn} className={Style.otherselector}>
						<span>Show Help</span>
						<div id={Style.helptext} onClick={(event)=>{
								event.stopPropagation();
							}}>
							<span>{this.props.states.error !== error.GET_HELP_TIMEOUT && this.props.HELP ? this.props.HELP : this.props.states.error !== undefined ? this.props.states.error : "Fetching..."}</span>
							{this.props.HELP === error.GET_HELP_TIMEOUT && <button onClick={this.getHelp} className={Style.retrybtn}>Retry</button> }
						</div>
					</li>)}
					<li onClick={this.showChat} className={Style.otherselector}>
						<i id={Style.online} className="fa fa-circle"></i>
						<span id={Style.totaluser}>{this.props.totalUser}</span>
					</li>
				</ul>
			</div>
		)
	}
}

export default connect(
	(state) => ({states: state.state, SHOW_HELP: state.questions.showHelp, HELP: state.questions.helpText, questions: state.questions, totalUser: state.messages.totalUsers, uo: state.user.userobject}),
	(dispatch) => bindActionCreators({ showChat, showDash, showHelp, hideHelp, setSocket, initSocketHanlder, getHelp, setTotalUser}, dispatch)
)(Footer);
