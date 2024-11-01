import {check} from 'k6';
import {crypto} from "k6/experimental/webcrypto";

export function checker(response, tag, desc, statusCode) {
    const des = `[${tag}] - ${desc} - status is ${statusCode}`;

    check(response, {[des]: (r) => r.status === statusCode});
}

export function factoryHeaders()
{
    return {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID()
        }
    };
}