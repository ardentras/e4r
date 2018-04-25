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
	userobject: undefined,
	token: undefined,
	charities: ["American Red Cross", "United Way", "Salvation Army", "Direct Relief", "Wounded Warrior Project", "Feeding America", "Task Force for Global Health", "Leukemia and Lymphoma Society"]
};

const userReducer = (state=initialState, action) => {
	switch(action.type) {
	case Types.User.SET_FNAME:
		return Object.assign({}, state, {
			userobject: {
				...state.userobject,
				user_data: {
					...state.userobject.user_data,
					first_name: action.value
				}
			}
		});
	case Types.User.SET_LNAME:
		return Object.assign({}, state, {
			userobject: {
				...state.userobject,
				user_data: {
					...state.userobject.user_data,
					last_name: action.value
				}
			}
		});
	case Types.User.SOLVED_QUESTION: 
		return Object.assign({}, state, {
			userobject: {
				...state.userobject,
				game_data: {
					...state.userobject.game_data,
					totalQuestions: (parseInt(state.userobject.game_data.totalQuestions) + 1)
				}
			}
		});
	case Types.User.SET_SESSION_TOKEN:
		return Object.assign({}, state, {
			token: action.value
		});
	case Types.User.SET_USER_OBJECT:
		return Object.assign({}, state, {
			userobject: action.value
		});
	case Types.User.SET_COMPLETED:
		return Object.assign({}, state, {
			userobject: {
				...state.userobject,
				game_data: {
					...state.userobject.game_data,
					completed_blocks: state.userobject.game_data.completed_blocks.concat(action.value)
				}
			}
		});
	case Types.User.GAME_RESET:
		return Object.assign({}, state, {
			userobject: {
				...state.userobject,
				game_data: {
					...state.userobject.game_data,
					subject_id: 1,
					difficulty: 0,
					totalQuestions: 0,
					totalDonated: 0,
					completed_blocks: []
				}
			}
		});
	case Types.User.RESET:
		return Object.assign({}, state, initialState);
	default:
		return Object.assign({}, state);
	}
};

export default userReducer;
