import http from 'k6/http';
import { check, sleep } from 'k6';
import { Trend, Rate } from 'k6/metrics';

const latencia = new Trend('latencia_ms', true);
const taxaErro = new Rate('taxa_erro');

export const options = {
    stages: [
        { duration: '10s', target: 10 },
        { duration: '30s', target: 50 },
        { duration: '10s', target: 0  },
    ],
    thresholds: {
        latencia_ms: ['p(95)<500'],
        taxa_erro: ['rate<0.05'],
    },
};

export default function () {
    const payload = JSON.stringify({
        contaBancariaId: 1,
        valor: 10.00,
        tipoMovimento: 0,
        descricao: 'Teste de carga k6'
    });

    const headers = { 'Content-Type': 'application/json' };

    const res = http.post(
        'http://localhost:5236/api/contasbancarias/movimentos',
        payload,
        { headers }
    );

    latencia.add(res.timings.duration);
    taxaErro.add(res.status !== 200);

    check(res, {
        'status 200': (r) => r.status === 200,
    });

    sleep(1);
}