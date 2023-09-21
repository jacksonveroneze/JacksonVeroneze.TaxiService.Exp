import http from 'k6/http';
import { sleep, randomSeed, check } from 'k6';
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import { crypto } from "k6/experimental/webcrypto";
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    duration: '60s',
    //iterations: 10000,
    vus: 500,
};

// export let options = {
//     stages: [
//         {duration: '3m', target: 10}, // simulate ramp-up of traffic from 1 to 3 virtual users over 0.5 minutes.
//         {duration: '3m', target: 100}, // simulate ramp-up of traffic from 1 to 3 virtual users over 0.5 minutes.
//         {duration: '2m', target: 15}, // ramp-down to 0 users
//         {duration: '1m', target: 150}, // ramp-down to 0 users
//         {duration: '5m', target: 50}, // ramp-down to 0 users
//         {duration: '30s', target: 0}, // ramp-down to 0 users
//     ],
// };

const url = 'http://localhost/templatewebapi';
//const url = 'http://localhost:8088/api';
//const url = 'http://localhost:9999';
//const url = 'http://10.0.0.199/templatewebapi';
//const url = 'http://localhost:7000';
//const url = 'http://localhost:9999';

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
    // const rnd = randomIntBetween(10000, 99999)
    // const rnd1 = randomIntBetween(10000, 99999)
    //
    // var body = JSON.stringify({
    //     Nome: crypto.randomUUID() + '_' + rnd,
    //     Nascimento: "2023-08-25",
    //     Apelido: "Jack" + '_' + rnd + '_' + rnd1
    // });
    //
    // var responsePost = http.post(`${url}/pessoas`, body, {
    //     headers: {'Content-Type': 'application/json'},
    // });


    // var body = JSON.stringify({
    //     name: crypto.randomUUID() + '_' + rnd,
    //     birthday: "2023-08-25",
    //     gender: "Male",
    //     document: rnd1 + "2" + rnd
    // });
    //
    // var responsePost = http.post(`${url}/api/v1/users`, body, {
    //     headers: {'Content-Type': 'application/json'},
    // });

    // var body = JSON.stringify({
    //     firstName: crypto.randomUUID() + '_' + rnd,
    //     lastName: crypto.randomUUID() + '_' + rnd,
    //     email: crypto.randomUUID() + '@mail.com',
    //     password: "Admin@123",
    // });
    //
    // var responsePost = http.post(`${url}/authentication/register`, body, {
    //     headers: {'Content-Type': 'application/json'},
    // });
    //
    // check(responsePost, {
    //     'status is sucess': (r) => r.status === 201,
    // });

    var res = http.get(`${url}/api/v1/users`);

    check(res, {
        'status is 200': (r) => r.status === 200,
    });

    //
    //    var id = JSON.parse(responsePost.body).data.id;
    //
    //    http.get(`${url}/api/v1/users/${id}`);
    //
    //    if (rnd % 2 === 0) {
    //        http.put(`${url}/api/v1/users/${id}/activate`);
    //
    //        if (rnd % 15 === 0) {
    //            http.put(`${url}/api/v1/users/${id}/inactivate`);
    //        }
    //
    //        var bodyMail = JSON.stringify({
    //            id: id,
    //            email: crypto.randomUUID() + '_' + rnd + '@user.com'
    //        });
    //
    //        var responsePostMail = http.post(`${url}/api/v1/users/${id}/emails`, bodyMail, {
    //            headers: {'Content-Type': 'application/json'},
    //        });
    //
    //        var idMail = JSON.parse(responsePostMail.body).data.id;
    //
    //        http.del(`${url}/api/v1/users/${id}/emails/${idMail}`);
    //    }


    //http.del(`${url}/api/v1/users/${id}`);
    // // //
    // // // //console.log('___' + rnd + '____')
    // // //
    // if (rnd % 2 === 0) {
    //     //console.log('___2___')
    //     http.del(`${url}/api/v1/users/${id}`);
    // }
    // //
    // if (rnd % 20 === 0) {
    //     //console.log('___3___')
    //     http.del(`${url}/api/v1/users/1111`);
    // }
    // //
    // if (rnd % 10 === 0) {
    //     //console.log('___4___')
    //     http.del(`${url}/api/v1/users/850323ca-567e-40fc-b16d-9e0021a8dfde`);
    // }

    //console.log(JSON.parse(responsePost.body).data.id);
}


//ps -T -p 1043090 -o 'pid tid args comm'