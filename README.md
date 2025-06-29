
# 📦 PruebaPA - Sistema de Inventario Web

**PruebaPA** es una aplicación web desarrollada con ASP.NET Core, diseñada para gestionar un inventario empresarial con acceso controlado por roles. Cuenta con una arquitectura en capas que facilita el mantenimiento, escalabilidad y reutilización del código. Además, incluye una API RESTful y una interfaz web adaptada a los distintos tipos de usuarios (Administrador, Operador e Invitado).

---

## 🚀 Características principales

- Autenticación segura mediante hashing (`PasswordHasher`)
- Gestión completa (CRUD) de Usuarios, Roles, Categorías, Artículos, Préstamos y Auditorías
- Control de acceso por rol: Administrador, Operador e Invitado
- Interfaz web con HTML + JavaScript para cada rol
- API RESTful documentada con Swagger
- Arquitectura en capas: Entities, DataAccess, Business y Presentation

---

## 📂 Estructura del proyecto

| Carpeta        | Descripción                                                                 |
|----------------|------------------------------------------------------------------------------|
| `Entities/`    | Define las clases modelo (Usuario, Articulo, Prestamo, etc.) que representan las tablas de la base de datos. |
| `DataAccess/`  | Incluye el `DbContext` de Entity Framework Core y repositorios con interfaces e implementaciones para acceder a la base de datos. |
| `Business/`    | Contiene los DTOs y los servicios con la lógica de negocio para cada entidad. |
| `Presentacion/`| Implementa los controladores de la API RESTful y aloja las páginas HTML/JS para la interfaz de usuario. |
| `.bak`         | Archivo `InventarioDB_Prueba.bak` para restaurar la base de datos en SQL Server. |

---

## 🛠 Requisitos

- Visual Studio 2022+ o VS Code con soporte .NET
- .NET SDK 7.0 o superior
- SQL Server (Express, Developer o LocalDB)
- Navegador web moderno (Chrome, Edge, Firefox)

---

## 📦 Cómo descargar y descomprimir el proyecto

1. Ve al repositorio: https://github.com/bryanDoice/PruebaPA  
2. Haz clic en el botón verde `Code` y selecciona `Download ZIP`.
3. Una vez descargado el archivo ZIP, haz clic derecho y selecciona **"Extraer todo..."** o utiliza un software como WinRAR, 7-Zip o el explorador de archivos de Windows para descomprimirlo.
4. Abre la carpeta descomprimida.

---

## 🧩 Cómo restaurar la base de datos desde el archivo `.bak`

1. Abre SQL Server Management Studio (SSMS).
2. Conéctate a tu servidor de SQL Server.
3. Haz clic derecho en "Bases de datos" → `Restaurar base de datos...`.
4. Selecciona:
   - **Origen**: Dispositivo
   - Haz clic en "..." y agrega el archivo `InventarioDB_Prueba.bak` que está en el proyecto.
5. Cambia el nombre de la base de datos restaurada a: `InventarioDB`
6. Asegúrate de marcar la opción de sobrescribir archivos existentes (si aplica).
7. Haz clic en **Aceptar** para iniciar la restauración.

> ⚠️ Asegúrate de que el archivo `.bak` esté copiado en una ruta accesible para SQL Server, como `C:\Program Files\Microsoft SQL Server\MSSQLXX.SQLEXPRESS\MSSQL\Backup\` o similar.

---

## ⚙️ Cómo configurar y ejecutar el proyecto

### 1. Abrir el proyecto
- Abre `PruebaPA.sln` con Visual Studio 2022 o superior.

### 2. Configurar la cadena de conexión
- Edita `appsettings.json` que se encuentra dentro de la carpeta `Presentacion/`:

```json
"ConnectionStrings": {
  "BancoConnection": "Server=TU_SERVIDOR_SQL;Database=InventarioDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 3. Ejecutar la aplicación
- Establece el proyecto **Presentacion** como proyecto de inicio.
- Ejecuta la aplicación (`F5` o botón Iniciar en Visual Studio).
- Accede a:
  - 🔹 Swagger: https://localhost:7279/swagger
  - 🔹 Interfaz Web: https://localhost:7279/wwwroot/login.html

---

## 🧪 Interfaz web y roles

| Rol           | Accede a          | Permisos principales                                                             |
|---------------|------------------|----------------------------------------------------------------------------------|
| **Administrador** | `admin.html`      | CRUD completo de todas las entidades, generación de hashes                      |
| **Operador**      | `operador.html`   | CRUD limitado (categorías, artículos, préstamos, perfil propio)                |
| **Invitado**      | `invitado.html`   | Solo lectura, sin acceso a la API ni acciones de modificación                  |

---

## 🧠 Conclusión General

El proyecto **“PruebaPA”** es una solución web robusta para la gestión de inventario, construida con ASP.NET Core y una arquitectura en capas que asegura una estructura limpia y mantenible. Mediante una API RESTful, interfaz web adaptada por rol y autenticación segura, permite gestionar usuarios, artículos, préstamos y más. Es ideal para entornos educativos o empresariales que requieran control de inventario con trazabilidad y acceso seguro. El diseño modular, el uso de DTOs y buenas prácticas de codificación lo convierten en un ejemplo claro de aplicación profesional escalable y reutilizable.

---

## 🧾 Estructura detallada

```
Solución "PruebaPA" 
│
├── Entities/
│   └── Models/
│       ├── Usuario.cs
│       ├── Rol.cs
│       ├── Articulo.cs
│       ├── Prestamo.cs
│       ├── Categoria.cs         
│       └── Auditoria.cs
│
├── DataAccess/
│   ├── Context/
│   │   └── InventarioDbContext.cs
│   └── Repositories/
│       ├── Interfaces/
│       │   ├── IUsuarioRepository.cs
│       │   ├── IRolRepository.cs
│       │   ├── IArticuloRepository.cs
│       │   ├── IPrestamoRepository.cs
│       │   ├── ICategoriaRepository.cs
│       │   └── IAuditoriaRepository.cs
│       └── Implementations/
│           ├── UsuarioRepository.cs
│           ├── RolRepository.cs
│           ├── ArticuloRepository.cs
│           ├── PrestamoRepository.cs
│           ├── CategoriaRepository.cs
│           └── AuditoriaRepository.cs
│
├── Business/
│   ├── DTOs/
│   │   ├── UsuarioDTO.cs
│   │   ├── RolDTO.cs
│   │   ├── ArticuloDTO.cs
│   │   ├── PrestamoDTO.cs
│   │   ├── CategoriaDTO.cs
│   │   └── AuditoriaDTO.cs
│   └── Services/
│       ├── UsuarioService.cs
│       ├── RolService.cs
│       ├── ArticuloService.cs
│       ├── PrestamoService.cs
│       ├── CategoriaService.cs
│       └── AuditoriaService.cs
│
└── Presentation/          
    ├── Controllers/
    │   ├── UsuarioController.cs
    │   ├── RolController.cs
    │   ├── ArticuloController.cs
    │   ├── PrestamoController.cs
    │   ├── CategoriaController.cs
    │   └── AuditoriaController.cs                
    ├── appsettings.json               
    └── Program.cs
```

---

## 🧑‍💻 Autor

Proyecto desarrollado como parte de una prueba de **Programación Avanzada**.  
Repositorio público: https://github.com/bryanDoice/PruebaPA
