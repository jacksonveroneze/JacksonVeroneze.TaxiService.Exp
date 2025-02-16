import {group} from 'k6';
import {checker, factoryHeaders} from "../util.js";
import * as driver from "./driver.js";

export default (baseUrl, headers) => {
    // group('Create Flow - Success', () => {
    //     driver.create(baseUrl, factoryHeaders());
    // });
    //
    // group('Activate Flow', () => {
    //     const idDriver = driver.create(baseUrl, factoryHeaders());
    //     driver.activate(baseUrl, factoryHeaders(), idDriver);
    // });

    group('Complete Flow', () => {
        const idDriver = driver.create(baseUrl, factoryHeaders());
        driver.activate(baseUrl, factoryHeaders(), idDriver);
    });
}