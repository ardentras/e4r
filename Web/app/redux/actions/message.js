import Types from "../types";

export function setMessage(data) {
	return {
		type: Types.Messages.SET_MESSAGE,
		value: data
	}
}

export function resetMessages() {
	return {
		type: Types.Messages.RESET
	}
}

export function setTotalUser(count) {
	return {
		type: Types.Messages.SET_TOTAL_USER,
		value: count
	}
}

export function initSocketHanlder(socket) {
	return async dispatch => {
		if (socket) {
			socket.on("new-message", (data)=>{
				dispatch(setMessage(data));
			});
			socket.on("user-connected", (data)=>{
				console.log("Connected");
				dispatch(setTotalUser(data));
			});
			socket.on("user-disconnected",(data)=>{
				dispatch(setTotalUser(data));
			});	
		}
	}
}

