import http from 'k6/http';
import {sleep, randomSeed, check} from 'k6';
import {uuidv4} from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import {crypto} from "k6/experimental/webcrypto";
import {randomIntBetween} from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

// export const options = {
//     //duration: '60s',
//     iterations: 1000,
//     vus: 10,
// };

export let options = {
    stages: [
        {duration: '3m', target: 0}, // simulate ramp-up of traffic from 1 to 3 virtual users over 0.5 minutes.
        {duration: '5m', target: 100}, // simulate ramp-up of traffic from 1 to 3 virtual users over 0.5 minutes.
        {duration: '2m', target: 15}, // ramp-down to 0 users
        {duration: '1m', target: 150}, // ramp-down to 0 users
        {duration: '5m', target: 50}, // ramp-down to 0 users
        {duration: '30s', target: 0}, // ramp-down to 0 users
    ],
};

//const url = 'http://10.0.0.150/ta';
//const url = 'http://localhost:8088/api';
//const url = 'http://localhost:9999';
const url = 'http://localhost:7000';
//const url = 'http://localhost:9999';
//const url = 'http://10.152.183.41:8084';

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
    // Common
    let headers = {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID()
        },
    };

    const rnd = randomIntBetween(10000, 99999)

    // 1. Create User
    const bodyUser = JSON.stringify({
        name: crypto.randomUUID() + '_' + rnd,
        birthday: "2023-08-25",
        gender: "Male",
        document: "06399214939"
    });
    const responsePostUser = http.post(`${url}/api/v1/users`, bodyUser, headers);
    const idUser = JSON.parse(responsePostUser.body).data.id;

    check(responsePostUser, {'[User] - Created - status is 201': (r) => r.status === 201});

    // 2. Get User By Id
    const responseGetById = http.get(`${url}/api/v1/users/${idUser}`, headers);
    check(responseGetById, {'[User] - GetById - status is 200': (r) => r.status === 200});

    const responseGetPagedd = http.get(`${url}/api/v1/users/`, headers);
    check(responseGetPagedd, {'[User] - GetPaged - status is 200': (r) => r.status === 200});

    // 3. Activate User
    const responseActivated = http.put(`${url}/api/v1/users/${idUser}/activate`, {}, headers);
    check(responseActivated, {'[User] - Activated - status is 204': (r) => r.status === 204});

    // 1. Create Ride
    const bodyRide = JSON.stringify({
        user_id: idUser,
        latitude_from: 27.4153974,
        longitude_from: -51.5536425,
        latitude_to: 27.4153974,
        longitude_to: -51.5536425,
    });
    const responsePostRide = http.post(`${url}/api/v1/rides`, bodyRide, headers);
    const idRide = JSON.parse(responsePostRide.body).data.id;

    check(responsePostRide, {'[Ride] - Created - status is 201': (r) => r.status === 201});

    // 2. Get Ride By Id
    const responseGetRideById = http.get(`${url}/api/v1/rides/${idRide}`, headers);
    check(responseGetRideById, {'[Ride] - GetById - status is 200': (r) => r.status === 200});

    // 3. Accept Ride
    accept(idRide, idUser, headers);

    // 4. Start Ride
    start(idRide, headers);

    // 5. Finish Ride
    finish(idRide, headers);

    // 5. Finish Ride
    cancel(idRide, headers);

    // 2. Delete User
    //const responseDelete = http.del(`${url}/api/v1/users/${idUser}`, null, headers);
    //check(responseDelete, {'[User] - Delete - status is 200': (r) => r.status === 200});
}

function accept(id, driveId, headers) {
    const bodyAcceptRide = JSON.stringify({
        driver_id: driveId
    });

    const responseAcceptRide = http.put(`${url}/api/v1/rides/${id}/accept`, bodyAcceptRide, headers);
    check(responseAcceptRide, {'[Ride] - Accepted - status is 204': (r) => r.status === 204});

    getAndCheckStatusRide(id, 'Accepted', headers);
}

function start(id, headers) {
    const responseStartRide = http.put(`${url}/api/v1/rides/${id}/start`, {}, headers);
    check(responseStartRide, {'[Ride] - Started - status is 204': (r) => r.status === 204});

    getAndCheckStatusRide(id, 'InProgress', headers);
}

function finish(id, headers) {
    const responseFinishRide = http.put(`${url}/api/v1/rides/${id}/finish`, {}, headers);
    check(responseFinishRide, {'[Ride] - Finished - status is 204': (r) => r.status === 204});

    getAndCheckStatusRide(id, 'Completed', headers);
}

function cancel(id, headers) {
    const responseFinishRide = http.put(`${url}/api/v1/rides/${id}/cancel`, {}, headers);
    check(responseFinishRide, {'[Ride] - Canceled - status is 400': (r) => r.status === 400});

    getAndCheckStatusRide(id, 'Completed', headers);
}

function getAndCheckStatusRide(id, status, headers) {
    const responseGetById = http.get(`${url}/api/v1/rides/${id}`, headers);
    check(responseGetById, {'[Ride] - GetById - status is 200': (r) => r.status === 200});

    const bodyResponse = JSON.parse(responseGetById.body).data;

    check(bodyResponse, {'[Ride] - GetById - status': (r) => r.status === status});
}