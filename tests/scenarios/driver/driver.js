import http from 'k6/http';
import {check} from 'k6';

import {randomIntBetween, randomItem, randomString} from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export function create(baseUrl, headers) {
    const genders = ['Male', 'Female'];

    const body = JSON.stringify({
        fullName: `${randomString(8)} ${randomString(8)}`,
        birthday: `2023-${randomIntBetween(10, 12)}-${randomIntBetween(10, 25)}`,
        gender: `${randomItem(genders)}`,
        document: "06399214939",
        email: `${randomString(10)}@mail.com`,
        phone: `${randomIntBetween(10, 99)}-${randomIntBetween(10000, 99999)}${randomIntBetween(1000, 9999)}`
    });

    const response = http.post(`${baseUrl}/drivers`, body, headers);
    check(response, {'[Driver] - Created - status is 201': (r) => r.status === 201});

    const content = JSON.parse(response.body);

    return content.data.id;
}

export function activate(baseUrl, headers, id) {
    const response = http.put(`${baseUrl}/drivers/${id}/activate`, {}, headers);
    check(response, {'[Driver] - Activated - status is 204': (r) => r.status === 204});
    
    return response;
}


export function createVehicle(baseUrl, headers, driverId) {
    const genders = ['Male', 'Female'];

    const body = JSON.stringify({
        driverId: driverId,
        model: `${randomString(8)}`,
        brand: `${randomString(8)}`,
        plate: `${randomString(8)}`,
        color: `${randomString(8)}`,
        year: randomIntBetween(2010, 2025)
    });

    const response = http.post(`${baseUrl}/vehicles`, body, headers);
    check(response, {'[Vehicle] - Created - status is 201': (r) => r.status === 201});

    const content = JSON.parse(response.body);

    return content.data.id;
}