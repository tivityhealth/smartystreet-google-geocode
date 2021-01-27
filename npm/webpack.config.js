const path = require("path")

module.exports = {
  entry: path.resolve(__dirname, "src/sgGeocode.js"),
  output: {
    path: path.resolve(__dirname, "build"),
    filename: "sgGeocode_bundle.js"
  },
  module: {
    rules: [
      {
        test: /\.(js)$/,
        exclude: /node_modules/,
        use: "babel-loader",
      },
    ],
  },
}