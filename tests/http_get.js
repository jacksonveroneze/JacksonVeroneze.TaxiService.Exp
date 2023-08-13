import http from 'k6/http';
import {sleep} from 'k6';
import {uuidv4} from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';

export const options = {
    //duration: '30000s',
    iterations: 100,
    vus: 5,
};

//const url = 'http://localhost/templatewebapi';
const url = 'http://localhost:7000';

// export const options = {
//     vus: 50,
//     stages: [
//         {duration: '2m', target: 10},
//         {duration: '5m', target: 40},
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
    // http.get(`${url}/api/v1/banks`);
    // http.get(`${url}/api/v1/banks/00000000-0000-0000-0000-000000000000`);
    // http.get(`${url}/api/v1/banks/850323ca-567e-40fc-b16d-9e0021a8dfde`);
    // http.del(`${url}/api/v1/banks/00000000-0000-0000-0000-000000000000`);
    // http.del(`${url}/api/v1/banks/850323ca-567e-40fc-b16d-9e0021a8dfde`);
    // http.put(`${url}/api/v1/banks/850323ca-567e-40fc-b16d-9e0021a8dfde/activate`);

    var body = JSON.stringify({name: uuidv4()});
    
    //console.log(body);
    
    var responsePost = http.post(`${url}/api/v1/banks`, body, {
        headers: {'Content-Type': 'application/json'},
    });
    
    var id = JSON.parse(responsePost.body).data.id;

    http.get(`${url}/api/v1/banks/${id}`);

    //console.log(JSON.parse(responsePost.body).data.id);
}


//ps -T -p 1043090 -o 'pid tid args comm'