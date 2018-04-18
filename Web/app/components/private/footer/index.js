import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { showChat, showDash, setSocket, setChatConnected } from "../../../redux/actions/state";
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
				this.props.states.CHAT_CONNECTED === "online" ? this.props.setTotalUser(this.props.totalUser - 1) : null;
			}
			this.props.setChatConnected("offline");
		}
		else {
			const connection = io("http://kevinjxu.me:40196",{transports: ['websocket']});
			const ChatBox = document.getElementsByClassName(ChatStyle.chatbox)[0];
			ChatBox.className = ChatStyle.activechatbox;
			if (!this.props.states.SOCKET) {
				this.props.setSocket(connection);
				this.props.initSocketHanlder(connection, this.props.uo.user_data.username);
			}
			this.props.setChatConnected("connecting");
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
		const helper = document.getElementsByClassName(Style.helptext)[0];
		const helpbtn = document.getElementById(Style.helpbtn);
		helpbtn.classList.toggle(Style.activehelpbtn);
		helper.classList.toggle(Style.activeHelperText);
		this.props.SHOW_HELP ? this.props.hideHelp() : this.props.showHelp();
	}
	render() {
		if (this.props.SHOW_HELP) {
			const helper = document.getElementsByClassName(Style.helptext)[0];
			if (helper) {
				helper.style.top = -(helper.clientHeight + 30) + "px";
			}
		}	
		return (
			<div className={[Style.footer, (this.props.states.THEME === "Light" ? null : Style.darkfooter)].join(" ")}>
				<span className={Style.menu} onClick={this.showDash}><i id={Style.menubtn} className="fa fa-bars"></i></span>
				<ul className={Style.other}>
					{this.props.states.SHOW_QUESTION && (<li onClick={this.showHelp} id={Style.helpbtn} className={[Style.otherselector, (this.props.states.THEME === "Light" ? Style.ligthhelpbtn : Style.darkhelpbtn)].join(" ")}>
						<span>Show Help</span>
						<div className={Style.helptext} onClick={(event)=>{
								event.stopPropagation();
							}}>
							{this.props.states.error !== error.GET_HELP_TIMEOUT && this.props.HELP ? this.props.HELP.indexOf("http") >= 0 ? <a className={[Style.helplink, Style.help].join(" ")} href={this.props.HELP} target="_blank">{this.props.HELP}</a> : <span className={Style.help}>{this.props.HELP}</span> : this.props.states.error !== undefined ? <span className={Style.help}>{this.props.states.error}</span> : "Fetching..."}
							{this.props.HELP === error.GET_HELP_TIMEOUT && <button onClick={this.getHelp} className={Style.retrybtn}>Retry</button> }
						</div>
					</li>)}
					<li onClick={this.showChat} className={Style.otherselector}>
						<i id={this.props.states.CHAT_CONNECTED === "online" ? Style.online :
							   this.props.states.CHAT_CONNECTED === "offline" ? Style.offline :
							   Style.connecting} className="fa fa-circle"></i>
						<span id={Style.totaluser}>{this.props.totalUser}</span>
					</li>
				</ul>
			</div>
		)
	}
}

export default connect(
	(state) => ({states: state.state, SHOW_HELP: state.questions.showHelp, HELP: state.questions.helpText, questions: state.questions, totalUser: state.messages.totalUsers, uo: state.user.userobject}),
	(dispatch) => bindActionCreators({ showChat, showDash, showHelp, hideHelp, setSocket, initSocketHanlder, getHelp, setTotalUser, setChatConnected}, dispatch)
)(Footer);
