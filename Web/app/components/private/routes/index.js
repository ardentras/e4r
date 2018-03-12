import React from "react";
import Home from "../home";
import Question from "../questions";
import Leaderboard from "../leaderboard";
import Settings from "../settings";
import Password from "../password";
const iconStyle = {
	padding: "0 10px",
	margin: "10px 0"
};

const Routes = [
	{
		path: "/dashboard",
		exact: true,
		label: "Home",
		component: Home,
		icon: <i className={["fa", "fa-home", "icons"].join(" ")}  style={iconStyle} aria-hidden="true"/>
	},
	{
		path: "/dashboard/questions",
		label: "Questions",
		component: Question,
		icon: <i className={["fa", "fa-superpowers", "icons"].join(" ")}  style={iconStyle} aria-hidden="true"/>
	},
	{
		path: "/dashboard/rankings",
		label: "Ranks",
		component: Leaderboard,
		icon: <i className={["fa", "fa-users", "icons"].join(" ")}  style={iconStyle} aria-hidden="true"/>
	},
	{
		path: "/dashboard/settings",
		label: "Settings",
		component: Settings,
		icon: <i className={["fa", "fa-cogs", "icons"].join(" ")} style={iconStyle} aria-hidden="true"/>
	},
	{
		path: "/dashboard/password-reset",
		label: "Password Reset",
		component: Password,
		icon : null
	}
];

export default Routes;