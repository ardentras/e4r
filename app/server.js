const user = require('./user_actions');
const express = require('express');
const app = express();

app.get('/', (req, res) => res.send("Hello, World!\n"));

app.route('/user')
	.get((req, res) => {
		res.send('Lemme try to login\n');
		user.getstuff();		
	})
	.post((req, res) => {
		res.send('Lemme create a new user\n');
	});

app.listen(3000, () => console.log("Application now listening on port 3000"));