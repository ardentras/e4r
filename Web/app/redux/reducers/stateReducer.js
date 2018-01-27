/****************************************************************************
 * 
 *->File Name: stateReducer.js
 *->File Purpose: To declare a reducer for keeping track of states.
 *
*****************************************************************************/

import { 
	AUTHENTICATING,
	DEAUTHENTICATING,
	SIGNINGUP,
	AUTHENTICATE_SUCCESSFUL,
	DEAUTHENTICATE_SUCCESSFUL,
	SIGNUP_SUCCESSFUL,
	SIGN_UP,
	ERROR,
	REFER,
	RESET } from "../types";

const initialState = {
	IS_AUTH: false,
	IS_SIGNUP: false,
	AUTHING: false,
	SIGNINGUP: false,
	DEAUTHING: false,
	AUTH_SUCCESSFUL: false,
	DEAUTH_SUCCESSFUL: false,
	SIGNUP_SUCCESSFUL: false,
	redirectToReferrer: false,
	error: undefined
};

const stateReducer = (state=initialState, action) => {
	switch(action.type) {
	case SIGN_UP:
		return Object.assign({}, state, {
			IS_SIGNUP: action.value
		});
	case AUTHENTICATING:
		return Object.assign({}, state, {
			AUTHING: action.value
		}); 
	case DEAUTHENTICATING:
		return Object.assign({}, state, {
			DEAUTHING: true
		});
	case SIGNINGUP:
		return Object.assign({}, state, {
			SIGNINGUP: true
		});
	case AUTHENTICATE_SUCCESSFUL:
		return Object.assign({}, state, {
			IS_AUTH: action.value
		});
	case DEAUTHENTICATE_SUCCESSFUL:
		return Object.assign({}, initialState);
	case SIGNUP_SUCCESSFUL:
		return Object.assign({}, state, {
			SIGNUP_SUCCESSFUL: true
		});
	case ERROR:
		return Object.assign({}, state, {
			error: action.value
		});
	case REFER:
		return Object.assign({}, state, {
			redirectToReferrer: action.value
		});
	case RESET:
		return Object.assign({}, initialState);
	default:
		return Object.assign({}, state);
	}
};

export default stateReducer;
