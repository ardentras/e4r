import "babel-polyfill";
import * as Types from "../types";
import { setComplete, getQuestions } from "./user";
import efrApi from "../../libraries/efrApi";

export function setQuestions(value) {
    return {
        type: Types.SET_QUESTIONS,
        value: value
    }
}

export function nextIndex() {
    return {
        type: Types.NEXT_INDEX
    }
}

export function resetSelectedAnswer() {
    return {
        type: Types.RESET_SELECTEDANSWER
    }
}

export function nextQuestion() {
    return (dispatch)=>{
        dispatch(nextIndex());
        dispatch(resetSelectedAnswer());
    };
}

export function resetIndex() {
    return {
        type: Types.RESET_INDEX
    }
}

export function getNextBlock(qid, userobject) {
    return (dispatch)=>{
        dispatch(updateCompletedBlock(userobject));
        dispatch(resetIndex());
        dispatch(resetAnswer());
        dispatch(setComplete(qid));
        dispatch(getQuestions(userobject));
    };
}

export function updateCompletedBlock(user) {
    return async (dispatch)=>{
        try {
            //const result = await efrApi.renewSession(user);
        }
        catch(err) {
            console.log(err);
        }
    }
}

export function resetQuestions() {
    return {
        type: Types.RESET_QUESTION
    }
}

export function resetAnswer() {
    return {
        type: Types.RESET_ANSWER
    }
}

export function correctAnswer() {
    return {
        type: Types.CORRECT_ANSWER
    }
}

export function incorrectAnswer() {
    return {
        type: Types.INCORRECT_ANSWER
    }
}