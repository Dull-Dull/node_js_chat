var express = require('express');
var app = express();

var http = require('http').createServer( app );
var io = require('socket.io')(http, {
    pingInterval: 10000000,
    pingTimeout: 5000,
    cookie: false
});

var userList = [];

function FindUserIdBySocket ( socket )
{
    var user = userList.find( ( element )=>{
        return element.sock == socket;
    });

    if( user != undefined )
        return user.id;

    return "Unknown";
}

io.on('connection', (socket)=>{
    console.log('a user connected');

    socket.on( 'disconnect', ()=>{
        console.log('user disconnected');

        var user = userList.find( ( element ) =>{
            return element.sock == socket;
        })

        if( user != undefined )
        {
            io.emit( 'user_logout', user.id );
            userList.splice( userList.indexOf(user), 1 );
        }
    });

    socket.on( 'req_login', ( userId )=>{
        console.log('user login');
        socket.emit( 'res_login', 'ok' );  

        var userIds = [];
        userList.forEach( ( value, index, array )=>{ userIds.push( value.id ); });        
        socket.emit( 'user_list', JSON.stringify( { userIds : userIds } ) );

        userList.push( { sock : socket, id : userId } ); 
        io.emit( 'user_login', userId );
    });

    socket.on( 'chat_message', ( msg )=>{
        io.emit('chat_message', FindUserIdBySocket(socket) + " : " + msg );
    });

});

http.listen( 19900, ()=>{
    console.log('server is run!!!');
});