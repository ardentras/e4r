## Getting Started with a RESTful API

```
GET: Retrieve Information
POST: Insert Information
PUT: Update Information
DELETE: Remove Information
```

## Using Education for Revitalization Api

```
SERVER PUBLIC IP: 34.216.143.255
SERVER PUBLIC HTTP PORT: 3002
SERVER PUBLIC HTTPS/SSL PORT: 3003

API Calls:
          API Welcome           -> hostname:port/api                -> ALL
          Sign Up               -> hostname:port/api/signup         -> POST
          Check Username        -> hostname:port/api/check_username -> POST
          Resend Verify         -> hostname:port/api/resend_verify  -> PUT
          Password Reset        -> hostname:port/api/reset_password -> POST
          Password Reset Verify -> hostname:port/api/verify_password_reset -> PUT
          Log In                -> hostname:port/api/login          -> POST
          Verify Email          -> hostname:port/api/verify_email/${VerifyID} -> GET
          Renew Session         -> hostname:port/api/renew          -> PUT
          Update User Object    -> hostname:port/api/update_uo      -> PUT
          Log Out               -> hostname:port/api/logout         -> PUT
          Delete User           -> hostname:port/api/delete_user    -> DELETE
          Bubble Live Feed      -> hostname:port/api/bubble_feed    -> GET
          Top Ten Questions     -> hostname:port/api/top_ten_q      -> GET
          Top Ten Money         -> hostname:port/api/top_ten_mon    -> GET
          Request Question Block-> hostname:port/api/q/request_block-> PUT
          Request Question Help -> hostname:port/api/q/request_help -> PUT

Debugging API Calls:
          Display Users Information -> hostname:port/test/display -> GET


```
## JSON Formats:

#### User Object Definition:
```
"userobject": {
    "user_data": {
        "username": "test1",
        "email": "test@test.com",
        "first_name": "John",
        "last_name": "Doe",
        "selected_charity": "",
        "favorite_charities": [""]
    },
    "game_data": {
        "subject_name": "Math",
        "subject_id": 1,
        "difficulty": 0,
        "totalQuestions": 0,
        "totalDonated": 0,
        "blocksRemaining": 0,
        "completed_blocks": [1, 3, 4]
    },
    "timestamp":"2018-01-24T02:06:58+00:00"
}
```

#### SIGN UP REQUEST:
```
{
    "user": {
      "username": "test1",
      "email": "test@test.com",
      "password": "testpassword"
    }
}
```
#### SIGN UP RESPONSE:
```
On successful signup:
{
  "response": "Succeed",
  "type": "POST",
  "code": 201,
  "action": "SIGNUP"
  "verifyID": {verifyID}
}

On user already exists:
{
  "response": "Failed",
  "type": "POST",
  "code": 100,
  "reason": "User already exists",
}
```
#### LOG IN REQUEST:
```
{
    "user": {
        "username": "test1 OR test@test.com",
        "password": "testpassword"
    }
}
```
#### LOG IN RESPONSE:
```
On successful login:
{
  "response": "Success",
  "type": "POST",
  "code": 200,
  "action": "LOGIN",
  "session_id": "{session_token}",
  "userobject": "{}"
}

On invalid username/email:
{
  "response": "Failed",
  "type": "POST",
  "code": 401,
  "reason": "User not found",
  "result": null
}

On invalid password:
{
    "response": "Failed",
    "type": "POST",
    "code": 401,
    "reason": "Invalid Password"
}

On unverified email:
{
    "response": "Failed",
    "type": "POST",
    "code": 428,
    "reason": "Email not verified, login failed"
}
```
#### VERIFY EMAIL REQUEST:
```
http://${url}/api/verify_email/${verifyID}
```
#### VERIFY EMAIL RESPONSE:
```
On invalid verify ID:
{
    response: "Failed",
    type: "GET",
    code: 100,
    reason: "That ID was not found."
}

On valid verify ID:
{
    response: "Success",
    type: "PUT",
    code: 200,
    reason: "Verification ID accepted"
}
```
#### RESEND VERIFY REQUEST:
```
{
    "user" : {
        "username": {username},
        "email": {email}
    }
}
```
#### RESEND VERIFY RESPONSE:
```
On invalid username and/or email:
{
    response: "Failed",
    type: "PUT",
    code: 100,
    action: "RESEND_VERIFY",
    reason: "That username/email was not found."
}

On valid username and email:
{
    response: "Succeed",
    type: "PUT",
    code: 201,
    action: "RESEND_VERIFY",
    verifyID: {verifyID}
}
```
#### PASSWORD RESET REQUEST:
```
{
    "user" : {
        "username": {username},
        "email": {email}
    }
}
```
#### PASSWORD RESET RESPONSE:
```
On invalid username and/or email:
{
    response: "Failed",
    type: "PUT",
    code: 100,
    action: "RESET_PASSWORD",
    reason: "That username/email was not found."
}

On valid username and email:
{
    response: "Succeed",
    type: "PUT",
    code: 201,
    action: "RESET_PASSWORD",
    verifyID: {verifyID}
}
```
#### VERIFY PASSWORD RESET REQUEST:
```
{
    "user": {
        "verifyid": {verifyID},
        "password": "defaultpass"
    }
}
```
#### VERIFY PASSWORD RESET RESPONSE:
```
On invalid verify ID:
{
    response: "Failed",
    type: "GET",
    code: 100,
    reason: "That ID was not found."
}

On valid verify ID:
Redirects to /login
```
#### SESSION RENEW REQUEST:
```
{
    "user": {
        "session": "{session_token}"
    }
}
```
#### SESSION RENEW RESPONSE:
```
On successful renewal:
{
    "response": "Success",
    "type": "PUT",
    "code": 200,
    "action": "RENEW_SESSION",
    "session_id": {session_token}
}

On invalid/expired session:
{
    "response": "Failed",
    "type": "PUT",
    "code": 401,
    "action": "LOGOUT",
    "reason": "User's session token is invalid."
}

On non-existant session:
{
    "response": "Failed",
    "type": "PUT",
    "code": 401,
    "action": "LOGOUT",
    "reason": "User's session token was not found."
}
```
#### LOGOUT REQUEST:
```
{
    "user": {
        "session": "{session_token}",
        "userobject": "{userobject}"
    }
}
```
#### LOGOUT RESPONSE:
```
On successful logout:
{
    "response": "Success",
    "type": "PUT",
    "code": 200,
    "action": "LOGOUT"
    "reason": "User successfully logged out."
}

On successful logout w/ invalid user object:
{
    response: "Success",
    type: "PUT",
    code: 409,
    action: "LOGOUT",
    reason: "Out of date user object cannot be saved. User logged out"
}

On invalid session:
{
    "response": "Failed",
    "type": "PUT",
    "code": 500,
    "reason": "Session invalid. User object could not be saved"
}
```
#### UPDATE_UO REQUEST:
```
{
    "user": {
        "session": "{session_token}",
        "userobject": "{userobject}"
    }
}
```
#### UPDATE_UO RESPONSE:
```
On User Object current:
{
    response: "Success",
    type: "PUT",
    code: 200,
    action: "SAVE UO",
    userobject: {userobject}
}

On User Object out of date:
{
    response: "Success",
    type: "PUT",
    code: 200,
    action: "RETRIEVE UO",
    reason: "User object out of date. Retrieving from database.",
    userobject: {userobject}
}
```
#### BUBBLE_FEED RESPONSE:
```
If active:
{
    response: "Success",
    type: "GET",
    code: 200,
    total: 5000
    data:   [
                {
                    username: ${username},
                    donated: 150,
                    timestamp: 158329855237
                    },
                {...},
            ]
}

If inactive, call will return 404
```
## Misc Requests:
#### CHECK_USERNAME REQUEST:
```
{
    "user": {
      "username": "test1",
      "email": "test@test.com",
    }
}
```
#### CHECK_USERNAME RESPONSE:
```
On user not found:
{
  "response": "Succeed",
  "type": "POST",
  "code": 200,
  "reason": "User not found"
}

On user already exists:
{
  "response": "Failed",
  "type": "POST",
  "code": 100,
  "reason": "User already exists",
}
```
#### DELETE_USER REQUEST:
```
{
    "user": {
        "session":"${session_token}"
    }
}
```
#### DELETE_USER RESPONSE:
```
On invalid session token:
{
    response: "Failed",
    type: "DELETE",
    code: 401,
    action: "DELETE_USER",
    reason: "Session invalid, user logged out."
}

On valid session token:
{
    response: "Success",
    type: "DELETE",
    code: 200,
    action: "DELETE_USER"
}
```
## Game Requests:
#### REQUEST_BLOCK REQUEST:
```
{
    "user": {
        "session":"{session_id}",
        "userobject": {userobject},
        "donated": "150"
    }
}

OR

{
    "user": {
        "session":"{session_id}",
        "userobject": {userobject},
        "donated": "150"
    },
    "game": {
        "questions": [
            {
                "QuestionID":{id},
                "QuestionText":"{text}",
                "QuestionOne":"{answer1}",
                "QuestionTwo":"{answer2}",
                "QuestionThree":"{answer3}",
                "QuestionFour":"{answer4}",
                "CorrectAnswer":"{correct_answer}",
                "StatsOne":"{statsAnswer1}",
                "StatsTwo":"{statsAnswer2}",
                "StatsThree":"{statsAnswer3}",
                "StatsFour":"{statsAnswer4}",
                "QuestionBlockID":"{block_id}"
            },
            {...},
        ]
    }
}
```
#### REQUEST_BLOCK RESPONSE:
```
{
    response: "Success",
    type: "PUT",
    code: 200,
    action: "RETRIEVE",
    question_block: [
        {
            "QuestionID":{id},
            "QuestionText":"{text}",
            "QuestionOne":"{answer1}",
            "QuestionTwo":"{answer2}",
            "QuestionThree":"{answer3}",
            "QuestionFour":"{answer4}",
            "CorrectAnswer":"{correct_answer}",
            "StatsOne":"{statsAnswer1}",
            "StatsTwo":"{statsAnswer2}",
            "StatsThree":"{statsAnswer3}",
            "StatsFour":"{statsAnswer4}",
            "QuestionBlockID":"{block_id}"
        },
        {...},
    ],
    userobject: {userobject}
}
```
#### REQUEST_HELP REQUEST:
```
{
    "question_id": 0
}
```
#### REQUEST_HELP RESPONSE:
```
On question contains help:
{
    response: "Success",
    type: "PUT",
    code: 200,
    action: "REQUEST_HELP",
    data: "{help_text}"}

One question doesn't have help:
{
    response: "Failed",
    type: "PUT",
    code: 100,
    action: "REQUEST_HELP",
    reason: "This question does not contain a help topic."
}
```
