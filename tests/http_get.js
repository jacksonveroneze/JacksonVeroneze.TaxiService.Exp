import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    duration: '300s',
    vus: 10,
};

export default function () {
    let res = http.get('http://0.0.0.0:7000/api/v1/states/SC');
}
