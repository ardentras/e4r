const express = require('express');
const app = express();
const cors = require('cors');
const bodyParser = require('body-parser');
const TDatabase = require('./TDatabase');

const HOST = "e4rdb.cz5nhcw7ql0u.us-west-2.rds.amazonaws.com";
const DB_PORT = "1433";
const DB_USER = "cooluser";
const DB_PW = "coolpassword";

const db = new TDatabase(
    HOST,
    DB_PORT,
    DB_USER,
    DB_PW);

//BASIC REST API
//GET - List/Retrieve
//PUT - Replace/Update
//POST - Create New
//DELETE - Removal/Erase

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
// app.use(cors());

app.get('/', (req, res)=>{
    console.log('Welcome to API');
});
app.post('/api/login', (req,res) => {
    db.LogIn(res, req.body.user);
});
app.post('/api/signup', (req, res) => {
    db.AccountCreation(res, req.body.user);
});
app.get('/api/test/display', (req, res) => {
    db.Display(res);
});

 app.listen(2000);
console.log("Running on Port 2000");
