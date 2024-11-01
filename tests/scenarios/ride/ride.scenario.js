import {group} from 'k6';
import * as user from "../user/user.js";
import * as ride from "./ride.js";
import * as position from "../position/position.js";
import {factoryHeaders} from "../util.js";

export default (baseUrl, headers) => {
    group('Cancel Flow - Complete', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());
        const idRide = ride.createRider(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.accept(baseUrl, factoryHeaders(), idRide, idUser);
        ride.start(baseUrl, factoryHeaders(), idRide);
        position.createBatchPositions(baseUrl, factoryHeaders(), idRide, 55);
        ride.cancel(baseUrl, factoryHeaders(), idRide);
    });

    group('Cancel Flow - Request and Cancel', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());
        const idRide = ride.createRider(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.cancel(baseUrl, factoryHeaders(), idRide);
    });

    group('Cancel Flow - Accept and Cancel', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());
        const idRide = ride.createRider(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.accept(baseUrl, factoryHeaders(), idRide, idUser);
        ride.cancel(baseUrl, factoryHeaders(), idRide);
    });

    group('Finish Flow', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());
        const idRide = ride.createRider(baseUrl, factoryHeaders(), idUser);
        ride.getById(baseUrl, factoryHeaders(), idRide);
        ride.accept(baseUrl, factoryHeaders(), idRide, idUser);
        ride.start(baseUrl, factoryHeaders(), idRide);
        position.createBatchPositions(baseUrl, factoryHeaders(), idRide, 50);
        ride.finish(baseUrl, factoryHeaders(), idRide);
    });
}