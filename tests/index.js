import {group, sleep} from 'k6';
import {crypto} from "k6/experimental/webcrypto";
import user from "./scenarios/user/user.scenario.js";
import mail from "./scenarios/mail/mail.scenario.js";
import ride from "./scenarios/ride/ride.scenario.js";

export let optionsRamp = {
    stages: [
        {duration: '120s', target: 5},
        {duration: '120s', target: 25},
        {duration: '300s', target: 100},
        {duration: '300s', target: 250},
        {duration: '300s', target: 100},
        {duration: '300s', target: 50},
        {duration: '300s', target: 25},
        {duration: '300s', target: 5},
        {duration: '300s', target: 100},
        {duration: '30s', target: 5}
    ]
};

export const optionsOneIteration = {
    iterations: 1,
    vus: 1
};

export const optionsIterations = {
    iterations: 1000,
    vus: 200
};

export const optionsDuration = {
    stages: [
        {duration: '10s', target: 10},
        {duration: '20s', target: 250}
    ]
};

export const options = optionsOneIteration;

// const baseUrl = 'http://localhost:7000/api/v1';
// const baseUrl = 'http://localhost:8080';
// const baseUrl = 'http://localhost:8085/api/v1';
const baseUrl = 'http://0.0.0.0:9095';

export default () => {
    let headers = {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID()
        }
    };

    group('Endpoint User', () => {
        user(baseUrl, headers);
    });

    group('Endpoint Mail', () => {
        mail(baseUrl, headers);
    });

    group('Endpoint Ride', () => {
        ride(baseUrl, headers);
    });
}
