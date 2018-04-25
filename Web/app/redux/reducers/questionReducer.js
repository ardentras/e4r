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
	questions: [],
	index: 0,
	answer: false,
	showHelp: false,
	fetching: false,
	fetched: false,
	fetching_help: false,
	fetched_help: false,
	helpText: undefined
};

const questionReducer = (state=initialState, action) => {
	switch(action.type) {
	case Types.Questions.FETCHING_HELP:
		return Object.assign({}, state, {
			fetching_help: true,
			fetched_help: false
		});
	case Types.Questions.FETCHED_HELP:
		return Object.assign({}, state, {
			fetching_help: false,
			fetched_help: true
		});
	case Types.Questions.FETCHED:
		return Object.assign({}, state, {
			fetched: true,
			fetching: false
		});
	case Types.Questions.FETCHING:
		return Object.assign({}, state, {
			fetching: true,
			fetched: false
		});
	case Types.Questions.SET_QUESTIONS:
		return Object.assign({}, state, {
			questions: action.value
		});
	case Types.Questions.SET_INDEX:
		return Object.assign({}, state, {
			index: action.value
		});
	case Types.Questions.SHOW_HELP:
		return Object.assign({}, state, {
			showHelp: true
		});
	case Types.Questions.SET_HELP_TEXT:
		return Object.assign({}, state, {
			helpText: action.value
		});
	case Types.Questions.SET_ANSWER:
		return Object.assign({}, state, {
			answer: action.value
		});
	case Types.Questions.HIDE_HELP:
		return Object.assign({}, state, {
			showHelp: false
		});
	case Types.Questions.RESET:
		return Object.assign({}, state, initialState);
	default:
		return Object.assign({}, state);
	}
};

export default questionReducer;
