const Path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports = {
    entry: "./controls/UI.js",
    module: {
        rules: [
            {test: /\.(js)$/, exclude: /node_modules/, use: "babel-loader"},
            {test: /\.css$/, use: ["style-loader", "css-loader"]}
        ]
    },
    output: {
        path: Path.resolve(__dirname, ""),
        filename: "./controls/UI_bundle.js",
        publicPath: "/"
    },
    devServer: {
        historyApiFallback: true
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: "./src/index.html"
        })
    ]
}