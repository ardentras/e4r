base_url="localhost:3002/api"

code=$(curl -XGET ${base_url}/check_username -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com"}}' | jq '.code')
if [[ ! $code -eq 200 ]]; then
    echo "- Test check username with non-existing user failed. Expected server code 200, got ${code}"
else
    echo "+ Test check username with non-existing user passed"
fi

code=$(curl -XPOST ${base_url}/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 201 ]]; then
    echo "- Test sign up request with non-existing user failed. Expected server code 201, got ${code}"
else
    echo "+ Test sign up request with non-existing user passed"
fi

code=$(curl -XGET ${base_url}/check_username -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com"}}' | jq '.code')
if [[ ! $code -eq 100 ]]; then
    echo "- Test check username with existing user failed. Expected server code 100, got ${code}"
else
    echo "+ Test check username with existing user passed"
fi

code=$(curl -XPOST ${base_url}/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 100 ]]; then
    echo "- Test sign up request with existing user failed. Expected server code 100, got ${code}"
else
    echo "+ Test sign up request with existing user passed"
fi

code=$(curl -XPOST ${base_url}/signup -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345","email":"abcde12345@!gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 500 ]]; then
    echo "- Test sign up request with invalid email failed. Expected server code 500, got ${code}"
else
    echo "+ Test sign up request with invalid email passed"
fi

code=$(curl -XPOST ${base_url}/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde1234567890@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test log in request with invalid username failed. Expected server code 401, got ${code}"
else
    echo "+ Test log in request with invalid username passed"
fi

code=$(curl -XPOST ${base_url}/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpasssssss"}}' | jq '.code')
if [[ ! $code -eq 401 ]]; then
    echo "- Test log in request with invalid password failed. Expected server code 401, got ${code}"
else
    echo "+ Test log in request with invalid password passed"
fi

code=$(curl -XPOST ${base_url}/login -sH 'Content-Type: application/json' -d '{"user":{"username":"abcde12345@gmail.com","password":"defaultpass"}}' | jq '.code')
if [[ ! $code -eq 200 ]]; then
    echo "- Test log in request with valid account failed. Expected server code 200, got ${code}"
else
    echo "+ Test log in request with valid account passed"
fi
