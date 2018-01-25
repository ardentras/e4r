/****************************************************************************
 * 
 *->File Name: authReducer.js
 *->File Purpose: To declare a reducer for authenticating.
 *
*****************************************************************************/

import { 
	SET_USER_INFO,
	SET_SESSION_TOKEN } from "../../types/types";

/****************************************************************************
 * 
 *->Object Name: initialState
 *->Object Purpose: To set the initial state of the store.
 *
*****************************************************************************/
const initialState = {
	username: undefined,
	email: undefined,
	password: undefined,
	session: undefined
};

const authReducer = (state=initialState, action) => {
	switch(action.type) {
	case SET_USER_INFO:
		return Object.assign({}, state, {
			email: action.email,
			username: action.username,
			password: action.password
		});
	case SET_SESSION_TOKEN:
		return Object.assign({}, state, {
			session: action.session
		});
	default:
		return Object.assign({}, state);
	}
};

export default authReducer;
