const express = require('express');
const app = express();
const cors = require('cors');
const bodyParser = require('body-parser');
const TDatabase = require('./database');

const DB_HOST = "e4rdb.cz5nhcw7ql0u.us-west-2.rds.amazonaws.com";
const DB_PORT = "1433";
const DB_USER = "cooluser";
const DB_PW = "coolpassword";
const DB_NAME = "testdb";

const db = new TDatabase(
    DB_HOST,
    DB_PORT,
    DB_USER,
    DB_PW,
	DB_NAME);

const port = 3002;

//BASIC REST API
//GET - List/Retrieve
//PUT - Replace/Update
//POST - Create New
//DELETE - Removal/Erase

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
// app.use(cors());

app.get('/', (req, res)=>{
    console.log('Welcome to API');
	res.send('Welcome to the API');
});
app.post('/api/login', (req,res) => {
    db.attemptLogin(res, req.body.user);
});
app.post('/api/signup', (req, res) => {
    db.createAccount(res, req.body.user);
});
app.get('/api/test/display', (req, res) => {
    db.displayUsers(res);
});

app.listen(port);
console.log("Running on port " + port);
