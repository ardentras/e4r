import Home from "../home";
import Contact from "../contact";
import Login from "../login";

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
	{
		path: "/login",
		label: "Login",
		component: Login 
	}
];

export default Routes;