import Home from "../home";
import Contact from "../contact";
import Login from "../login";
import Password from "../password";

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
	},
	{
		path: "/password_reset",
		label: "Password Reset",
		component: Password
	}
];

export default Routes;