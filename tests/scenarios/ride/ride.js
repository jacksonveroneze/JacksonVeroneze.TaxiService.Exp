import http from 'k6/http';
import {checker} from '../util.js';

export function create(baseUrl, headers, idUser) {
    const body = JSON.stringify({
        userId: idUser,
        latitudeFrom: 27.4153974,
        longitudeFrom: -51.5536425,
        latitudeTo: 27.4153974,
        longitudeTo: -51.5536425,
    });

    const response = http.post(`${baseUrl}/rides`, body, headers);
    checker(response, 'Ride', 'Created', 201)

    return JSON.parse(response.body).data.id;
}

export function getPaged(baseUrl, headers) {
    const response = http.get(`${baseUrl}/rides`, headers);
    checker(response, 'Ride', 'GetPaged', 200)
}

export function getById(baseUrl, headers, idRide) {
    const response = http.get(`${baseUrl}/rides/${idRide}`, headers);
    checker(response, 'Ride', 'GetById', 200)
}

export function accept(baseUrl, headers, rideId, driveId) {
    const bodyAcceptRide = JSON.stringify({
        driver_id: driveId
    });

    const response = http.put(`${baseUrl}/rides/${rideId}/accept`, bodyAcceptRide, headers);
    checker(response, 'Ride', 'Accepted', 204)
}

export function start(baseUrl, headers, rideId) {
    const response = http.put(`${baseUrl}/rides/${rideId}/start`, {}, headers);
    checker(response, 'Ride', 'Started', 204)
}

export function finish(baseUrl, headers, rideId) {
    const response = http.put(`${baseUrl}/rides/${rideId}/finish`, {}, headers);
    checker(response, 'Ride', 'Finished', 204)
}

export function cancel(baseUrl, headers, rideId) {
    const response = http.put(`${baseUrl}/rides/${rideId}/cancel`, {}, headers);
    checker(response, 'Ride', 'Cancel', 204)
}