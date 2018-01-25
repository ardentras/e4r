import { 
	SET_USER_INFO,
	SET_SESSION_TOKEN,
	AUTHENTICATING, 
	DEAUTHENTICATING } from "../../types/types";
import {
	setAuthenticateSuccess,
	setDeAuthenticateSuccess,
	setSignUpSuccessful,
	ifSignUp,
	Error,
	Refer } from "../state/state";
import iAuth from "../../../libraries/iAuth";
import iCookie from "../../../libraries/iCookie";

/****************************************************************************
 * 
 *->Action Name: setUserformation
 *->Action Purpose: To set the user login information.
 *
*****************************************************************************/
export function setUserInformation({username=undefined, password=undefined, email=undefined}) {
	return {
		type: SET_USER_INFO,
		email: email,
		username: username,
		password: password
	};
}

/****************************************************************************
 * 
 *->Action Name: setSessionToken
 *->Action Purpose: To set the value of session token.
 *
*****************************************************************************/
export function setSessionToken(token) {
	return {
		type: SET_SESSION_TOKEN,
		session: token,
	};
}

/****************************************************************************
 * 
 *->Action Name: Authenticate
 *->Action Purpose: To set the authenticating state.
 *
*****************************************************************************/
export function Authenticate(state) {
	return {
		type: AUTHENTICATING,
		value: state	
	};
}

/****************************************************************************
 * 
 *->Action Name: DeAuthenticate
 *->Action Purpose: To set the deauthenticating state.
 *
*****************************************************************************/
export function DeAuthenticate() {
	return {
		type: DEAUTHENTICATING
	};
}

/****************************************************************************
 * 
 *->Action Name: handlerUserAuth
 *->Action Purpose: To return a dispatch function which handles async api 
 *					call to login.
 *
*****************************************************************************/
export function handlerUserAuth(user) {
	return (dispatch)=>{
		dispatch(Error());
		dispatch(Authenticate(true));
		iCookie.reset();
		iAuth.Authenticate(user)
			.then((result)=>{
				dispatch(Authenticate(false));
				dispatch(setSessionToken(result));
				dispatch(setAuthenticateSuccess(true));
				const date = new Date();
				date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000));
				const expire = "expires=" + date.toUTCString();
				const cookie1 = "username=" + user.username + ";" + expire + ";path=/";
				const cookie2 = "session=" + result + ";" + expire + ";path=/";
				iCookie.set(cookie1)
					.catch((reject)=>{ // eslint-disable-line no-unused-vars
						dispatch(Error("COOKIE_FAIL")); // eslint-disable-line no-console
					});
				iCookie.set(cookie2)
					.catch((reject)=>{ // eslint-disable-line no-unused-vars
						dispatch(Error("COOKIE_FAIL")); // eslint-disable-line no-console
					});
				dispatch(Refer());
			})
			.catch((reject)=>{ // eslint-disable-line no-unused-vars
				dispatch(Authenticate(false));
				if (reject.response) {
					dispatch(Error("AUTH_FAIL"));
				}
				else {
					dispatch(Error("CONN_ERR"));
				}
			});
	};
}

/****************************************************************************
 * 
 *->Action Name: handlerUserDeAuth
 *->Action Purpose: To a dispatch function which handles async functions.
 *
*****************************************************************************/
export function handlerUserDeAuth(user) {
	return (dispatch)=>{
		dispatch(Error());
		dispatch(DeAuthenticate());
		iAuth.Deauthenticate(user)
			.then((result)=>{ // eslint-disable-line no-unused-vars
				dispatch(setDeAuthenticateSuccess(true));
				iCookie.reset();
				window.location.reload(true);
			})
			.catch((reject)=>{ // eslint-disable-line no-unused-vars
				console.log(reject);
				dispatch(Error("DEAUTH_FAIL"));
			});
	};
}

/****************************************************************************
 * 
 *->Action Name: handlerRegister
 *->Action Purpose: To a dispatch function which handles async functions.
 *
*****************************************************************************/
export function handlerRegister(user) {
	return (dispatch)=>{
		dispatch(Error());
		iAuth.Register(user)
			.then((result)=>{ // eslint-disable-line no-unused-vars
				if(result.response === "Succeed") {
					dispatch(setSignUpSuccessful(true));
					dispatch(ifSignUp(false));
				}

			})
			.catch((reject)=>{ // eslint-disable-line no-unused-vars
				if(reject.response) {
					dispatch(reject.Code === 500 ? Error("INVALID_EMAIL") : Error("SIGNUP_FAIL"));
				}
				else {
					dispatch(Error("CONN_ERR"));
				}
			});
	};
}