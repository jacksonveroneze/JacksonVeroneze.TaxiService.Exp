import {group} from 'k6';
import {crypto} from "k6/experimental/webcrypto";
import {checker, factoryHeaders} from "../util.js";
import * as user from "./user.js";

export default (baseUrl, headers) => {
    group('Create Flow - Success', () => {
        user.createUser(baseUrl, factoryHeaders());
    });

    group('Get Paged Flow', () => {
        user.getUsersPaged(baseUrl, factoryHeaders());
    });

    group('Get By Id Flow - Found', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());
        user.getUserById(baseUrl, factoryHeaders(), idUser, 200);
    });

    group('Get By Id Flow - NotFound', () => {
        user.createUser(baseUrl, factoryHeaders());
        user.getUserById(baseUrl, factoryHeaders(), crypto.randomUUID(), 404);
    });

    group('Activate Flow', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());

        const responseCallOne = user.activateUser(baseUrl, factoryHeaders(), idUser);
        checker(responseCallOne, 'User', 'Activated', 204)

        const responseCallTwo = user.activateUser(baseUrl, factoryHeaders(), idUser);
        checker(responseCallTwo, 'User', 'Already activated - BadRequest', 400);

        const responseCallNotFound = user.activateUser(baseUrl, factoryHeaders(), crypto.randomUUID());
        checker(responseCallNotFound, 'User', 'NotFound', 404)
    });

    group('Inactivate Flow', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());

        const responseCallOne = user.inactivateUser(baseUrl, factoryHeaders(), idUser);
        checker(responseCallOne, 'User', 'Activated', 204)

        const responseCallTwo = user.inactivateUser(baseUrl, factoryHeaders(), idUser);
        checker(responseCallTwo, 'User', 'Already inactivated - BadRequest', 400)
    });

    group('Delete Flow', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());

        const responseCallOne = user.deleteUser(baseUrl, factoryHeaders(), idUser);
        checker(responseCallOne, 'User', 'Delete', 204)

        const responseCallTwo = user.deleteUser(baseUrl, factoryHeaders(), idUser);
        checker(responseCallTwo, 'User', 'Already Deleted - NotFound', 404);
    });

    group('Complete Flow', () => {
        const idUser = user.createUser(baseUrl, factoryHeaders());
        user.getUserById(baseUrl, factoryHeaders(), idUser);
        user.activateUser(baseUrl, factoryHeaders(), idUser);
        user.inactivateUser(baseUrl, factoryHeaders(), idUser);
        user.deleteUser(baseUrl, factoryHeaders(), idUser);
    });
}