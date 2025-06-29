const API_BASE = 'https://localhost:7279/api';

const ENTIDADES = {
    Articulo: ['codigo', 'nombre', 'estado', 'ubicacion', 'categoriaId'],
    Categoria: ['nombre'],
    Prestamo: ['articuloId', 'usuarioSolicitaId', 'fechaSolicitud', 'fechaEntrega', 'fechaDevolucion', 'estado']
};

const mostrarMensaje = (msg, ok = true) => {
    const div = document.getElementById('message');
    div.textContent = msg;
    div.style.color = ok ? 'green' : 'red';
    setTimeout(() => div.textContent = '', 5000);
};

function aplicarFiltroBusqueda() {
    const input = document.getElementById('buscador');
    const filtro = input.value.toLowerCase();
    const filas = document.querySelectorAll('#readonly-container table tbody tr');

    filas.forEach(fila => {
        const textoFila = fila.textContent.toLowerCase();
        fila.style.display = textoFila.includes(filtro) ? '' : 'none';
    });
}

async function cargarVistaSoloLectura(entidad) {
    const cont = document.getElementById('readonly-container');
    cont.innerHTML = "";

    const campos = ENTIDADES[entidad];
    const resp = await fetch(`${API_BASE}/${entidad}`);
    if (!resp.ok) return mostrarMensaje(`Error al cargar ${entidad}`, false);
    const data = await resp.json();

    let html = `<h2>${entidad}s</h2>
    <input type="text" id="buscador" placeholder="Buscar..." oninput="aplicarFiltroBusqueda()" class="buscador">
    <table><thead><tr><th>ID</th>`;
    campos.forEach(c => html += `<th>${c}</th>`);
    html += `</tr></thead><tbody>`;

    for (let item of data) {
        html += `<tr><td>${item[entidad.toLowerCase() + 'Id'] ?? item.id ?? ''}</td>`;
        campos.forEach(campo => {
            html += `<td>${item[campo] ?? ''}</td>`;
        });
        html += `</tr>`;
    }

    html += `</tbody></table>`;
    cont.innerHTML = html;
}

function construirMenu() {
    const menu = document.getElementById('menu');
    Object.keys(ENTIDADES).forEach(ent => {
        const li = document.createElement('li');
        const a = document.createElement('a');
        a.textContent = ent + 's';
        a.href = "#";
        a.onclick = e => {
            e.preventDefault();
            document.querySelectorAll('aside a').forEach(link => link.classList.remove('active'));
            a.classList.add('active');
            cargarVistaSoloLectura(ent);
        };
        li.appendChild(a);
        menu.appendChild(li);
    });
}

document.getElementById('logout').addEventListener('click', () => {
    localStorage.clear();
    window.location.href = 'login.html';
});

window.addEventListener('DOMContentLoaded', () => {
    construirMenu();
    const primera = Object.keys(ENTIDADES)[0];
    document.querySelector('#menu a')?.classList.add('active');
    cargarVistaSoloLectura(primera);
});





