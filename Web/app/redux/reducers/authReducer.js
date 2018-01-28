/****************************************************************************
 * 
 *->File Name: authReducer.js
 *->File Purpose: To declare a reducer for authenticating.
 *
*****************************************************************************/

import { SET_UID } from "../types";

/****************************************************************************
 * 
 *->Object Name: initialState
 *->Object Purpose: To set the initial state of the store.
 *
*****************************************************************************/
const initialState = {
	uid: undefined,
};

const authReducer = (state=initialState, action) => {
	switch(action.type) {
	case SET_UID:
		return Object.assign({}, state, {
			uid: action.value
		});
	default:
		return Object.assign({}, state);
	}
};

export default authReducer;
