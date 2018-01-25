/****************************************************************************
 * 
 *->Name: webpack.config.js 
 *->Purpose: Configuration file for webpack.
 *
*****************************************************************************/

const Path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const webpack = require("webpack");
// var BundleAnalyzerPlugin = require("webpack-bundle-analyzer").BundleAnalyzerPlugin;
// var WebpackBundleSizeAnalyzerPlugin = require("webpack-bundle-size-analyzer").WebpackBundleSizeAnalyzerPlugin;

const config = {
	entry: "./app/index.js",
	output: {
		path: Path.resolve(__dirname, "dist"), // eslint-disable-line no-undef
		filename: "index_bundle.js"
	},
	devtool: "inline-source-map",
	module: {
		rules: [
			{ test: /\.(js)$/, exclude: /node_modules/, use: "babel-loader" },
			{test: /\.css$/, loader: "style-loader!css-loader?modules=true&localIdentName=[name]__[local]__[hash:base64:5]"}
		]
	},
	devServer: {
		historyApiFallback: true
	},
	plugins: [
		// new WebpackBundleSizeAnalyzerPlugin("./reports/plain-report.txt"),
		// new BundleAnalyzerPlugin(),
		new HtmlWebpackPlugin({
			template: "app/index.html"
		}),
	]
};

if (process.env.NODE_ENV === "production") { // eslint-disable-line no-undef
	config.plugins.push(
		new webpack.DefinePlugin({
			"process.env": {
				"NODE_ENV": JSON.stringify(process.env.NODE_ENV) // eslint-disable-line no-undef
			}
		}),
		new webpack.optimize.UglifyJsPlugin()
	);
}

module.exports = config; // eslint-disable-line no-undef