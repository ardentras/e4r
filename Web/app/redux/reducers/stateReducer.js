/****************************************************************************
 * 
 *->File Name: stateReducer.js
 *->File Purpose: To declare a reducer for keeping track of states.
 *
*****************************************************************************/

import Types from "../types";

const initialState = {
	IS_AUTH: false,
	IS_SIGNUP: false,
	AUTHING: false,
	SIGNINGUP: false,
	DEAUTHING: false,
	AUTH_SUCCESSFUL: false,
	DEAUTH_SUCCESSFUL: false,
	SIGNUP_SUCCESSFUL: false,
	PERSIST: false,
	THEME: "Light",
	SOCKET: null,
	CHAT_CONNECTED: false,
	SHOW_QUESTION: false,
	SHOW_SETTING: false,
	SHOW_DASH: false,
	SHOW_CHAT: false,
	redirectToRefer: false,
	error: undefined
};

const stateReducer = (state=initialState, action) => {
	switch(action.type) {
	case Types.Auth.AUTHENTICATING:
		return Object.assign({}, state, {
			AUTHING: true
		});
	case Types.Auth.AUTHENTICATE_SUCCESSFUL:
		return Object.assign({}, state, {
			AUTH_SUCCESSFUL: true,
			IS_AUTH: true
		});
	case Types.Auth.DEAUTHENTICATING:
		return Object.assign({}, state, {
			DEAUTHING: true
		});
	case Types.Auth.DEAUTHENTICATE_SUCCESSFUL:
		return Object.assign({}, state, {
			DEAUTH_SUCCESSFUL: true
		});
	case Types.Auth.RESET:
		return Object.assign({}, state, {
			DEAUTHING: false,
			AUTHING: false,
			AUTH_SUCCESSFUL: false,
			DEAUTH_SUCCESSFUL: false
		});
	case Types.Join.SIGNUP:
		return Object.assign({}, state, {
			IS_SIGNUP: action.value
		});
	case Types.Join.SIGNUP_SUCCESSFUL:
		return Object.assign({}, state, {
			SIGNUP_SUCCESSFUL: true
		});
	case Types.Join.SIGNINGUP:
		return Object.assign({}, state, {
			SIGNINGUP: action.value
		});
	case Types.Join.RESET:
		return Object.assign({}, state, {
			IS_SIGNUP: false,
			SIGNUP_SUCCESSFUL: false,
			SIGNINGUP: false
		});
	case Types.State.SET_SHOWQUESTION: 
		return Object.assign({}, state, {
			SHOW_QUESTION: action.value
		});
	case Types.State.SET_SOCKET:
		return Object.assign({}, state, {
			SOCKET: action.value
		});
	case Types.State.SET_CHAT_CONNECTED:
		return Object.assign({}, state, {
			CHAT_CONNECTED: action.value
		});
	case Types.State.SHOW_CHAT:
		return Object.assign({}, state, {
			SHOW_CHAT: action.value
		});
	case Types.State.SET_THEME:
		return Object.assign({}, state, {
			THEME: action.value
		});
	case Types.State.SHOW_DASH:
		return Object.assign({}, state, {
			SHOW_DASH: action.value
		});
	case Types.State.SET_SHOWSETTING:
		return Object.assign({}, state, {
			SHOW_SETTING: action.value
		});
	case Types.State.REFER:
		return Object.assign({}, state, {
			redirectToRefer: action.value
		});
	case Types.State.SET_PERSIST:
		return Object.assign({}, state, {
			PERSIST: action.value
		});
	case Types.State.ERROR:
		return Object.assign({}, state, {
			error: action.value
		});
	case Types.State.RESET:
		return Object.assign({}, state, initialState);
	default:
		return Object.assign({}, state);
	}
};

export default stateReducer;
