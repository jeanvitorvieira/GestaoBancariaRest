import http from 'k6/http';
import { check } from 'k6';

export const options = {
    vus: 1,
    iterations: 1000
};

const headers = { 'Content-Type': 'application/json' };

export default function () {
    const tipoMovimento = __ITER % 2 === 0 ? 0 : 1;

    const payload = JSON.stringify({
        contaBancariaId: 1,
        valor: 10.00,
        tipoMovimento: tipoMovimento,
        descricao: `Movimento seed #${__ITER}`
    });

    const res = http.post(
        'http://localhost:5236/api/contasbancarias/movimentos',
        payload,
        { headers }
    );

    check(res, { 'inserido': (r) => r.status === 200 });
}