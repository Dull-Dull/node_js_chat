var app = require('express')();
var http = require('http').createServer( app );
var io = require('socket.io')(http);

app.get('/', ( req, res) => {
    res.send('<h1>Hello World</h1>');
});

io.on('connection', (socket)=>{
    console.log('a user connected');
});

http.listen( 80, ()=>{
    console.log('server is run!!!');
});