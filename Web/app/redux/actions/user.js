import "babel-polyfill";
import * as Types from "../types";
import efrApi from "../../libraries/efrApi";
import { setQuestions } from "./questions";
import iCookie from "../../libraries/iCookie";

export function getQuestions(userObject) {
    return async (dispatch)=>{
        try {
            const result = await efrApi.getQuestions({session: iCookie.get("session"), userobject: userObject});
            dispatch(setQuestions(result.data));
        }
        catch(err) {
            console.log("err",err);
        }
    };
}

export function setSessionToken(token) {
    return {
        type: Types.SET_SESSION_TOKEN,
        value: token
    }
}

export function setUserObject(object) {
    return {
        type: Types.SET_USER_OBJECT,
        value: object
    }
}

export function setComplete(qid) {
    return {
        type: Types.SET_COMPLETED,
        value: [qid]
    }
}