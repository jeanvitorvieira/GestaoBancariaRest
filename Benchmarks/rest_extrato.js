import http from 'k6/http';
import { check, sleep } from 'k6';
import { Trend, Rate, Counter } from 'k6/metrics';

const latencia = new Trend('latencia_ms', true);
const taxaErro = new Rate('taxa_erro');
const bytesRecebidos = new Counter('bytes_recebidos');

export const options = {
    stages: [
        { duration: '10s', target: 10 },
        { duration: '30s', target: 50 },
        { duration: '10s', target: 0  },
    ],
    thresholds: {
        latencia_ms: ['p(95)<1000'],
        taxa_erro: ['rate<0.01'],
    },
};

export default function () {
    const res = http.get('http://localhost:5236/api/contasbancarias/1/extrato');

    latencia.add(res.timings.duration);
    taxaErro.add(res.status !== 200);
    bytesRecebidos.add(res.body.length);

    check(res, {
        'status 200': (r) => r.status === 200,
        'tem movimentos': (r) => {
            const body = JSON.parse(r.body);
            return body.movimentos.length > 0;
        },
    });

    sleep(1);
}