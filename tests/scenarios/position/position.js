import http from 'k6/http';
import {checker, factoryHeaders} from "../util.js";

export function createBatchPositions(baseUrl, headers, idRide, total) {
    for (let i = 0; i < total; i++) {
        const bodyPositionRide = JSON.stringify({
            rideId: idRide,
            latitude: -27.1678441 + i,
            longitude: -51.5048672 + i,
        });

        const response = http.post(`${baseUrl}/positions`, bodyPositionRide, factoryHeaders());
        checker(response, 'Position', 'Created', 201)
    }
}