import http from 'k6/http';

export const options = {
    duration: '60s',
    vus: 50,
};

export default function () {
   let res1 = http.get('http://127.0.0.1:8085/api/v1/states/SC');
   // let res2 = http.get('http://0.0.0.0:7000/api/v1/states/RS');
    //let res3 = http.get('http://127.0.0.1:7000/api/v1/states/SC/cities');
    //let res4 = http.get('http://127.0.0.1:7000/api/v1/states/RS/cities');
}
