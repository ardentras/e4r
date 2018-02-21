import "babel-polyfill";
import * as Types from "../types";
import { setComplete, getQuestions, setUserObject } from "./user";
import iCookie from "../../libraries/iCookie";
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

export function handleSolvedQuestions(count, object) {
    return async dispatch => {
        const user = {
            session: iCookie.get("session"),
            userobject: Object.assign({}, object, {
                game_data: {
                    ...object.game_data,
                    totalQuestions: count,
                }
            })
        };
        const result = await efrApi.updateUser(user);
        if (result.data.response === "Success") {
            dispatch(setUserObject(result.data.userobject));
        }
    }
}

export function handleCompleteReset(object) {
    return async dispatch => {
        const user = {
            session: iCookie.get("session"),
            userobject: Object.assign({}, object, {
                game_data: {
                    ...object.game_data,
                    completed_blocks: [],
                    difficulty: "0",
                    totalDonated: 0,
                    totalQuestions: 0
                }
            })
        }
        const result = await efrApi.updateUser(user);
        if (result.data.response === "Success") {
            dispatch(setUserObject(result.data.userobject));
            dispatch(resetIndex());
            dispatch(resetAnswer());
            dispatch(getQuestions(result.data.userobject));
        }
    };
}

export function resetCompleted() {
    return {
        type: Types.RESET_COMPLETE
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