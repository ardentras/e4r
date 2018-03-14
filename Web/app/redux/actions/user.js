import "babel-polyfill";
import * as Types from "../types";
import efrApi from "../../libraries/efrApi";
import { setQuestions } from "./questions";
import { handlerDeAuth } from "./auth";
import { Error } from "./state";
import iCookie from "../../libraries/iCookie";

export function getQuestions(userObject) {
    return async (dispatch)=>{
        try {
            if (efrApi.ValidateObject(userObject)) {
                const result = await efrApi.getQuestions({session: iCookie.get("session"), userobject: userObject});
                dispatch(setQuestions(result.data.question_block));
            }
            else {
                alert("Trying to retrieve questions with an Invalid User Object!");
                dispatch(Error("INVALID_USEROBJECT"));
                dispatch(handlerDeAuth(null));
            }

        }
        catch(err) {
            console.log("err",err);
        }
    };
}

export function resetPWRequest(user) {
    return async (dispatch)=>{
        try {
            const result = await efrApi.resetPWRequest({username: user.username, email: user.email});
            console.log("check", result.data);
        }
        catch(err) {
            console.log("err",err);
        }
    }
}

export function resetPW(user) {
    return async (dispatch)=>{
        try {
            dispatch(Error());
            const result = await efrApi.resetPW({VerifyID: user.id, password: user.pw});
            if (result.data.code !== 100) {
                window.location.href = "http://52.40.134.152/login";
            }
            else {
                dispatch(Error("INVALID_ID"));
            }
        }
        catch(err) {
            console.log("err",err);
        }
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

export function handleNames(fname, lname, object) {
    return async dispatch => {
        const user = {
            session: iCookie.get("session"),
            userobject: Object.assign({}, object, {
                user_data: {
                    ...object.user_data,
                    first_name: fname,
                    last_name: lname
                }
            })
        };
        if (efrApi.ValidateObject(user.userobject)) {
            const result = await efrApi.updateUser(user);
            if (result.data.response === "Success") {
                iCookie.setStorage("userobject", result.data.user_object);
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

export function setFname(fname) {
    return {
        type: Types.SET_F_NAME,
        value: fname
    }
}

export function setLname(lname) {
    return {
        type: Types.SET_L_NAME,
        value: lname
    }
}

