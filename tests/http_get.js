import http from 'k6/http';
import {sleep, randomSeed} from 'k6';
import {uuidv4} from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import { crypto } from "k6/experimental/webcrypto";
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    //duration: '30000s',
    iterations: 100000,
    vus: 50,
};

// export let options = {
//     stages: [
//         { duration: '30s', target: 500 }, // simulate ramp-up of traffic from 1 to 3 virtual users over 0.5 minutes.
//         { duration: '10s', target: 10000 }, // simulate ramp-up of traffic from 1 to 3 virtual users over 0.5 minutes.
//         { duration: '2m', target: 15 }, // ramp-down to 0 users
//         { duration: '2m', target: 150 }, // ramp-down to 0 users
//         { duration: '2m', target: 50 }, // ramp-down to 0 users
//         { duration: '2m', target: 0 }, // ramp-down to 0 users
//     ],
// };


//const url = 'http://localhost/templatewebapi';
const url = 'http://localhost:7000';

// export const options = {
//     vus: 50,
//     stages: [
//         {duration: '1m', target: 10},
//         {duration: '2m', target: 40},
//         {duration: '30s', target: 0}
//     ]
//     // scenarios: {
//     // shared_iter_scenario: {
//     //     executor: "shared-iterations",
//     //     vus: 100,
//     //     iterations: 1000,
//     //     startTime: "0s",
//     // },
//     // per_vu_scenario: {
//     //     executor: "per-vu-iterations",
//     //     vus: 10,
//     //     iterations: 1000,
//     //     startTime: "120s",
//     // },
//     //     ramping_vus: {
//     //         executor: 'ramping-vus',
//     //         startVUs: 10,
//     //         stages: [
//     //             {duration: '200s', target: 5},
//     //             {duration: '300s', target: 500},
//     //             {duration: '200s', target: 5},
//     //             {duration: '300s', target: 500},
//     //             {duration: '200s', target: 5},
//     //             {duration: '300s', target: 500},
//     //             {duration: '200s', target: 5},
//     //             {duration: '300s', target: 500},
//     //         ],
//     //     }
//     // },
// };


export default function () {
    const rnd = randomIntBetween(1, 50000)
    
    var body = JSON.stringify({name: crypto.randomUUID() + '_' + rnd});

    var responsePost = http.post(`${url}/api/v1/users`, body, {
        headers: {'Content-Type': 'application/json'},
    });

    var id = JSON.parse(responsePost.body).data.id;

    http.get(`${url}/api/v1/users/${id}`);
    http.put(`${url}/api/v1/users/${id}/activate`);
    http.put(`${url}/api/v1/users/${id}/inactivate`);
    //
    // //console.log('___' + rnd + '____')
    //
    if(rnd % 2 === 0)
    {
        //console.log('___2___')
        http.del(`${url}/api/v1/users/${id}`);
    }
    //
    // if(rnd % 20 === 0)
    // {
    //     //console.log('___3___')
    //     http.del(`${url}/api/v1/banks/1111`);
    // }
    //
    // if(rnd % 10 === 0)
    // {
    //     //console.log('___4___')
    //     http.del(`${url}/api/v1/banks/850323ca-567e-40fc-b16d-9e0021a8dfde`);
    // }
    
    //console.log(JSON.parse(responsePost.body).data.id);
}


//ps -T -p 1043090 -o 'pid tid args comm'