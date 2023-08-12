import http from 'k6/http';
import {sleep} from 'k6';

// export const options = {
//     duration: '30000s',
//     //iterations: 5000,
//     vus: 250,
// };

//const url = 'http://localhost/templatewebapi/';
const url = 'http://localhost:7000';

export const options = {
    // Key configurations for Stress in this section
    stages: [
        { duration: '1m', target: 10 }, // traffic ramp-up from 1 to a higher 200 users over 10 minutes.
        { duration: '1m', target: 50 }, // stay at higher 200 users for 10 minutes
        { duration: '1m', target: 0 }, // ramp-down to 0 users
    ],
};


export default function () {
    http.get(`${url}/health`);
    http.get(`${url}/api/v1/banks`);
}


//ps -T -p 1043090 -o 'pid tid args comm'