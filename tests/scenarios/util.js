import {check} from 'k6';

export function checker(response, tag, desc, statusCode) {
    const des = `[${tag}] - ${desc} - status is ${statusCode}`;

    check(response, {[des]: (r) => r.status === statusCode});
}

export function factoryHeaders() {
    return {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID(),
            'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjRVQzhiOVJFZl83cVlkUDRWUV9JeSJ9.eyJpc3MiOiJodHRwczovL2Rldi1lMWRyN3RlZTVxOGNxZ3prLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJkTHlOTkVwTnlsZzBRcWtiYkl5azJOM1dNWW5TQTN3cEBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly90YXhpLXNlcnZpY2UtYXBpIiwiaWF0IjoxNzUzNTcwODY2LCJleHAiOjE3NTM2NTcyNjYsInNjb3BlIjoidXNlcjpyZWFkIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiZEx5Tk5FcE55bGcwUXFrYmJJeWsyTjNXTVluU0Ezd3AifQ.Fa73nAoR0QbapRmyEdhqR3-7BJt-YWYvbkdRARKKC8oPH80fTIZ6pjxrx4XFzsGHX0CsTCs3H6WQZJTqmnugYSS9a1B939IxJtU195JA_4V2fJgR9LW-EjJWEyEnm4QUGYtejLpc4tF8soJinJ8MCPV2y56MS-hN2YLQVUBjUa8_bIAN6fCYJptS7GRIEljzEtl2FtfiTuC_n1bC8OiO_TYyewzJSEOpwOrXmcs7UojcSyObRlBJcB1wucT-8mveOF8a8Q_b1U_bCujrw1hOVOWztj1FOmei6pXaulzDZRTKkTdo9WHZkLOyLTJ20h_ZqqN8mn-1WzmtQapn-_ia4A'
        }
    };
}