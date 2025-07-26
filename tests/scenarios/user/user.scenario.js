import {group} from 'k6';
import {checker, factoryHeaders} from "../util.js";
import * as user from "./user.js";

export default (baseUrl, headers) => {
    group('Create Flow - Success', () => {
        user.create(baseUrl, factoryHeaders());
    });

    group('Get Paged Flow', () => {
        user.getPaged(baseUrl, factoryHeaders());
    });

    group('Get By Id Flow - Found', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        user.getById(baseUrl, factoryHeaders(), idUser, 200);
    });

    group('Get By Id Flow - NotFound', () => {
        user.getById(baseUrl, factoryHeaders(), crypto.randomUUID(), 404);
    });

    group('Activate Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());

        const responseCallOne = user.activate(baseUrl, factoryHeaders(), idUser);
        checker(responseCallOne, 'User', 'Activated', 204)

        const responseCallTwo = user.activate(baseUrl, factoryHeaders(), idUser);
        checker(responseCallTwo, 'User', 'Already activated - BadRequest', 400);
    });

    group('Inactivate Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());

        const responseCallOne = user.inactivate(baseUrl, factoryHeaders(), idUser);
        checker(responseCallOne, 'User', 'Inactivate', 204)

        const responseCallTwo = user.inactivate(baseUrl, factoryHeaders(), idUser);
        checker(responseCallTwo, 'User', 'Already inactivated - BadRequest', 400)
    });

    group('Delete Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());

        const responseCallOne = user.remove(baseUrl, factoryHeaders(), idUser);
        checker(responseCallOne, 'User', 'Delete', 204)

        const responseCallTwo = user.remove(baseUrl, factoryHeaders(), idUser);
        checker(responseCallTwo, 'User', 'Already Deleted - NotFound', 404);
    });

    group('Complete Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        user.getById(baseUrl, factoryHeaders(), idUser);
        user.activate(baseUrl, factoryHeaders(), idUser);
        user.inactivate(baseUrl, factoryHeaders(), idUser);
        user.remove(baseUrl, factoryHeaders(), idUser);
    });
}