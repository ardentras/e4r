# Education for Revitalization - Server
current deployed server code of Education for Revitalization.

## Server Infomation
*note: SSL is not functional due to restriction of aws domain. 

| Protocol | IP | PORT |
| --- | --- | --- |
| HTTP | 35.163.221.182 | 3002 |
| HTTPS | 35.163.221.182 | 3003 |

## Current Apis
| APIs | Link |
| --- | --- |
| GET | <a href="#get-routes">View all GET routes</a> |
| POST | <a href="#post-routes">View all POST routes</a> | 
| PUT | <a href="#put-routes">View all PUT routes</a> |
| DELETE | <a href="#delete-routes">View all DELETE routes</a> |

## Routes

#### <ul><li>GET Routes</li></ul>
| Name | Route | Description | Fields | Response |
| --- | --- | --- | --- | --- |
| <a href="#verify-email">Verify Email</a> | /api/verify_email/${VerifyID} | Verify a user's email after signup | NA | Redirect |

#### <ul><li>POST Routes</li></ul>
| Name | Route | Description | Fields | Response |
| --- | --- | --- | --- | --- |
| <a href="#login">Login</a> | /api/login | Verify user's uid and password | username, password | session_id, user_object |
| <a href="#signup">Sign Up</a> | /api/signup | Append user to database | username, email, password | verifyID |
| <a href="#check-username">Check Username</a> | /api/check_username | Check if the username/email exist | username, email | reason |

#### <ul><li>PUT Routes</li></ul>
| Name | Route | Description | Fields | Response |
| --- | --- | --- | --- | --- |
| <a href="#renew-session">Renew Session</a> | /api/renew | Renew a user's session token | session | session_id |
| <a href="#update-user-object">Update Userobject</a> | /api/update_uo | Update a user's user object | session, userobject | userobject |
| <a href="#logout">Logout</a> | /api/logout | Update user object and deauthorize user | session, userobject | reason |
| <a href="#request-questions">Request Question</a> | /api/q/request_block | Retrieve a user's question block | session, userobject | questions |

#### <ul><li>DELETE Routes</li></ul>
| Name | Route | Description | Fields | Response |
| --- | --- | --- | --- | --- |
| <a href="#remove-user">Remove User</a> | /api/delete_user | Remove a user from database | session | reason |

## Formatting

### Verify Email (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
http://${url}/api/verify_email/${verifyID}
```

<ul>
    <li>Response:</li>      
</ul>

```
On invalid verify ID:
{
    response: "Failed",
    type: "GET",
    code: 100,
    reason: "That ID was not found."
}

On valid verify ID:
    HTTP 302: Redirects to /login
```

### Login (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
        "username": "test1 OR test@test.com",
        "password": "testpassword"
    }
}
```

<ul>
    <li>Response:</li>      
</ul>

```
On successful login:
{
  "response": "Success",
  "type": "POST",
  "code": 200,
  "action": "LOGIN",
  "session_id": "{session_token}",
  "user_object": "{}"
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

### Signup (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
      "username": "test1",
      "email": "test@test.com",
      "password": "testpassword"
    }
}
```



<ul>
    <li>Response:</li>      
</ul>

```
On successful signup:
{
  "response": "Succeed",
  "type": "POST",
  "code": 201,
  "action": "SIGNUP"
  "verifyID": ${verifyID}
}

On user already exists:
{
  "response": "Failed",
  "type": "POST",
  "code": 100,
  "reason": "User already exists",
}
```

### Check Username (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
      "username": "test1",
      "email": "test@test.com",
    }
}
```


<ul>
    <li>Response:</li>      
</ul>

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

### Renew Session (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
        "session": "{session_token}"
    }
}
```

<ul>
    <li>Response:</li>      
</ul>

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

### Update User Object (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
        "session": "{session_token}",
        "userobject": "{user_object}"
    }
}
```


<ul>
    <li>Response:</li>      
</ul>

```
On User Object current:
{
    response: "Success",
    type: "PUT",
    code: 200,
    action: "SAVE UO",
    userobject: {user_object}
}

On User Object out of date:
{
    response: "Success",
    type: "PUT",
    code: 200,
    action: "RETRIEVE UO",
    reason: "User object out of date. Retrieving from database.",
    userobject: {user_object}
}
```

### Logout (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
        "session": "{session_token}",
        "userobject": "{user_object}"
    }
}
```


<ul>
    <li>Response:</li>      
</ul>

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

### Request Questions (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
        "session":"{session_id}",
        "userobject": {user_object}
    }
}
```

<ul>
    <li>Response:</li>      
</ul>

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
    user_object: {user_object}
}
```

### Remove User (<a href="#education-for-revitalization---server">Top</a>)
<ul>
    <li>Request:</li>      
</ul>

```
{
    "user": {
        "session":"${session_token}"
    }
}
```

<ul>
    <li>Response:</li>      
</ul>

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
