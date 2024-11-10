import {group} from 'k6';
import * as user from "../user/user.js";
import * as mail from "./mail.js";
import {factoryHeaders} from "../util.js";

export default (baseUrl, headers) => {
    group('Create Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        mail.createBatchEmails(baseUrl, factoryHeaders(), idUser, 10);
    });

    group('Get Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        mail.createBatchEmails(baseUrl, factoryHeaders(), idUser, 10);
        mail.getEmailsPaged(baseUrl, factoryHeaders(), idUser);
    });

    group('Delete Flow', () => {
        const idUser = user.create(baseUrl, factoryHeaders());
        mail.createBatchEmails(baseUrl, factoryHeaders(), idUser, 10);
        mail.deleteAllEmails(baseUrl, factoryHeaders(), idUser);
    });
}