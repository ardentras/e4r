import "babel-polyfill";
import Types from "../types";
import EFRapi from "../../libraries/efrApi";
import iCookie from "../../libraries/iCookie";
import http from "../httpCodes";
import error from "../errorCodes";
import { Error, authenticated, redirect, resetState, deauthenticating, deauthenticated, authenticating, resetAuth, setPersist,SigningUp,SignedUp, SignUp } from "./state";
import { resetQuestion } from "./questions";
import SpinnerStyle from "../../components/loading/style.css";

export function setFName(name) {
	return {
		type: Types.User.SET_FNAME,
		value: name
	}
}

export function setLName(name) {
	return {
		type: Types.User.SET_LNAME,
		value: name
	}
}

export function solvedQuestion() {
	console.log("solved questions");
	return {
		type: Types.User.SOLVED_QUESTION
	}
}

export function setToken(token) {
	return {
		type: Types.User.SET_SESSION_TOKEN,
		value: token
	}
}

export function setUserObject(userobject) {
	return {
		type: Types.User.SET_USER_OBJECT,
		value: userobject
	}
}

export function setComplete(questionId) {
	return {
		type: Types.User.SET_COMPLETED,
		value: [questionId]
	}
}

export function resetGame() {
	return {
		type: Types.User.GAME_RESET
	}
}

export function resetUser() {
	return {
		type: Types.User.RESET
	}
}

export function handleUserPersist() {
	return async dispatch => {
		try {
			dispatch(setPersist(true));
			dispatch(Error(error.NONE));
			const uo = iCookie.getStorage("userobject", true);
			const token = iCookie.getStorage("token", true);
			if (token && EFRapi.ValidateObject(uo)) {
				const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
				Loading.style.display = "flex";
				const renewToken = await EFRapi.renewSession(token);
				if (renewToken.data.code === http.Ok) {
					iCookie.setStorage("token", renewToken.data.session_id, true);
					dispatch(setToken(renewToken.data.session_id));
					const updatedUO = await EFRapi.updateUser({session: renewToken.data.session_id, userobject: uo});
					if (updatedUO.data.code === http.Ok) {
						if (EFRapi.ValidateObject(updatedUO.data.userobject)) {
							iCookie.setStorage("userobject", updatedUO.data.userobject, true);
							dispatch(setUserObject(updatedUO.data.userobject));
							dispatch(authenticated());
							dispatch(redirect(true));
						}
						else {
							dispatch(setPersist(false));
							iCookie.removeStorage("userobject");
							iCookie.removeStorage("token");
							dispatch(Error(error.INVALID_OBJECT));
						}
					}
					else {
						dispatch(setPersist(false));
						iCookie.removeStorage("userobject");
						iCookie.removeStorage("token");
						dispatch(Error(error.UPDATE_FAIL));
					}
				}
				else {
					dispatch(setPersist(false));
					iCookie.removeStorage("userobject");
					iCookie.removeStorage("token");
					dispatch(Error(error.INVALID_TOKEN));
				}
				Loading.style.display = "none";
			}
			else {
				dispatch(setPersist(false));
				iCookie.removeStorage("userobject");
				iCookie.removeStorage("token");
				dispatch(Error(error.CORRUPTED_FIELDS));
			}
		}
		catch(err) {
			dispatch(setPersist(false));
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "none";
			iCookie.removeStorage("userobject");
			iCookie.removeStorage("token");
			dispatch(Error((err.message.indexOf("timeout") >= 0 ? error.PERSIST_TIMEOUT : error.CONN_FAIL)));
			console.log("Persist Error: ", err);
		}
	}
}

export function handleAuthentication(uid=undefined, pw=undefined) {
	//Format
	// {
	// 	"user": {
	// 		"username": "test1 OR test@test.com",
	// 		"password": "testpassword"
	// 	}
	// }
	return async dispatch => {
		try {
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "flex";
			dispatch(authenticating());
			dispatch(Error(error.NONE));
			if (uid && pw) {
				const result = await EFRapi.login({username: uid, password: pw})
				if (result.data.code === http.Ok) {
					dispatch(authenticated());
					dispatch(setUserObject(result.data.user_object));
					dispatch(setToken(result.data.session_id));
					iCookie.setStorage("userobject",result.data.user_object, true);
					iCookie.setStorage("token", result.data.session_id, true);
					dispatch(redirect(true));
				}
				else {
					dispatch(resetAuth());
					dispatch(Error(error.LOGIN_FAIL));
				}
			}
			else {
				dispatch(resetAuth());
				dispatch(Error(error.EMPTY_FIELDS));
			}
			Loading.style.display = "none";
		}
		catch(err) {
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "none";
			dispatch(Error((err.message.indexOf("timeout") >= 0 ? error.TIME_OUT : error.CONN_FAIL)));
			dispatch(resetAuth());
			console.log("Authenticate Error: ", err);
		}
	}
}

export function handleDeAuthentication(token=undefined, userobject=undefined) {
	//Format
	// {
	// 	"user": {
	// 		"session": "{session_token}",
	// 		"userobject": "{user_object}"
	// 	}
	// }
	return async dispatch => {
		try {
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "flex";
			dispatch(deauthenticating());
			iCookie.removeStorage("token");
			iCookie.removeStorage("userobject");
			dispatch(Error(error.NONE));
			if (token && EFRapi.ValidateObject(userobject)) {
				EFRapi.logout({session: token, userobject: userobject});
			}
			else {
				dispatch(Error(error.ILLEGAL_AUTH));
			}
			dispatch(resetState());
			dispatch(resetUser());
			dispatch(resetQuestion());
			Loading.style.display = "none";
		}
		catch(err) {
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "none";
			dispatch(resetState());
			dispatch(resetUser());
			dispatch(resetQuestion());
		}
	}
}

export function handleUserObjectUpdate(token=undefined, userobject=undefined) {
	//Format
	// {
	// 	"user": {
	// 		"session": "{session_token}",
	// 		"userobject": "{user_object}"
	// 	}
	// }
	return async dispatch => {
		try {
			if (EFRapi.ValidateObject(userobject)) {
				const result = await EFRapi.updateUser({session: token, userobject: userobject});
				console.log(result.data);
				if (result.data.code === http.Ok) {
					iCookie.setStorage("userobject", result.data.userobject, true);
					dispatch(setUserObject(result.data.userobject));
				}
				else {
					alert("Invalid Token Detected!");
					dispatch(handleDeAuthentication());
				}
			}
			else {
				alert("Invalid Userobject Detected!");
				dispatch(handleDeAuthentication());
			}
		}
		catch(err) {
			alert("Changes Cannot Be Saved,please try again!");
		}
	}
}

export function handleSaveButton(fname, lname, userobject) {
	return async dispatch => {
		try {
			if (EFRapi.ValidateObject(userobject)) {
				const newObject = Object.assign({}, userobject, {
					user_data: {
						...userobject.user_data,
						first_name: fname,
						last_name: lname
					}
				});
				dispatch(handleUserObjectUpdate(iCookie.getStorage("token", true), newObject));
			}
			else {
				alert("Invalid Object Detected!");
				dispatch(handleDeAuthentication());
			}
		}
		catch(err) {
			dispatch(handleDeAuthentication());
		}
	}
}

export function handleSignUp(userinfo) {
	return async dispatch => {
		try {
			dispatch(SigningUp(true));
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "flex";
			const result = await EFRapi.signup(userinfo);
			console.log(result);
			if (result.data.code === 201) {
				dispatch(SignedUp());
				dispatch(SignUp(false));
			}
			else {
				dispatch(Error(error.SIGNUP_FAIL));
			}
			Loading.style.display = "none";
			dispatch(SigningUp(false));
		}
		catch(err) {
			dispatch(SigningUp(false));
			const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
			Loading.style.display = "none";
			dispatch(Error((err.message.indexOf("timeout") >= 0 ? error.TIME_OUT : error.CONN_FAIL)));
		}
	}
}

export function handlePasswordResetButton(uid, email) {
	return async dispatch => {
		try {
			if (uid && email) {
				const result = await EFRapi.resetPWRequest({username: uid, email: email});
				if (result.data.code === 201) {
					dispatch(handleDeAuthentication(null));
				}
			}
		}
		catch(err) {
			alert("Request time out, please try again!");
		}
	}
}

export function handleResendEmailVerify(uid, email) {
	return async dispatch => {
		try {
			if (uid && email) {
				
			}
			else {

			}
		}
		catch(err) {

		}
	}
}

export function handlePasswordRest(verifyID, pw) {
	return async dispatch => {
		try {
			dispatch(Error());
			if (verifyID && pw) {
				const result = await EFRapi.resetPW({VerifyID: verifyID, password: pw});
				if (result.data.code !== 100) {
					window.location.href = "http://52.40.134.152/login";
				}
				else {
					dispatch(Error("INVALID_ID"));
				}
			}
		}
		catch(err) {
			if (alert("Request time out, please try again...")) {
				window.location.reload();
			}
		}
	}
}

export function handleTokenRenew(token) {
	return async dispatch => {
		try {
			if (token) {

			}
		}
		catch(err) {

		}
	}
}

export function handleUsernameCheck(uid) {
	return async dispatch => {
		try {
			if (uid) {

			}
			else {

			}
		}
		catch(err) {

		}
	}
}

export function handleGameRestart(userobject) {
	return async dispatch => {
		dispatch(Error());
		dispatch(resetGame());
		const newObject = Object.assign({}, userobject, {
			...userobject,
			game_data: {
				...userobject.game_data,
				subject_id: 1,
				difficulty: 0,
				totalQuestions: 0,
				totalDonated: 0,
				completed_blocks: []
			}
		});
		if (EFRapi.ValidateObject(newObject)) {
			dispatch(handleUserObjectUpdate(newObject));
		}
	}
}