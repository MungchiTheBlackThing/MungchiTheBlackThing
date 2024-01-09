//const express = require("express");
//const app = express(); //연결
require('dotenv').config();

const webSocket= require('socket.io')(process.env.PORT); //export한 변수 전달
console.log("webSocket 연결 ",process.env.PORT);
const mysqlCnf= require("./connectDB");
//mysql 연결
mysqlCnf.connect((err)=>{
  if(err)
    throw err;
    console.log("Mysql 연결 완료..");

});
//on은 클라이언트에서 보낸 이벤트를 실행할 때 사용
//socket번호 전달.. 
webSocket.on('connection',function(socket)
{
  const clientIP = socket.handshake.address;
  console.log('Player Connected');

  //client가 server에게 playerid를 전송한다.
  //그 PlayerId를 받는다.
  socket.on("PlayerID",(idData) =>{
      console.log(idData);
      //데이터를 전송한 해당 ip는 socket.id가 저장중이기 때문에,이를 이용해 데이터를 전송하고
      var selectQry="select * from Player where PlayerID=?";
      mysqlCnf.query(selectQry,idData,function(err,rows,field){
      if(err)
      { 
        console.log(err);
      }
      
      console.log(rows);
      console.log('socket.id:', socket.id);

      const jsonData = JSON.stringify(rows); 
      webSocket.to(socket.id).emit("pass_player_data",jsonData);
      
      //ㄴㄴ.. 걍 틀린 문법 사용했음 webSocket.to(socket.id).emit() 문법이 맞음
      webSocket.to(socket.id).emit("Success",true);
      
    });
  });
});

