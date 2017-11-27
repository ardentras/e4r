const express = require('express');
const app = express();
const Router = express.Router();

const bodyParser = require('body-parser');
const TDatabase = require('./database');
const DB_CONFIG = require('./configurations/config').DB_CONFIG;
const db = new TDatabase(DB_CONFIG);
const port = 3002;

//Local Testing
// const cors = require('cors');
// app.use(cors());

//BASIC REST API
//GET - List/Retrieve
//PUT - Replace/Update
//POST - Create New
//DELETE - Removal/Erase

//Prepend Api path to all HTTP Request
app.use('/api', Router);

function terminator(sig) {
	if (typeof sig === "string") {
		console.log('Received %s - terminating Node server ...', sig);
		db.gracefulShutdown();
		process.exit(1);
	};
	console.log('Node server stopped.');
};

function terminatorSetup(element, index, array) {
	process.on(element, function() { terminator(element); });
};

['SIGHUP', 'SIGINT', 'SIGQUIT', 'SIGILL', 'SIGTRAP', 'SIGABRT', 'SIGBUS', 'SIGFPE', 'SIGUSR1', 'SIGSEGV', 'SIGUSR2', 'SIGPIPE', 'SIGTERM'].forEach(terminatorSetup);

process.on('exit', function() { terminator(); });

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

Router.get('/', (req, res)=>{
	res.send('Welcome to the API');
});
Router.post('/signup', (req, res) => {
    db.createAccount(res, req.body.user);
});
Router.post('/login', (req,res) => {
    db.attemptLogin(res, req.body.user);
});
Router.put('/renew', (req, res) => {
	db.renewSessionToken(res, req.body.user);
});
Router.put('/logout', (req, res) => {
	db.attemptLogout(res, req.body.user);
});
Router.get('/test/display', (req, res) => {
    db.displayUsers(res);
});

app.listen(port);
console.log("Running on port " + port);
