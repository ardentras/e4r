## <a href='https://efr.firebaseapp.com'>Education for Revitalization Website</a>

## General Info
```
Version: Testing

Current URL: https://ef4.firebaseapp.com
Current Hosting: firebase
Current Library: react
```

## Settin up development eviroment

#### Required Node packages
```
Webapck - A bundler
Webpack-dev-server - A developement temporary server
Firebase-tools - Deploy to hosting site
React - UI Library
React-Dom - UI Library
React-router-dom - A routing library
Html-webpack-plugin
Axios - For Ajax/Http/Https requests
Css-loader - for Css stylesheets
babel-core - Transpiler for the latest javascript
babel-loader
babel-preset-env
babel-preset-react - Enable Transpiling for react
file-loader - Maybe not, depends on webpack is behaving towards relative path
babel-preset-stage-2 - This is not needed, it is for ES7 but I didn't use any of ES7 yet.
```
#### Steps to run website locally and Edit Layout

```
1. Install Nodejs - https://nodejs.org/en/ - Any of the two releases is fine.
2. Go to command line/terminal and change directory to the root folder of the app and dist folders. (ie /E4R/app, E4R is root)
3. Run command [ npm init ] - ignore the brackets
4. Run command [ npm i --save react react-dom react-router-dom ] - ignore the brackets
4. Run command [ npm i --save-dev babel-core babel-loader babel-preset-env babel-preset-react babel-preset-stage-2 file-loader html-webpack-plugin
   axios css-loader firebase-tools ] - ignore the brackets
5. Run command [ npm run start].
6. The website should be started in your default browser.
7. All edits in the file will be automatically refreshed in the browser with live refresh.
8. When ready to deploy website, Run the following commands
9. [ npm firebase-init ] - this initialize what service you wanna use, which is hosting.
10. [ npm run deploy ] - this will bundle all the files and deploy to the project in firebase.
8. ENJOY CODING!!!
```

