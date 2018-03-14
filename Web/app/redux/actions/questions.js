import "babel-polyfill";
import * as Types from "../types";
import { setComplete, getQuestions, setUserObject } from "./user";
import iCookie from "../../libraries/iCookie";
import efrApi from "../../libraries/efrApi";
import { handlerDeAuth, DeAuthenticate } from "./auth";
import { Reset } from "./state";

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
        if (efrApi.ValidateObject(user.userobject)) {
            const result = await efrApi.updateUser(user);
            if (result.data.response === "Success") {
                iCookie.setStorage("userobject", result.data.userobject);
                dispatch(setUserObject(result.data.userobject));
            }
        }
        else {
            alert("Trying to update an Invalid User Object!");
            dispatch(Error("INVALID_USEROBJECT"));
            dispatch(handlerDeAuth(null));
        }
    }
}

export function displayHelp() {
    return {
        type: Types.SHOW_HELP
    }
}

export function hideHelp() {
    return {
        type: Types.HIDE_HELP
    }
}

export function setHelpText(text) {
    return {
        type: Types.SET_HELP_TEXT,
        value: text
    }
}

export function getHelp(qid) {
    return async (dispatch)=>{
        try {
            const result = await efrApi.getHelp(qid);
            if (result.data.code) {
                dispatch(setHelpText(result.data.data));
            }
            else {
                dispatch(setHelpText("Error Fetching Help"));
            }
        }
        catch(err) {
            console.log("err", err);
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
                    difficulty: 0,
                    totalDonated: 0,
                    totalQuestions: 0
                }
            })
        }
        if (efrApi.ValidateObject(user.userobject)) {
            const result = await efrApi.updateUser(user);
            if (result.data.response === "Success") {
                iCookie.setStorage("userobject", result.data.userobject);
                dispatch(setUserObject(result.data.userobject));
                dispatch(resetIndex());
                dispatch(resetAnswer());
                dispatch(getQuestions(result.data.userobject));
            }
        }
        else {
            alert("Trying to update an Invalid User Object!");
            dispatch(Error("INVALID_USEROBJECT"));
            dispatch(handlerDeAuth(null));
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
        if (efrApi.ValidateObject(userobject)) {
            dispatch(updateCompletedBlock(userobject,qid));
            dispatch(resetIndex());
            dispatch(resetAnswer());
            dispatch(getQuestions(userobject));
        }
        else {
            alert("Trying to update an Invalid User Object!");
            dispatch(Error("INVALID_USEROBJECT"));
            dispatch(handlerDeAuth(null));
        }
    };
}

export function updateCompletedBlock(uo, qid) {
    return async (dispatch)=>{
        try {
            const user = {
                session: iCookie.get("session"),
                userobject: Object.assign({}, uo, {
                    game_data: {
                        ...uo.game_data,
                        completed_blocks: uo.game_data.completed_blocks.concat([qid])
                    }
                })
            };
            if (efrApi.ValidateObject(user)) {
                const result = await efrApi.updateUser(user);
                if (result.data.response === "Success") {
                    iCookie.set("userobject", result.data.userobject);
                    dispatch(setUserObject(result.data.userobject));
                }
                else if (result.data.action === "LOGOUT") {
                    dispatch(Reset());
                    iCookie.reset();
                    iCookie.removeStorage("userobject");
                    window.location.reload();
                }
            }
            else {
                alert("Trying to update an Invalid User Object!");
                dispatch(DeAuthenticate(null));
            }
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