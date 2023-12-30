import {group} from 'k6';
import {crypto} from "k6/experimental/webcrypto";

import user from "./scenarios/user/user.scenario.js";
import mail from "./scenarios/mail/mail.scenario.js";
import ride from "./scenarios/ride/ride.scenario.js";

export let optionsRamp = {
    stages: [
        {duration: '10s', target: 2},
        {duration: '10s', target: 10},
        {duration: '60s', target: 30},
        {duration: '30s', target: 10},
        {duration: '10s', target: 1}
    ]
};

export const optionsIterations = {
    iterations: 1,
    vus: 1
};

export const optionsDuration = {
    duration: '7200s',
    vus: 5
};

export const options = optionsRamp;

const baseUrl = 'http://localhost:7000';

export default () => {
    let headers = {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID()
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