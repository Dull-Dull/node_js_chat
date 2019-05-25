var express = require('express');
var app = express();

var http = require('http').createServer( app );
var io = require('socket.io')(http);

io.on('connection', (socket)=>{
    console.log('a user connected');

    socket.on( 'disconnect', ()=>{
        console.log('user disconnected');        
    });

    socket.on( 'login', ( data )=>{
        console.log('user login');
        console.log( data );
    });

});

http.listen( 19900, ()=>{
    console.log('server is run!!!');
});