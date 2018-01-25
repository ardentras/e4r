import Home from "../home";
import Question from "../questions";
import Leaderboard from "../leaderboard";
import Settings from "../settings";
const Routes = [
	{
		path: "/dashboard",
		exact: true,
		label: "Home",
		component: Home 
	},
	{
		path: "/dashboard/questions",
		label: "Questions",
		component: Question 
	},
	{
		path: "/dashboard/leaderboard",
		label: "Leaderboard",
		component: Leaderboard 
	},
	{
		path: "/dashboard/settings",
		label: "Settings",
		component: Settings
	}
];

export default Routes;