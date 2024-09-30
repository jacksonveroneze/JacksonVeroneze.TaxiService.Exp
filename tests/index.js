import {group} from 'k6';
import {crypto} from "k6/experimental/webcrypto";
import user from "./scenarios/user/user.scenario.js";
import mail from "./scenarios/mail/mail.scenario.js";
import ride from "./scenarios/ride/ride.scenario.js";

export let optionsRamp = {
    stages: [
        {duration: '10s', target: 2},
        {duration: '100s', target: 10},
        {duration: '60s', target: 300},
        {duration: '30s', target: 100},
        {duration: '240s', target: 5}
    ]
};

export const optionsOneIteration = {
    iterations: 1,
    vus: 1
};

export const optionsIterations = {
    iterations: 2,
    vus: 2
};

export const optionsDuration = {
    duration: '30s',
    vus: 50
};

export const options = optionsDuration;

const baseUrl = 'http://localhost:7000/api/v1';
// const baseUrl = 'http://localhost:8080';
// const baseUrl = 'http://localhost:8085/api/v1';

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


    // const idUser1 = "916e4031-34d3-43ce-9cb2-262d816b8041";
    // const idUser2 = "2e6d0bf8-f6a5-428f-9fa8-069609c582d0";
    //
    // const idRide = "2913230e-dd3f-4317-a50a-eae4e61da09a";
    //
    // const bodyAcceptRide1 = JSON.stringify({
    //     driver_id: idUser1
    // });
    //
    // const bodyAcceptRide2 = JSON.stringify({
    //     driver_id: idUser2
    // });
    //
    // const response1 = http.put(`${baseUrl}/rides/${idRide}/accept`, bodyAcceptRide1, headers);
    // //const response2 = http.put(`${baseUrl}/rides/${idRide}/accept`, bodyAcceptRide2, headers);
}
