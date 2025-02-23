import {group} from 'k6';
import * as driver from "./driver.js";
import {checker, factoryHeaders} from "../util.js";

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
        driver.getById(baseUrl, factoryHeaders(), idDriver);
        driver.activate(baseUrl, factoryHeaders(), idDriver);
        driver.inactivate(baseUrl, factoryHeaders(), idDriver);

        const idDriver1 = driver.create(baseUrl, factoryHeaders());
        driver.getById(baseUrl, factoryHeaders(), idDriver1);
        driver.activate(baseUrl, factoryHeaders(), idDriver1);
        driver.block(baseUrl, factoryHeaders(), idDriver1, 'TemporaryBlock');

        const idDriver2 = driver.create(baseUrl, factoryHeaders());
        driver.getById(baseUrl, factoryHeaders(), idDriver2);
        driver.activate(baseUrl, factoryHeaders(), idDriver2);
        driver.remove(baseUrl, factoryHeaders(), idDriver2);

        const idDriver3 = driver.create(baseUrl, factoryHeaders());
        driver.getById(baseUrl, factoryHeaders(), idDriver3);
        driver.activate(baseUrl, factoryHeaders(), idDriver3);
        
        const vehicleId = driver.createVehicle(baseUrl, factoryHeaders(), idDriver3);
        driver.getVehicleById(baseUrl, factoryHeaders(), vehicleId);
    });
}