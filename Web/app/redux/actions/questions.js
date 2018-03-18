import "babel-polyfill";

import Types from "../types";
import { Error } from "./state";
import error from "../errorCodes";
import EFRapi from "../../libraries/efrApi";
import http from "../httpCodes";
import FooterStyle from "../../components/private/footer/style.css";

export function setQuestions(questions) {
	return {
		type: Types.Questions.SET_QUESTIONS,
		value: questions
	}
}

export function setIndex(index) {
	return {
		type: Types.Questions.SET_INDEX,
		value: index
	}
}

export function showHelp() {
	return {
		type: Types.Questions.SHOW_HELP
	}
}

export function fetchingQuestions() {
	return {
		type: Types.Questions.FETCHING
	}
}

export function fetchingHelp() {
	return {
		type: Types.Questions.FETCHING_HELP
	}
}

export function fetchedHelp() {
	return {
		type: Types.Questions.FETCHED_HELP
	}
}

export function fetchedQuestions() {
	return {
		type: Types.Questions.FETCHED
	}
}

export function hideHelp() {
	return {
		type: Types.Questions.HIDE_HELP
	}
}

export function setHelpText(text) {
	return {
		type: Types.Questions.SET_HELP_TEXT,
		value: text
	}
}

export function setAnswer(check) {
	return {
		type: Types.Questions.SET_ANSWER,
		value: check
	}
}

export function resetQuestion() {
	return {
		type: Types.Questions.RESET
	}
}

export function getQuestions(token, userobject) {
	//Request Format
	// {
	// 	"user": {
	// 		"session":"{session_id}",
	// 		"userobject": {user_object},
	// 		"donated": "150"
	// 	}
	// }
	return async dispatch => {
		try {
			dispatch(fetchingQuestions());
			dispatch(Error());
			if (token && EFRapi.ValidateObject(userobject)) {
				if (EFRapi.ValidateObject(userobject)) {
					const result = await EFRapi.getQuestions({session: token, userobject: userobject});
					if (result.data.code === http.Ok) {
						dispatch(setQuestions(result.data.question_block));
					}
					else {
						dispatch(Error(error.FETCH_QUESTION_FAIL));
					}
				}
			}
			else {
				dispatch(Error(error.INVALID_OBJECT));
			}
			dispatch(fetchedQuestions());
		}
		catch(err) {
			dispatch(fetchedQuestions());
			dispatch(Error((err.message.indexOf("timeout") >= 0 ? error.TIME_OUT : error.CONN_FAIL)));
		}
	}
}

export function getHelp(qid) {
	return async dispatch => {
		try {
			if (qid) {
				dispatch(fetchingHelp());
				dispatch(Error());
				const result = await EFRapi.getHelp(qid);
				if (result.data.code === http.Ok) {
					dispatch(setHelpText(result.data.data));
					const helper = document.getElementById(Style.helptext);
					const helpbtn = document.getElementById(Style.helpbtn);
					if (helper && helpbtn) {
						helpbtn.style.background = "white";
						helpbtn.style.color = "#333F4F";
						helper.style.display = "none";
						helper.style.opacity = 0;
						dispatch(hideHelp());
					}
				}
				dispatch(fetchedHelp());
			}
		}
		catch(err) {
			dispatch(fetchedHelp());
			if (err.message.indexOf("timeout") >= 0) {
				dispatch(setHelpText(error.GET_HELP_TIMEOUT));
			}
			dispatch(Error((err.message.indexOf("timeout") >= 0 ? error.GET_HELP_TIMEOUT : error.GET_HELP_FAIL)));
		}
	}
}

export function handleNextBlock(token, userobject) {
	return async dispatch => {
		try {
			if (token && userobject) {
				if (EFRapi.ValidateObject(userobject)) {

				}
				else {

				}
			}
			else {
				
			}
		}
		catch(err) {

		}
	}
}