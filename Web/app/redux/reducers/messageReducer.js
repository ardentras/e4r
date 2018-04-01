/****************************************************************************
 * 
 *->File Name: authReducer.js
 *->File Purpose: To declare a reducer for authenticating.
 *
*****************************************************************************/

import Types from "../types";

/****************************************************************************
 * 
 *->Object Name: initialState
 *->Object Purpose: To set the initial state of the store.
 *
*****************************************************************************/
const initialState = {
	totalUsers: 0
};

const messageReducer = (state=initialState, action) => {
	switch(action.type) {
	case Types.Messages.SET_TOTAL_USER:
		return Object.assign({}, state, {
			totalUsers: action.value
		});
	default:
		return Object.assign({}, state);
	}
};

export default messageReducer;
