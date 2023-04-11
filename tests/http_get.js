import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    duration: '3000s',
    vus: 50,
};

export default function () {
   let res1 = http.get('http://0.0.0.0:7000/api/v1/states/SC');
   // let res2 = http.get('http://0.0.0.0:7000/api/v1/states/RS');
    //let res3 = http.get('http://127.0.0.1:7000/api/v1/states/SC/cities');
    //let res4 = http.get('http://127.0.0.1:7000/api/v1/states/RS/cities');
}
