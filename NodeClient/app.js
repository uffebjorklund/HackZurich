var xsockets = require('xsockets.net');

var c = new xsockets.TcpClient('hackzurich.cloudapp.net', 8080, ['monitor']);
//On WeatherData
c.controller('monitor').on('wd', function (d) {
    console.log('weather data', d);
});

c.controller('monitor').onopen = function (ci) {    
    c.controller('monitor').send('set_clienttype', 'monitor');
}
c.onconnected = function (d) {
    console.log('connected', d);
}

c.open();