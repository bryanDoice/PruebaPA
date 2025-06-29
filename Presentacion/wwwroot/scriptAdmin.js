
// scriptAdmin.js
const API_BASE = 'https://localhost:7279/api';

const ENTIDADES = {
    Usuario: ['nombre', 'email', 'hashPassword', 'rolId'],
    Rol: ['nombre'],
    Categoria: ['nombre'],
    Articulo: ['codigo', 'nombre', 'estado', 'ubicacion', 'categoriaId'],
    Prestamo: ['articuloId', 'usuarioSolicitaId', 'usuarioApruebaId', 'fechaSolicitud', 'fechaEntrega', 'fechaDevolucion', 'estado'],
    Auditoria: ['usuarioId', 'accion', 'fecha', 'entidad', 'entidadId', 'antes', 'despues']
};

const mostrarMensaje = (msg, ok = true) => {
    const div = document.getElementById('message');
    div.textContent = msg;
    div.style.color = ok ? 'green' : 'red';
    setTimeout(() => (div.textContent = ''), 5000);
};

async function generarHash() {
    const password = document.getElementById('plainPassword').value;
    if (!password) return mostrarMensaje('Escribe una contraseña para hashear', false);

    const res = await fetch(`${API_BASE}/Auth/hash`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(password)
    });

    if (res.ok) {
        const hash = await res.text();
        document.getElementById('hashOutput').value = hash;
        mostrarMensaje('Hash generado correctamente');
    } else {
        mostrarMensaje('Error al generar hash', false);
    }
}

function aplicarFiltroBusqueda() {
    const input = document.getElementById('buscador');
    const filtro = input.value.toLowerCase();
    const filas = document.querySelectorAll('#crud-container table tbody tr');

    filas.forEach(fila => {
        const celdas = fila.querySelectorAll('td');
        const textoFila = Array.from(celdas).map(td => td.textContent.toLowerCase()).join(' ');
        fila.style.display = textoFila.includes(filtro) ? '' : 'none';
    });
}

async function loadEntity(entity) {
    const cont = document.getElementById('crud-container');
    const campos = ENTIDADES[entity];
    const resp = await fetch(`${API_BASE}/${entity}`);
    if (!resp.ok) return mostrarMensaje(`Error al cargar ${entity}`, false);
    const data = await resp.json();

    let html = `<h2 id="${entity}">${entity}s</h2>
    <input type="text" id="buscador" placeholder="Buscar..." oninput="aplicarFiltroBusqueda()" class="buscador">`;

    html += `<div class="form-row" id="form-${entity}">`;
    campos.forEach(c => (html += `<input placeholder="${c}" id="${entity}-${c}">`));
    html += `<button onclick="addItem('${entity}')">Agregar</button></div>`;

    html += `<table><thead><tr><th>ID</th>`;
    campos.forEach(c => (html += `<th>${c}</th>`));
    html += `<th>Acciones</th></tr></thead><tbody>`;

    for (const item of data) {
        const idProp = entity.charAt(0).toLowerCase() + entity.slice(1) + 'Id';
        const id = item[idProp] ?? item.id;
        html += `<tr><td>${id}</td>`;
        campos.forEach(c => (html += `<td contenteditable>${item[c] ?? ''}</td>`));
        html += `<td>
            <button onclick="saveItem(this,'${entity}',${id})">Guardar</button>
            <button onclick="deleteItem('${entity}',${id})">Eliminar</button>
        </td></tr>`;
    }

    html += '</tbody></table>';
    cont.innerHTML = html;
}

async function saveItem(btn, entity, id) {
    const tr = btn.closest('tr');
    const tds = tr.querySelectorAll('td[contenteditable]');
    const campos = ENTIDADES[entity];
    const body = {};
    const idProp = entity.charAt(0).toLowerCase() + entity.slice(1) + 'Id';
    body[idProp] = id;

    for (let i = 0; i < campos.length; i++) {
        const valor = tds[i].innerText.trim();
        if (!valor) return mostrarMensaje(`Campo '${campos[i]}' no puede estar vacío`, false);
        body[campos[i]] = isNaN(valor) ? valor : parseInt(valor, 10);
    }

    const res = await fetch(`${API_BASE}/${entity}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });

    res.ok ? mostrarMensaje(`${entity} actualizado`) : mostrarMensaje(await res.text(), false);
    if (res.ok) loadEntity(entity);
}

async function deleteItem(entity, id) {
    if (!confirm(`¿Eliminar ${entity} ${id}?`)) return;
    const res = await fetch(`${API_BASE}/${entity}/${id}`, { method: 'DELETE' });
    res.ok ? mostrarMensaje(`${entity} eliminado`) : mostrarMensaje(await res.text(), false);
    if (res.ok) loadEntity(entity);
}

async function addItem(entity) {
    const campos = ENTIDADES[entity];
    const body = {};
    for (const campo of campos) {
        const valor = document.getElementById(`${entity}-${campo}`).value.trim();
        if (!valor) return mostrarMensaje(`Campo '${campo}' no puede estar vacío`, false);
        body[campo] = isNaN(valor) ? valor : parseInt(valor, 10);
    }

    const res = await fetch(`${API_BASE}/${entity}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });

    res.ok ? mostrarMensaje(`${entity} agregado correctamente`) : mostrarMensaje(await res.text(), false);
    if (res.ok) loadEntity(entity);
}

function buildMenu() {
    const ul = document.getElementById('menu');
    Object.keys(ENTIDADES).forEach(ent => {
        const li = document.createElement('li');
        const a = document.createElement('a');
        a.textContent = ent + 's';
        a.href = '#';
        a.onclick = e => {
            e.preventDefault();
            document.querySelectorAll('aside a').forEach(n => n.classList.remove('active'));
            a.classList.add('active');
            loadEntity(ent);
        };
        li.appendChild(a);
        ul.appendChild(li);
    });
}

document.getElementById('logout').addEventListener('click', () => {
    localStorage.clear();
    window.location.href = 'login.html';
});

window.addEventListener('DOMContentLoaded', () => {
    buildMenu();
    const first = Object.keys(ENTIDADES)[0];
    document.querySelector('#menu a').classList.add('active');
    loadEntity(first);
});

