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
	questions: [],
	index: 0,
	selectedAnswer: undefined,
	showHelp: false,
	helpText: undefined
};

const authReducer = (state=initialState, action) => {
	switch(action.type) {
	case Types.SET_QUESTIONS:
		return Object.assign({}, state, {
			questions: action.value
		});
	case Types.NEXT_INDEX:
		return Object.assign({}, state, {
			index: state.index + 1,
			showHelp: false
		});
	case Types.RESET_INDEX:
		return Object.assign({}, state, {
			index: 0
		});
	case Types.RESET_QUESTION:
		return Object.assign({}, initialState);
	case Types.CORRECT_ANSWER:
		return Object.assign({}, state, {
			selectedAnswer: "correct"
		});
	case Types.SET_HELP_TEXT:
		return Object.assign({}, state, {
			helpText: action.value
		});
	case Types.INCORRECT_ANSWER:
		return Object.assign({}, state, {
			selectedAnswer: "incorrect"
		});
	case Types.RESET_ANSWER:
		return Object.assign({}, state, {
			selectedAnswer: undefined
		});
	case Types.RESET_SELECTEDANSWER:
		return Object.assign({}, state, {
			selectedAnswer: undefined
		});
	case Types.SHOW_HELP:
		return Object.assign({}, state, {
			showHelp: true
		});
	case Types.HIDE_HELP:
		return Object.assign({}, state, {
			showHelp: false
		});
	default:
		return Object.assign({}, state);
	}
};

export default authReducer;
