import {group} from 'k6';
import {crypto} from "k6/experimental/webcrypto";
import * as user from "../user/user.js";
import * as ride from "./ride.js";
import * as position from "../position/position.js";
import {factoryHeaders} from "../util.js";

export default (baseUrl, headers) => {
    group('Create Flow - Success', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        ride.create(baseUrl, factoryHeaders(), idUser);
    });

    group('Get Paged Flow', () => {
        ride.getPaged(baseUrl, factoryHeaders());
    });

    group('Get By Id Flow - Found', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        const rideId = ride.create(baseUrl, factoryHeaders(), idUser);

        ride.getById(baseUrl, factoryHeaders(), rideId, 200);
    });

    group('Get By Id Flow - NotFound', () => {
        ride.getById(baseUrl, factoryHeaders(), crypto.randomUUID(), 404);
    });

    group('Cancel Flow - Request and Cancel', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        const idRide = ride.create(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.cancel(baseUrl, factoryHeaders(), idRide);
    });

    group('Cancel Flow - Accept and Cancel', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        const idRide = ride.create(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.accept(baseUrl, factoryHeaders(), idRide, idUser);
        ride.cancel(baseUrl, factoryHeaders(), idRide);
    });

    group('Finish Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        const idRide = ride.create(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.accept(baseUrl, factoryHeaders(), idRide, idUser);
        ride.start(baseUrl, factoryHeaders(), idRide);
        position.createBatchPositions(baseUrl, factoryHeaders(), idRide, 50);
        ride.finish(baseUrl, factoryHeaders(), idRide);
    });
}