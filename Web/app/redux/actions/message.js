import Types from "../types";
import Style from "../../components/private/chatBox/style.css";
import NavStyle from "../../components/public/navagations/style.css";
import FooterStyle from "../../components/private/footer/style.css";
import { setSocket, setChatConnected } from "./state";

export function setTotalUser(count) {
	return {
		type: Types.Messages.SET_TOTAL_USER,
		value: count
	}
}

export function initSocketHanlder(socket, uid) {
	return async dispatch => {
		if (socket) {
			socket.on("connect", ()=>{
				dispatch(setChatConnected(true));
			});
			socket.on("disconnect", (data)=>{
				dispatch(setChatConnected(false));
			});
			socket.on("new-message", (data)=>{
				dispatch(displayMessage(data.name, data.msg, uid));
			});
			socket.on("connect_timeout", (timeout)=>{	
				socket.close();
				dispatch(displayMessage("System", " Connection Timed Out!"));
				dispatch(setSocket(null));
			});
			socket.on("error", (err)=>{
				socket.close();
				dispatch(displayMessage("System", "Error has occured!"));
				dispatch(setSocket(null));
			});
			socket.on("connect_error",(err)=>{
				socket.close();
				dispatch(displayMessage("System", "Cannot Establish Connection with Server!"));
				dispatch(setSocket(null));
			});
			socket.on("user-connected", (data)=>{
				console.log("Connected");
				dispatch(setTotalUser(data));
			});
			socket.on("user-disconnected",(data)=>{
				dispatch(setTotalUser(data));
			});	
		}
	}
}

export function clearMessages() {
	return async dispatch => {
		const messages = document.getElementById(Style.msgcontainer);
		while (messages.firstChild) {
			messages.removeChild(messages.firstChild);
		}
	}
}

export function displayMessage(from, text, me) {
	return async dispatch => {
		const messages = document.getElementById(Style.msgcontainer);
		const rootcontainer = document.createElement("div");
		const msgcontainer = document.createElement("div");
		const textcontainer = document.createElement("div");
		const name = document.createElement("span");
		const nametext = document.createTextNode(from + ": ");
		const newText = document.createTextNode(text);

		textcontainer.classList.add(Style.textcontainer);
		if (from === me) {
			msgcontainer.classList.add(Style.selfmsgcontainer);
			rootcontainer.classList.add(Style.selfrootmsgcontainer);
		}
		else if (from === "Server") {
			rootcontainer.classList.add(Style.rootmsgcontainer);
			msgcontainer.classList.add(Style.servermsgcontainer);
		}
		else if (from === "System") {
			rootcontainer.classList.add(Style.rootmsgcontainer);
			msgcontainer.classList.add(Style.systemmsgcontainer);
		}
		else {
			rootcontainer.classList.add(Style.rootmsgcontainer);
			msgcontainer.classList.add(Style.msgcontainer);
		}
		name.classList.add(Style.name);

		name.appendChild(nametext);
		textcontainer.appendChild(newText);

		msgcontainer.appendChild(name);
		msgcontainer.appendChild(textcontainer);
		rootcontainer.appendChild(msgcontainer);
		messages.appendChild(rootcontainer);
		if (messages.scrollHeight > messages.clientHeight) {
			messages.scrollTop = messages.scrollHeight;
		}
	}
}

