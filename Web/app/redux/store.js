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
import { combineReducers } from "redux";

/****************************************************************************
 * 
 *->Reducer Name: reducer
 *->Reducer Purpose: To handle the actions of User login.
 *
*****************************************************************************/
const reducer = combineReducers({
	user: authReducer,
	state: stateReducer
});

/****************************************************************************
 * 
 *->Function Name: middleware
 *->Function Purpose: To apply the thunk middleware to redux, allowing functions to be dispatched.
 *
*****************************************************************************/
const middleware = applyMiddleware(thunk);

export default createStore(reducer, middleware);