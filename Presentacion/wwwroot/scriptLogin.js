// Mostrar/Ocultar contraseña
document.getElementById("togglePassword").addEventListener("click", function () {
    const passwordInput = document.getElementById("password");
    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        this.innerText = "🙈";
    } else {
        passwordInput.type = "password";
        this.innerText = "👁";
    }
});

// Formulario de login
document.getElementById("loginForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch("https://localhost:7279/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (response.ok) {
        const data = await response.json();
        const rol = data.rol.toLowerCase();

        // Guardar usuarioId y rol
        localStorage.setItem("rol", rol);
        localStorage.setItem("usuarioId", data.usuario.usuarioId);

        if (rol === "administrador") {
            window.location.href = "admin.html";
        } else if (rol === "operador") {
            window.location.href = "operador.html";
        } else {
            window.location.href = "invitado.html";
        }
    } else {
        const err = await response.json();
        document.getElementById("error").innerText = err.message || "Error en la autenticación";
    }
});

// Botón "Entrar como invitado"
document.getElementById("guestLogin").addEventListener("click", function () {
    window.location.href = "invitado.html";
});
