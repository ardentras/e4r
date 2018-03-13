############################################
# Endpoint: check_username
# Action: Send unique username and email
# Expected Response: 200
############################################
code=$(curl -XPOST localhost:3002/api/check_username -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com"}}' | jq '.code')
if [[ ! $code -eq 200 ]]; then
    echo "- Test check username with non-existing user failed. Expected server code 200, got ${code}"
else
    echo "+ Test check username with non-existing user passed"
fi

############################################
# Endpoint: signup
# Action: Send valid unique username and email
# Expected Response: 201
############################################
curl -XPOST localhost:3002/api/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com","password":"defaultpass"}}' > response.json
code=$(cat response.json | jq '.code')
verifyid=($(cat response.json | jq '.verifyID'))
if [[ ! $code -eq 201 ]]; then
    echo "- Test sign up request with non-existing user failed. Expected server code 201, got ${code}"
else
    echo "+ Test sign up request with non-existing user passed"
fi

############################################
# Endpoint: check_username
# Action: Send existing username and email
# Expected Response: 100
############################################
code=$(curl -XPOST localhost:3002/api/check_username -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com"}}' | jq '.code')
if [[ ! $code -eq 100 ]]; then
    echo "- Test check username with existing user failed. Expected server code 100, got ${code}"
else
    echo "+ Test check username with existing user passed"
fi

############################################
# Endpoint: signup
# Action: Send existing username and email
# Expected Response: 100
############################################
code=$(curl -XPOST localhost:3002/api/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 100 ]]; then
    echo "- Test sign up request with existing user failed. Expected server code 100, got ${code}"
else
    echo "+ Test sign up request with existing user passed"
fi

############################################
# Endpoint: signup
# Action: Send invalid email
# Expected Response: 500
############################################
code=$(curl -XPOST localhost:3002/api/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@!gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 500 ]]; then
    echo "- Test sign up request with invalid email failed. Expected server code 500, got ${code}"
else
    echo "+ Test sign up request with invalid email passed"
fi

############################################
# Endpoint: signup
# Action: Send invalid username
# Expected Response: 500
############################################
code=$(curl -XPOST localhost:3002/api/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde!12345","email":"abcde12345@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 500 ]]; then
    echo "- Test sign up request with invalid username failed. Expected server code 500, got ${code}"
else
    echo "+ Test sign up request with invalid username passed"
fi

############################################
# Endpoint: login
# Action: Send invalid username
# Expected Response: 401
############################################
code=$(curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde1234567890@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test log in request with invalid username failed. Expected server code 401, got ${code}"
else
    echo "+ Test log in request with invalid username passed"
fi

############################################
# Endpoint: login
# Action: Send valid user w/ unverified email
# Expected Response: 428
############################################
code=$(curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpasssssss"}}' | jq '.code')
if [[ ! $code -eq 428 ]]; then
    echo "- Test log in request with unverified email failed. Expected server code 428, got ${code}"
else
    echo "+ Test log in request with unverified email passed"
fi

############################################
# Endpoint: verify_email
# Action: Send invalid verifyID
# Expected Response: 100
############################################
code=$(curl -s -XGET localhost:3002/api/verify_email/000000000 | jq '.code')
if [[ ! $code -eq 100 ]]; then
    echo "- Test verify email request with invalid id failed. Expected server code 100, got ${code}"
else
    echo "+ Test verify email request with invalid id passed"
fi

############################################
# Endpoint: verify_email
# Action: Send invalid verifyID
# Expected Response: 100
############################################
code=$(curl -s -o /dev/null -w "%{http_code}" localhost:3002/api/verify_email/${verifyid[0]})
if [[ ! $code -eq 302 ]]; then
    echo "- Test verify email request with valid id failed. Expected server code 302, got ${code}"
else
    echo "+ Test verify email request with valid id passed"
fi

############################################
# Endpoint: login
# Action: Send invalid password, valid user
# Expected Response: 401
############################################
code=$(curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpasssssss"}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test log in request with invalid password failed. Expected server code 401, got ${code}"
else
    echo "+ Test log in request with invalid password passed"
fi

############################################
# Endpoint: login
# Action: Send valid username and password
# Expected Response: 200, session token
############################################
curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpass"}}' > response.json
code=$(cat response.json | jq '.code')
session_token=$(cat response.json | jq '.session_id')
user_object=$(cat response.json | jq '.user_object')
if [[ -z $user_object ]]; then
    echo "- Test log in request with valid account failed. Response did not contain user object"
elif [[ -z $session_token ]]; then
    echo "- Test log in request with valid account failed. Response did not contain session token"
elif [[ ! $code -eq 200 ]]; then
    echo "- Test log in request with valid account failed. Expected server code 200, got ${code}"
else
    echo "+ Test log in request with valid account passed"
fi

############################################
# Endpoint: renew
# Action: Send invalid session token
# Expected Response: 401
############################################
code=$(curl -XPUT localhost:3002/api/renew -sH 'Content-Type: application/json' -d '{"user":{"session":"imnotval-idxx-xxxx-xxxx-xxxxxxxxxxxx"}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test renew session request with invalid token failed. Expected server code 401, got ${code}"
else
    echo "+ Test renew session request with invalid token passed"
fi

############################################
# Endpoint: renew
# Action: Send valid session token
# Expected Response: 200, session token
############################################
curl -XPUT localhost:3002/api/renew -sH 'Content-Type: application/json' -d '{"user":{"session":'$session_token'}}' > response.json
code=$(cat response.json | jq '.code')
session_token=$(cat response.json | jq '.session_id')
if [[ -z $session_token ]]; then
    echo "- Test renew session request with valid token failed. Response did not contain session token"
elif [[ ! $code -eq 200 ]]; then
    echo "- Test renew session request with valid token failed. Expected server code 200, got ${code}"
else
    echo "+ Test renew session request with valid token passed"
fi

############################################
# Endpoint: update_uo
# Action: Send invalid session token
# Expected Response: 401
############################################
code=$(curl -XPUT localhost:3002/api/update_uo -sH 'Content-Type: application/json' -d '{"user":{"session":"imnotval-idxx-xxxx-xxxx-xxxxxxxxxxxx", "userobject":{}}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test update user object request with invalid token failed. Expected server code 401, got ${code}"
else
    echo "+ Test update user object request with invalid token passed"
fi

############################################
# Endpoint: update_uo
# Action: Send user object with valid token and user object
# Expected Response: 200, user object
############################################
curl -XPUT localhost:3002/api/update_uo -sH 'Content-Type: application/json' -d '{"user":{"session":'$session_token', "userobject":{ "timestamp": "2018-03-07T05:56:15.033Z", "game_data": { "completed_blocks": [], "blocksRemaining": 0, "totalDonated": 0, "totalQuestions": 0, "difficulty": 0, "subject_id": 1, "subject_name": "" }, "user_data": { "favorite_charities": [ "" ], "selected_charity": "", "last_name": "", "first_name": "", "email": "abcde12345@gmail.com", "username": "abcde12345" } }}}' > response.json
code=$(cat response.json | jq '.code')
user_object=$(cat response.json | jq '.userobject')
if [[ -z $user_object ]]; then
    echo "- Test update user object with valid token and user object failed. Response did not contain user object"
elif [[ ! $code -eq 200 ]]; then
    echo "- Test update user object with valid token and user object failed. Expected server code 200, got ${code}"
else
    echo "+ Test update user object with valid token and user object passed"
fi

############################################
# Endpoint: delete_user
# Action: Send invalid session token
# Expected Response: 401
############################################
code=$(curl -XDELETE localhost:3002/api/delete_user -sH 'Content-Type: application/json' -d '{"user":{"session":"imnotval-idxx-xxxx-xxxx-xxxxxxxxxxxx"}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test delete user request with invalid session token failed. Expected server code 401, got ${code}"
else
    echo "+ Test delete user request with invalid session token passed"
fi

############################################
# Endpoint: delete_user
# Action: Send valid session token
# Expected Response: 200, then 401, user invalid
############################################
code=$(curl -XDELETE localhost:3002/api/delete_user -sH 'Content-Type: application/json' -d '{"user":{"session":'$session_token'}}' | jq '.code')
if [[ ! $code -eq 200 ]]; then
    echo "- Test delete user request with valid session token failed. Expected server code 200, got ${code}"
else
    code=$(curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpass"}}' | jq '.code')
    if [[ ! $code -eq 401 ]]; then
        echo "- Test delete user request with valid session token failed. Expected server code 401, got ${code}"
    else
        echo "+ Test delete user request with valid session token passed"
    fi
fi

############################################
# Endpoint: resend_verify
# Action: Send nonexistant username
# Expected Response: 100
############################################
code=$(curl -XPOST localhost:3002/api/resend_verify -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com"}}' | jq '.code')
if [[ ! $code -eq 100 ]]; then
    echo "- Test resend verify request with nonexistant username failed. Expected server code 500, got ${code}"
else
    echo "+ Test resend verify request with nonexistant username passed"
fi

############################################

curl -XPOST localhost:3002/api/signup -o /dev/null -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com","password":"defaultpass"}}'

############################################
# Endpoint: resend_verify
# Action: Send existing username
# Expected Response: 100
############################################
curl -XPOST localhost:3002/api/resend_verify -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com"}}' > response.json
code=$(cat response.json | jq '.code')
verifyid=($(cat response.json | jq '.verifyID'))
if [[ ! $code -eq 201 ]]; then
    echo "- Test resend verify request with existing username failed. Expected server code 201, got ${code}"
else
    echo "+ Test resend verify request with existing username passed"
fi

############################################

curl -XGET -s localhost:3002/api/verify_email/${verifyid[0]}
curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpass"}}' > response.json
session_token=$(cat response.json | jq '.session_id')

############################################
# Endpoint: logout
# Action: Send invalid session token
# Expected Response: 500
############################################
code=$(curl -XPUT localhost:3002/api/logout -sH 'Content-Type: application/json' -d '{"user":{"session":"imnotval-idxx-xxxx-xxxx-xxxxxxxxxxxx", "userobject":{}}}' | jq '.code')
if [[ ! $code -eq 500 ]]; then
    echo "- Test logout request with invalid session token failed. Expected server code 500, got ${code}"
else
    echo "+ Test logout request with invalid session token passed"
fi

############################################
# Endpoint: logout
# Action: Send out of date user object
# Expected Response: 409
############################################
code=$(curl -XPUT localhost:3002/api/logout -sH 'Content-Type: application/json' -d '{"user":{"session":'$session_token', "userobject":{ "timestamp": "1970-01-01T00:00:00.000Z", "game_data": { "completed_blocks": [], "blocksRemaining": 0, "totalDonated": 0, "totalQuestions": 0, "difficulty": 0, "subject_id": 1, "subject_name": "" }, "user_data": { "favorite_charities": [ "" ], "selected_charity": "", "last_name": "", "first_name": "", "email": "abcde12345@gmail.com", "username": "abcde12345" } }}}'  | jq '.code')
if [[ ! $code -eq 409 ]]; then
    echo "- Test logout request with out of date user object failed. Expected server code 409, got ${code}"
else
    echo "+ Test logout request with out of date user object passed"
fi

############################################
# Endpoint: logout
# Action: Send valid session token
# Expected Response: 200
############################################
code=$(curl -XPUT localhost:3002/api/logout -sH 'Content-Type: application/json' -d '{"user":{"session":'$session_token', "userobject":{ "timestamp": "2018-03-07T05:56:15.033Z", "game_data": { "completed_blocks": [], "blocksRemaining": 0, "totalDonated": 0, "totalQuestions": 0, "difficulty": 0, "subject_id": 1, "subject_name": "" }, "user_data": { "favorite_charities": [ "" ], "selected_charity": "", "last_name": "", "first_name": "", "email": "abcde12345@gmail.com", "username": "abcde12345" } }}}' | jq '.code')
if [[ ! $code -eq 200 ]]; then
    echo "- Test logout request with valid session token failed. Expected server code 200, got ${code}"
else
    echo "+ Test logout request with valid session token passed"
fi

############################################

curl -XPOST localhost:3002/api/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpass"}}' > response.json
session_token=$(cat response.json | jq '.session_id')

# Clean up mess
rm response.json
