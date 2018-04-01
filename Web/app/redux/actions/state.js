import Types from "../types";
import PrivateStyle from "../../components/private/style.css";
import SettingStyle from "../../components/private/settings/style.css";
import HomeStyle from "../../components/private/home/style.css";
import iCookie from "../../libraries/iCookie";

export function authenticating() {
	return {
		type: Types.Auth.AUTHENTICATING
	}
}

export function authenticated() {
	return {
		type: Types.Auth.AUTHENTICATE_SUCCESSFUL
	}
}

export function deauthenticating() {
	return {
		type: Types.Auth.DEAUTHENTICATING
	}
}

export function setTheme(theme) {
	return {
		type: Types.State.SET_THEME,
		value: theme
	}
}

export function deauthenticated() {
	return {
		type: Types.Auth.DEAUTHENTICATE_SUCCESSFUL
	}
}

export function resetAuth() {
	return {
		type: Types.Auth.RESET
	}
}

export function SignUp(status) {
	return {
		type: Types.Join.SIGNUP,
		value: status
	}
}

export function SignedUp() {
	return {
		type: Types.Join.SIGNUP_SUCCESSFUL
	}
}

export function SigningUp(status) {
	return {
		type: Types.Join.SIGNINGUP,
		value: status
	}
}

export function resetSignUp() {
	return {
		type: Types.Join.RESET
	}
}

export function redirect(status) {
	return {
		type: Types.State.REFER,
		value: status
	}
}

export function setShowQuestion(status) {
	return {
		type: Types.State.SET_SHOWQUESTION,
		value: status
	}
}

export function Error(status=undefined) {
	return {
		type: Types.State.ERROR,
		value: status
	}
}

export function resetState() {
	return {
		type: Types.State.RESET
	}
}

export function setChatConnected() {
	return {
		type: Types.State.SET_CHAT_CONNECTED
	}
}

export function setShowSetting(status) {
	return {
		type: Types.State.SET_SHOWSETTING,
		value: status
	}
}

export function showChat(status) {
	return {
		type: Types.State.SHOW_CHAT,
		value: status
	}
}

export function showDash(status) {
	return {
		type: Types.State.SHOW_DASH,
		value: status
	}
}

export function chatConnected(status) {
	return {
		type: Types.State.SET_CHAT_CONNECTED,
		value: status
	}
}

export function handleRegisterButton(status) {
	return async dispatch => {
		try {

		}
		catch(err) {

		}
	}
}

export function setSocket(socket) {
	return {
		type: Types.State.SET_SOCKET,
		value: socket
	}
}

export function setPersist(status) {
	return {
		type: Types.State.SET_PERSIST,
		value: status
	}
}

export function handleTheme(current) {
	return async dispatch => {
		iCookie.setStorage("theme", (current === "Light" ? "Dark" : "Light"));
		dispatch(setTheme(current === "Light" ? "Dark" : "Light"));
	}
}