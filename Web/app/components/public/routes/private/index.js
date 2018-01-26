import React from "react";
import {connect} from "react-redux";
import {
    Route,
    Redirect
  } from "react-router-dom";

const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={props => (
      rest.states.IS_AUTH ? (
        <Component {...props}/>
      ) : (
        <Redirect to={{
          pathname: '/login',
          state: { from: props.location }
        }}/>
      )
    )}/>
);

export default PrivateRoute;
