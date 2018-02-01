/****************************************************************************
 * 
 *->File Name: authReducer.js
 *->File Purpose: To declare a reducer for authenticating.
 *
*****************************************************************************/

import * as Types from "../types";
/****************************************************************************
 * 
 *->Object Name: initialState
 *->Object Purpose: To set the initial state of the store.
 *
*****************************************************************************/
const initialState = {
	user_data: undefined,
	game_data: undefined,
	timestamp: undefined
};

const userReducer = (state=initialState, action) => {
	switch(action.type) {
	case Types.SET_USER_OBJECT:
		return Object.assign({}, state, action.value);
	case Types.SET_COMPLETED:
		return Object.assign({}, state, {
			game_data: {
				...state.game_data,
				completed_blocks: state.game_data.completed_blocks.concat(action.value)
			}
		})
	default:
		return Object.assign({}, state);
	}
};

export default userReducer;
