import http from 'k6/http';

export const options = {
    duration: '60s',
    vus: 50,
};

export default function () {
    http.get('http://127.0.0.1:8085/api/v1/states');
    http.get('http://127.0.0.1:8085/api/v1/states/SC');
    http.get('http://127.0.0.1:8085/api/v1/states/RS');
    http.get('http://127.0.0.1:8085/api/v1/states/RX');
    http.get('http://127.0.0.1:8085/api/v1/states/SC/cities');
    http.get('http://127.0.0.1:8085/api/v1/states/RS/cities');
    http.get('http://127.0.0.1:8085/api/v1/states/RX/cities');
}
