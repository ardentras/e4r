/****************************************************************************
 * 
 *->Name: Store.js 
 *->Purpose: Declare the Redux Store/State tree and Reducer.
 *
*****************************************************************************/

import {createStore, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import stateReducer from "./reducers/stateReducer";
import userReducer from "./reducers/userReducer";
import questionReducer from "./reducers/questionReducer";
import messageReducer from "./reducers/messageReducer";
import { combineReducers } from "redux";

/****************************************************************************
 * 
 *->Reducer Name: reducer
 *->Reducer Purpose: To handle the actions of User login.
 *
*****************************************************************************/
const reducer = combineReducers({
	state: stateReducer,
	user: userReducer,
	questions: questionReducer,
	messages: messageReducer
});

/****************************************************************************
 * 
 *->Function Name: middleware
 *->Function Purpose: To apply the thunk middleware to redux, allowing functions to be dispatched.
 *
*****************************************************************************/
const middleware = applyMiddleware(thunk);

export default createStore(reducer, middleware);