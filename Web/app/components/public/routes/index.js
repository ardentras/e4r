import Home from "../home";
import Contact from "../contact";
// import Login from "../login";
// import Private from "./private";

const Routes = [
	{
		path: "/",
		exact: true,
		label: "Home",
		component: Home 
	},
	{
		path: "/contacts",
		label: "Contact Us",
		component: Contact 
	},
	// {
	// 	path: "/login",
	// 	label: "Login",
	// 	component: Login 
	// },
	// {
	// 	path: "/dashboard",
	// 	label: "Dashboard",
	// 	component: Private
	// }
];

export default Routes;