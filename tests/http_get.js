import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    duration: '15s',
    //iterations:50,
    vus: 50,
};


// export let options = {
//     stages: [
//         {duration: '20s', target: 10},
//         {duration: '30s', target: 100},
//         {duration: '30s', target: 300},
//         {duration: '30s', target: 5},
//         {duration: '20s', target: 10},
//         {duration: '30s', target: 100},
//         {duration: '30s', target: 300},
//         {duration: '30s', target: 5}
//     ]
// };

export default function () {
    http.get('http://127.0.0.1:80/domain/api/v1/states');
    // http.get('http://127.0.0.1:7000/api/v1/states');
    http.get('http://127.0.0.1:80/domain/api/v1/states/SC');
    http.get('http://127.0.0.1:80/domain/api/v1/states/SC/cities');
}



//ps -T -p 1043090 -o 'pid tid args comm'