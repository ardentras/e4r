const express = require('express');
const app = express();
const Router = express.Router();

//Generate private key file and certificate file
// openssl req -new -newkey rsa:2048 -nodes -out mydomain.csr -keyout private.key
// openssl x509 -req -days 365 -in mydomain.csr -signkey private.key -out mydomain.crt

const bodyParser = require('body-parser');
const fs = require('fs');
const http = require('http');
const https = require('https');
const TDatabase = require('./database');
const DB_CONFIG = require('./configurations/config').DB_CONFIG;
const USE_HTTPS = require('./configurations/config').USE_HTTPS;
const db = new TDatabase(DB_CONFIG);
const HTTPport = 3002;
const HTTPsport = 3003;

//BASIC REST API
//GET - List/Retrieve
//PUT - Replace/Update
//POST - Create New
//DELETE - Removal/Erase

//Prepend Api path to all HTTP Request and HTTPS request

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

Router.all('/', (req, res)=>{
	console.log('test');
	res.send('Welcome to the API');
});
Router.post('/signup', (req, res) => {
	console.log('ok');
    db.createAccount(res, req.body.user);
});
Router.post('/login', (req, res) => {
	console.log('ok');
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

app.use('/api', Router);
http.createServer(app).listen(HTTPport);
console.log("HTTP running on port " + HTTPport);

if (USE_HTTPS) {
	const key = fs.readFileSync('./encryption/private.key');
	const cert = fs.readFileSync( './encryption/mydomain.crt' );

	const options = {
		key: key,
		cert: cert,
	};
	
	https.createServer(options, app).listen(HTTPsport);
	console.log("HTTPs running on port " + HTTPsport);
}
