/****************************************************************************
 * 
 *->Name: Store.js 
 *->Purpose: Declare the Redux Store/State tree and Reducer.
 *
*****************************************************************************/

import {createStore, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import authReducer from "./reducers/authReducer";
import stateReducer from "./reducers/stateReducer";
import userReducer from "./reducers/userReducer";
import questionReducer from "./reducers/questionReducer";
import { combineReducers } from "redux";

/****************************************************************************
 * 
 *->Reducer Name: reducer
 *->Reducer Purpose: To handle the actions of User login.
 *
*****************************************************************************/
const reducer = combineReducers({
	auth: authReducer,
	state: stateReducer,
	user: userReducer,
	questions: questionReducer
});

/****************************************************************************
 * 
 *->Function Name: middleware
 *->Function Purpose: To apply the thunk middleware to redux, allowing functions to be dispatched.
 *
*****************************************************************************/
const middleware = applyMiddleware(thunk);

export default createStore(reducer, middleware);