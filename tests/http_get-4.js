import http from 'k6/http';
import { sleep } from 'k6';
import { check } from 'k6';


export const options = {
    duration: '3600s',
    //iterations: 5000,
    vus: 500,
};

export default function () {
    const res1 = http.get('http://localhost/templatewebapi/api/v1/states');
    const res2 = http.get('http://localhost/templatewebapi/api/v1/states/AC');
    //const res3 = http.get('http://localhost/templatewebapi/api/v1/states/SC/cities');

    check(res1, { 'is status 200': (r) => r.status === 200 });
    check(res1, { 'body contains': (r) => r.body.includes('AC') });

    check(res2, { 'is status 200': (r) => r.status === 200 });
    check(res2, { 'body contains': (r) => r.body.includes('AC') });
    //check(res2, { 'is status 200': (r) => r.status === 200 });
    //check(res3, { 'is status 200': (r) => r.status === 200 });
}