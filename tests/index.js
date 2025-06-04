import {group, sleep} from 'k6';
import {crypto} from "k6/experimental/webcrypto";
import driver from "./scenarios/driver/driver.scenario.js";
import user from "./scenarios/user/user.scenario.js";
import mail from "./scenarios/mail/mail.scenario.js";
import ride from "./scenarios/ride/ride.scenario.js";

export let optionsRamp = {
    stages: [
        {duration: '5s', target: 1},
        {duration: '120s', target: 25},
        {duration: '300s', target: 100},
        {duration: '300s', target: 250},
        {duration: '300s', target: 100},
        {duration: '300s', target: 50},
        {duration: '300s', target: 25},
        {duration: '100s', target: 5},
        {duration: '300s', target: 100},
        {duration: '30s', target: 5},
        {duration: '300s', target: 5}
    ]
};

export const optionsOneIteration = {
    iterations: 1,
    vus: 1
};

export const optionsIterations = {
    iterations: 20000,
    vus: 250
};

export const optionsDuration = {
    stages: [
        {duration: '10s', target: 10},
        {duration: '60s', target: 200}
    ]
};

//
// export const options = {
//     scenarios: {
//         constant_request_rate: {
//             executor: 'constant-arrival-rate',
//             rate: 1000,               // 💡 9 requisições por segundo
//             timeUnit: '1s',
//             duration: '30s',
//             preAllocatedVUs: 20,
//             maxVUs: 250,
//         },
//     },
// };

export const options = optionsRamp;

// const baseUrl = 'http://localhost:7000/api/v1';
const baseUrl = 'http://srv754980.hstgr.cloud:9098/v1';
// const baseUrl = 'http://localhost:8080';
// const baseUrl = 'http://localhost:8085/api/v1';
//const baseUrl = 'http://0.0.0.0:9095';

export default () => {
    let headers = {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID()
        }
    };

    group('Endpoint Driver', () => {
        driver(baseUrl, headers);
    });

    // group('Endpoint User', () => {
    //     user(baseUrl, headers);
    // });
    //
    // group('Endpoint Mail', () => {
    //     mail(baseUrl, headers);
    // });
    //
    // group('Endpoint Ride', () => {
    //     ride(baseUrl, headers);
    // });

    // sleep(1);
}
