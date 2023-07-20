import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    duration: '30000s',
    //iterations: 5000,
    vus: 500,
};

export default function () {
    http.get('http://127.0.0.1:7000/api/v1/states');
    sleep(Math.random() * 75)
}