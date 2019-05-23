var app = require('express')();
var http = require('http').createServer( app );
var io = require('socket.io')(http);

// app.get('/', ( req, res) => {
//     res.send('<h1>Hello World</h1>');
// });

app.post('/Login', (req, res)=>{
    Console.log('login user', req.ip );
});

io.on('connection', (socket)=>{
    socket.broadcast.emit

    console.log('a user connected');

    socket.on( 'disconnect', ()=>{
        console.log('user disconnected');

        
    });

});

http.listen( 19900, ()=>{
    console.log('server is run!!!');
});