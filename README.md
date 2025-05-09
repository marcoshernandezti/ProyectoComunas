Proyecto simple desarrollado en .NET 8
Se divide en 3 subproyectos.
* Datos --> La logica de conexión a la base de datos para consumir procedimientos almacenados. Por ahora no hay pruebas de conexión y se utiliza datos de pruebas en API para probar el proyecto.
* API --> Servicio API, que utiliza el proyecto Datos para devolver los resultados en formato json. Utiliza un Token fijo.
* WEB --> Capa de presentación, que consume los servicio API REST y muestra los resultados en MVC (muy parecido a .NET core 3.1). Por ahora no tiene seguridad

Adicionalmente se agrega.
* REgistro de log en los proyectos API y DATOS.
* Los script para la base de datos, se encuentran en la carpeta T-SQL

*** NOTA
* Se utiliza GitHubCopilot para generar gran parte del proyecto y revisar código con buenas prácticas.
* Por ahora, no tengo Base de datos para probar los servicios del API, para que consuma los SP de la base de datos.
