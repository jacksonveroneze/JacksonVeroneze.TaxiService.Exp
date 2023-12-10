import http from 'k6/http';
import {sleep, randomSeed, check} from 'k6';
import {uuidv4} from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
import {crypto} from "k6/experimental/webcrypto";
import {randomIntBetween} from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    //duration: '7200s',
    iterations: 50,
    vus: 20,
};

// export let options = {
//     stages: [
//         {duration: '10s', target: 10},
//         {duration: '40s', target: 50},
//         {duration: '24s', target: 15},
//         {duration: '50s', target: 25},
//         {duration: '20s', target: 100},
//         {duration: '24s', target: 0},
//         {duration: '24s', target: 2},
//     ],
// };

const url = 'http://localhost/taxi-service-exp';
//const url = 'http://localhost:7000';

export default function () {
    let headers = {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID()
        },
    };

    console.log(headers);

    const rnd = randomIntBetween(10000, 99999)

    const responsehealth = http.get(`${url}/health`, headers);
    check(responsehealth, {'[Health] - status is 200': (r) => r.status === 200});
    //return;

    // 1. Create User
    const bodyUser = JSON.stringify({
        name: crypto.randomUUID(),
        birthday: "2023-08-25",
        gender: "Male",
        document: "06399214939"
    });
    const responsePostUser = http.post(`${url}/api/v1/users`, bodyUser, headers);
    const idUser = JSON.parse(responsePostUser.body).data.id;

    console.log("USER_ID: " + idUser);

    check(responsePostUser, {'[User] - Created - status is 201': (r) => r.status === 201});

    // 2. Get User By Id
    const responseGetById = http.get(`${url}/api/v1/users/${idUser}?page=1&page_size=2`, headers);
    check(responseGetById, {'[User] - GetById - status is 200': (r) => r.status === 200});

    const responseGetPaged = http.get(`${url}/api/v1/users/`, headers);
    check(responseGetPaged, {'[User] - GetPaged - status is 200': (r) => r.status === 200});

    // 3. Activate User
    const responseActivated = http.put(`${url}/api/v1/users/${idUser}/activate`, {}, headers);
    check(responseActivated, {'[User] - Activated - status is 204': (r) => r.status === 204});

    // 4. Activate User
    const responseInactivated = http.put(`${url}/api/v1/users/${idUser}/inactivate`, {}, headers);
    check(responseInactivated, {'[User] - Inactivated - status is 204': (r) => r.status === 204});

    // 5. Delete User
    // const responseDelete = http.del(`${url}/api/v1/users/${idUser}`, null, headers);
    // check(responseDelete, {'[User] - Delete - status is 204': (r) => r.status === 204});


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
    return;
    console.info(`Ride id: ${idRide}`)

    // 2. Get Ride By Id
    const responseGetRideById = http.get(`${url}/api/v1/rides/${idRide}`, headers);
    check(responseGetRideById, {'[Ride] - GetById - status is 200': (r) => r.status === 200});

    // 3. Accept Ride
    accept(idRide, idUser, headers);

    // // 4. Start Ride
    start(idRide, headers);

    const positions = [
        {
            ride_id: idRide,
            latitude: -27.4093877,
            longitude: -51.5631909,
        },
        {
            ride_id: idRide,
            latitude: -27.3330812,
            longitude: -51.6073263,
        },
        {
            ride_id: idRide,
            latitude: -27.1678441,
            longitude: -51.5048672,
        }
    ];

    for (let pos of positions) {
        const bodyPositionRide = JSON.stringify(pos);
        const responsePostPosition = http.post(`${url}/api/v1/positions`, bodyPositionRide, headers);
        check(responsePostPosition, {'[Position] - Created - status is 201': (r) => r.status === 201});
    }

    // 5. Finish Ride
    finish(idRide, headers);

    // 5. Finish Ride
    //cancel(idRide, headers);

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