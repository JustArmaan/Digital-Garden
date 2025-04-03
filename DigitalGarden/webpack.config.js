const path = require("path");

module.exports = {
    entry: "./ClientApp/main.js",
    output: {
        filename: "bundle.js",
        path: path.resolve(__dirname, "wwwroot/js"),
    },
    mode: "development",
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-env"],
                    },
                },
            },
        ],
    },
};
