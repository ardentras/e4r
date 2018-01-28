import { 
	AUTHENTICATE_SUCCESSFUL,
	DEAUTHENTICATE_SUCCESSFUL,
	ERROR, 
	SIGN_UP,
	SIGNUP_SUCCESSFUL,
	REFER,
	RESET } from "../types";
    
/****************************************************************************
 * 
 *->Action Name: AuthenticateSuccess
 *->Action Purpose: To set the successful state in the store.
 *
*****************************************************************************/
export function setAuthenticateSuccess(state) {
	return {
		type: AUTHENTICATE_SUCCESSFUL,
		value: state
	};
}

/****************************************************************************
 * 
 *->Action Name: DeAuthenticateSuccess
 *->Action Purpose: To set the success state in the store.
 *
*****************************************************************************/
export function setDeAuthenticateSuccess(state) {
	return {
		type: DEAUTHENTICATE_SUCCESSFUL,
		value: state
	};
}

/****************************************************************************
 * 
 *->Action Name: signUp
 *->Action Purpose: To set the signup state in the store.
 *
*****************************************************************************/
export function ifSignUp(state) {
	return {
		type: SIGN_UP,
		value: state
	};
}

/****************************************************************************
 * 
 *->Action Name: signUpSuccessful
 *->Action Purpose: To set the signup state in the store.
 *
*****************************************************************************/
export function setSignUpSuccessful(state) {
	return {
		type: SIGNUP_SUCCESSFUL,
		value: state
	};
}

export function Reset() {
	return {
		type: RESET
	}
}

export function Error(err=undefined) {
	return {
		type: ERROR,
		value: err
	};
}

export function Refer(value=true) {
	return {
		type: REFER,
		value: value
	};
}