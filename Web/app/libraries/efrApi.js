/****************************************************************************
 * 
 *->Name: efrApi.js 
 *->Purpose: An object which contains api calls to interact with the e4r server.
 *
*****************************************************************************/

"use strict";
import "babel-polyfill";
import Axios from "axios";
import iCookie from "./iCookie";

const efrApi = (()=>{
    let _host = new WeakMap();
    let _port = new WeakMap();
    let _protocol = new WeakMap();
    let _gameRoute = new WeakMap();
    let _renewRoute = new WeakMap();
	class efrApi {
		constructor() {
            _host.set(this, "localhost");
            _port.set(this, 8080);
            _protocol.set(this, "http");
            _gameRoute.set(this, undefined);
            _renewRoute.set(this, undefined);
            
		}
		config({host="localhost", port=8080, protocol="http", gameRoute=undefined, renewRoute=undefined}) {
            _host.set(this, host);
            _port.set(this, port);
            _protocol.set(this, protocol);
            _gameRoute.set(this, gameRoute);
            _renewRoute.set(this, renewRoute);
		}
		createJSON({username=undefined, email=undefined, first_name=undefined, last_name=undefined, charity_name=undefined,
			subject_id=undefined, subject_name=undefined, difficulty=undefined, completed_blocks=undefined}) {
			return Promise.resolve(
                {
                userobject: {
                    user_data: {
                        username: username,
                        email: email,
                        first_name: first_name,
                        last_name: last_name,
                        charity_name: charity_name,
                        },
                    game_data: {
                        subject_id: subject_id,
                        subject_name: subject_name,
                        difficulty: difficulty,
                        completed_blocks: completed_blocks
                    }
                }
            });
        }
        getQuestions(user) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _gameRoute.get(this);
            return Axios.put(apiRoute, {user});
        }
        renewSession(user) {
            const session = iCookie.get("session");
            const objToSent = {user: {session: session, userobject: user}};
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _renewRoute.get(this);
            console.log(objToSent);
			return Axios.put(apiRoute, objToSent);
        }
	}
	return efrApi;
})();

export default new efrApi();
