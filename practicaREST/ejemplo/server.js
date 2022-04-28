var express= require('express')
var app=express();

var bodyParser = require('body-parser')//dar formato a las peticiones
var cors = require('cors')

var corsOptions = {
    origin: true,
    credentials: true
}

app.use(cors(corsOptions));
app.use(bodyParser.json());
app.use('/public', express.static(__dirname + '/public'));

var routes = require('./routes/routes');
routes.assignRoutes(app);

app.listen(3000);