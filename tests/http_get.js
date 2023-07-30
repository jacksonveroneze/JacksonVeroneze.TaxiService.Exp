import http from 'k6/http';
import {sleep} from 'k6';

export const options = {
    duration: '30000s',
    //iterations: 5000,
    vus: 250,
};

export default function () {
    http.get('http://localhost/templatewebapi/health');
    sleep(Math.random() * 5);
    http.get('http://localhost/templatewebapi/api/v1/states');
    sleep(Math.random() * 2)
    http.get('http://localhost/templatewebapi/api/v1/states/SC');
    http.get('http://localhost/templatewebapi/api/v1/states/RS');
    http.get('http://localhost/templatewebapi/api/v1/states/RX');
    sleep(Math.random() * 5)
    http.get('http://localhost/templatewebapi/api/v1/states/SC/cities');
    http.get('http://localhost/templatewebapi/api/v1/states/RS/cities');
    http.get('http://localhost/templatewebapi/api/v1/states/RX/cities');
    sleep(Math.random() * 1)
}


//ps -T -p 1043090 -o 'pid tid args comm'