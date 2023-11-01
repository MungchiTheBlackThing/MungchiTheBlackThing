const express = require("express");
const app = express(); //연결
const webSocket= require("./socket"); //export한 변수 전달
const mysqlCnf= require("./connectDB");

mysqlCnf.connect((err)=>{
  if(err)
    throw err;
    console.log("Mysql 연결 완료..");

});
const server = app.listen(8080,()=>
{
  console.log("listening on *:8080");
});