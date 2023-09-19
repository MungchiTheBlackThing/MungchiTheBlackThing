'use strict';
 
const express = require('express');
require('dotenv').config();
// 상수
const PORT = process.env.PORT
const HOST = process.env.HOST
 
// 앱
const app = express();
app.get('/', (req, res) => {
  res.send('Hello World');
});
 
app.listen(PORT, HOST, () => {
  console.log(`Running on http://${HOST}:${PORT}`);
});