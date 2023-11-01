//소켓 연결
const socket=require('socket.io');

module.exports = (server) => {
    const io = socket(server,{ path: "/socket.io"});
    io.on("connection",(socket)=>{
        socket.on("message",(mag)=>{
            io.emit("message",msg); //msg 서버에서 출력?을 의미하는가?
        });
    });
};