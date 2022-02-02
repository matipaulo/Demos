import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        // A list of virtual users { target: ..., duration: ... } objects that specify 
        // the target number of VUs to ramp up or down to for a specific period.
        { duration: '5m', target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
        { duration: '10m', target: 100 }, // stay at 100 users for 10 minutes
        { duration: '5m', target: 0 }, // ramp-down to 0 users
    ],
    thresholds: {
        // A collection of threshold specifications to configure under what condition(s) 
        // a test is considered successful or not
        'http_req_duration': ['p(99)<1500'], // 99% of requests must complete below 1.5s
        'logged in successfully': ['p(99)<1500'], // 99% of requests must complete below 1.5s
    }
};

export default function () {
    // Here, we set the endpoint to test.
    const response = http.get('https://localhost:7037/WeatherForecast');

    // An assertion
    check(response, {
        'is status 200': (x) => x.status === 200
    });

    sleep(1);
}