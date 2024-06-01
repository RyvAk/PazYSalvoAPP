# Proyecto 1 - Pasos Iniciales

## Crear proyecto

<<<<<<< Updated upstream
1. Crear solución en blanco: PazYSalvoAPP
=======
1. Crear solución en blanco: **PazYSalvoAPP**
>>>>>>> Stashed changes

## Crear Capas

### Capa de presentación, Capa del Modelo de datos, Datos y Lógica de negocio

1. Clic derecho en la solución
2. Agregar, opción Nuevo Proyecto
3. Selecciona la opción de proyecto biblioteca de clases: 
![](./assets//bibClases.png)
4. Lo normal será nombrar de la siguiente manera: Proyecto.Models, Proyecto.DLL, Proyecto.BLL

Para la capa de presentación, se creará un proyecto MVC .NET Core.
![](./assets/webApp.png)

### Capa Datos - Configuraciones Iniciales
Esta capa nos permitirá la conexión a la BD y la utilización de recursos, para esto instalaremos y utilizaremos los siguientes paquetes:

1. Clic derecho en capa datos, **Definir proyecto como proyecto de inicio**,
2. Clic derecho en capa datos, Administrar paquetes Nuget
3. Instalaremos las siguientes librerías:
    * **EntityFrameWorkCore:** biblioteca principal de Entity Framework Core. Proporciona funcionalidades básicas de mapeo objeto-relacional (ORM) para trabajar con bases de datos en aplicaciones .NET Core. Esta biblioteca incluye las clases y métodos necesarios para definir modelos de datos, realizar consultas, realizar operaciones de actualización y persistir datos en la base de datos.
    * **EntityFrameWorkCore.SqlServer:** es un proveedor específico de base de datos para SQL Server en Entity Framework Core. Proporciona compatibilidad específica para SQL Server, lo que permite a Entity Framework Core interactuar con bases de datos SQL Server de manera eficiente. Esta biblioteca incluye clases y métodos que permiten la generación de consultas SQL específicas de SQL Server, optimizaciones de rendimiento y funcionalidades específicas de este motor de base de datos.
    * **EntityFrameWorkCore.Tools:** biblioteca que proporciona herramientas de línea de comandos para Entity Framework Core. Estas herramientas son útiles para realizar tareas comunes durante el desarrollo de aplicaciones, como la creación de migraciones de base de datos, la aplicación de migraciones, la generación de clases de contexto de base de datos a partir de una base de datos existente, y más. EntityFrameworkCore.Tools simplifica el proceso de administración y mantenimiento de la base de datos en tu aplicación utilizando Entity Framework Core.

Una vez instalados los paquetes, agregaremos unos cuantos recursos más de forma manual:

1. Una carpeta para el contexto,
    * **Contexto:** representa una sesión de trabajo con la base de datos, donde se pueden realizar consultas, insertar, actualizar y eliminar registros. 
2. Establecer esta capa como proyecto de inicio
3. Enlazar nuestro modelo de datos a nuestra bd
    * `Scaffold-DbContext "Server=(local); DataBase=PazYSalvo; Integrated Security=true"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir context`
    * Scaffold-DbContext "Data Source=DESKTOP-88MTFLQ\SQLEXPRESS;Initial Catalog=PazSalvo;Integrated Security=true;Encrypt=True;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Context.

Una vez establecida nuestra conexión, podremos visualizar el contexto asociado a nuestro modelo de datos:

```cs
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.Models;

public partial class PazSalvoContext : DbContext
{
    public PazSalvoContext()
    {
    }

    public PazSalvoContext(DbContextOptions<PazSalvoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesDeFactura> DetallesDeFacturas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MediosDePago> MediosDePagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC0768B1D232");

            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Clientes__Person__59FA5E80");

            entity.HasOne(d => d.Rol).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Clientes__RolId__5AEE82B9");
        });

        modelBuilder.Entity<DetallesDeFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3214EC07DE5B9E42");

            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Factura).WithMany(p => p.DetallesDeFacturas)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__DetallesD__Factu__6E01572D");

            entity.HasOne(d => d.Pago).WithMany(p => p.DetallesDeFacturas)
                .HasForeignKey(d => d.PagoId)
                .HasConstraintName("FK__DetallesD__PagoI__6EF57B66");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC07800A561E");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3214EC07BC5E51CA");

            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Saldo)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Facturas__Client__619B8048");

            entity.HasOne(d => d.Estado).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK__Facturas__Estado__6477ECF3");

            entity.HasOne(d => d.MedioDePago).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.MedioDePagoId)
                .HasConstraintName("FK__Facturas__MedioD__6383C8BA");

            entity.HasOne(d => d.ServicioAdquirido).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ServicioAdquiridoId)
                .HasConstraintName("FK__Facturas__Servic__628FA481");
        });

        modelBuilder.Entity<MediosDePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MediosDe__3214EC072DC27202");

            entity.ToTable("MediosDePago");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC078704C71A");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoDePago)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Factura).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__Pagos__FacturaId__693CA210");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC07959AA70C");

            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.DocumentoIdentificacion).HasMaxLength(20);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07897A08BD");

            entity.Property(e => e.Activo).HasDefaultValueSql("((0))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC0715665506");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC075BD745D6");

            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);

            entity.HasOne(d => d.Persona).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Usuarios__Person__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

```

## Referencia entre capas

La referencia entre capas es lo que nos permite dar abstracción a nuestra aplicación, consiste en hacer referencias entre los proyectos, estableciendo una comunicación directa. Dependerá de la misma lógica del proyecto determinar cómo se deben comunicar las capas, pero como estandar, manejaremos las referencias de la siguiente manera:

1. nuestra capa de datos, solo tendrá referencia a `models`. 
2. Nuestra capa de negocio hará referencia tanto a la capa `data`, como a `models`
3. Finalmente, nuestra capa de presentación hará referencia a nuestra capa `business` y `models`.

## Configurar la cadena de conexión

Ubicados en la capa de presentación, abrimos el archivo `appsettings.json`, allí haremos el siguiente ajuste:

1. En `appsettings.json` agregaremos la cadena de conexión configurada inicialmente:
    * `Data Source=MI_SERVIDOR;Initial Catalog=PazSalvo;Integrated Security=true;Encrypt=True;TrustServerCertificate=true;`
2. Eliminar del archivo de nuestro contexto la configuración de conexión que está dentro del siguiente método:
    * `protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)`
3. En `program.cs` vamos a inyectar la referencia de la cadena de conexión así:
    ```cs
        // program.cs
        // Cadena de conexión
        builder.Services.AddDbContext<PazSalvoContext>( c =>
        {
            c.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
        });
    ```


**********************************

**********************************

## Configuración del data access layer

Primero crearemos una nueva carpeta para nuestro repositorio, en donde indicaremos las operaciones que vamos a gestionar:

Acá vamos a crear una interfaz, esta (`IGenericRepository`) define un conjunto de métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) genéricas en un repositorio de datos, donde `T` representa el tipo de entidad que se manipulará en el repositorio

1. `public interface IGenericRepository<T> where T : class`: Declara una interfaz pública llamada IGenericRepository que toma un parámetro de tipo genérico T. La interfaz está restringida a tipos de referencia (where T : class). Esto significa que T debe ser una clase y no un tipo de valor.

    ```cs
    namespace PazYSalvoAPP.Data.Repositories
    {
        // Declaración de la interfaz IGenericRepository<T>
        public interface IGenericRepository<T> where T : class
        {
            // Declaración de método para insertar un objeto de tipo T en la base de datos
            Task<bool> Insertar(T model);

            // Declaración de método para actualizar un objeto de tipo T en la base de datos
            Task<bool> Actualizar(T model);

            // Declaración de método para leer un objeto de tipo T de la base de datos mediante su identificador único
            Task<T> Leer(int id);

            // Declaración de método para leer todos los objetos de tipo T de la base de datos
            // Este método devuelve una consulta IQueryable, que permite realizar operaciones de consulta sobre la colección de objetos de tipo T
            Task<IQueryable> LeerTodos();

            // Declaración de método para eliminar un objeto de tipo T de la base de datos mediante su identificador único
            Task<bool> Eliminar(int id);
        }
    }

    ```

2. Configura cada uno de tus entidades en donde estas hereden de esta interfaz, como se muestra en el ejemplo a continuación:

    ```cs
    using Microsoft.EntityFrameworkCore;
    using PazYSalvoAPP.Data.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace PazYSalvoAPP.Data.Repositories
    {
        public class FacturaRepository : IGenericRepository<Factura>
        {
            // 1- Establecer conexión
            private readonly PazSalvoContext _context;
            public FacturaRepository(PazSalvoContext context)
            {
                _context = context;
            }

            public async Task<bool> Actualizar(Factura model)
            {
                bool result = default(bool);
                try
                {
                    _context.Facturas.Update(model);
                    await _context.SaveChangesAsync();

                    return !result;
                }
                catch (Exception ex) 
                {
                    return result;
                }
            }

            public async Task<bool> Eliminar(int id)
            {
                bool result = default(bool);
                var factura = _context.Facturas.FirstOrDefault(f => f.Id == id);
                if (factura == null) return result;

                try
                {
                    _context.Facturas.Remove(factura);
                    await _context.SaveChangesAsync();

                    return !result;
                }
                catch (Exception ex) 
                {
                    return result;
                }
            }

            public async Task<bool> Insertar(Factura model)
            {
                bool result = default(bool);
                try
                {
                    _context.Facturas.Add(model);
                    await _context.SaveChangesAsync();

                    return !result;
                }
                catch (Exception ex)
                {
                    return result;
                }
            }

            public async Task<Factura> Leer(int id)
            {
                if (id == 0) return null;
                var factura = _context.Facturas.FirstAsync(f => f.Id == id);
                if (factura == null) return null;

                return await factura;       
                
                            
            }

            public async Task<IQueryable> LeerTodos()
            {
                IQueryable<Factura> listaDeFacturas = _context.Facturas;

                return listaDeFacturas;
            }
        }
    }

    ```

La clase `FacturaRepository` implementa todos los métodos definidos en la interfaz `IGenericRepository<Factura>`. Cada método realiza una operación específica en la base de datos relacionada con las facturas.


# Capa Lógica de negocio

1. Crearemos una carpeta de servicios (`services`),
2. Crearemos una interfaz por cada modelo, en donde se definirá el comportamiento de cada uno de estos,
3. Finalmente, crearemos una clase asociada a cada una, en donde aplicaremos la lógica que nos permita hacer las distintas operaciones necesarias para nuestro negocio.
    * **Definición de la clase:** La clase `FacturaService` se encuentra en el espacio de nombres PazYSalvoAPP.Business.Services, lo que indica su función en la capa de lógica de negocio. Implementa la interfaz `IFacturaService`, que contiene las diferentes operaciones que esta podrá implementar.
    * **Uso del repositorio:** El servicio delega la lógica de acceso a datos a la instancia del repositorio inyectado (`_modelRepository`). Esta separación de responsabilidades promueve la mantenibilidad y la capacidad de prueba del código.

    ```cs
    using PazYSalvoAPP.Data.Context; // Espacio de nombres para las clases de contexto de datos (probablemente para interacción con la base de datos)
    using PazYSalvoAPP.Data.Repositories; // Espacio de nombres para interfaces o clases de repositorio (abstracciones para acceso a datos)
    using System; 
    using System.Collections.Generic; 
    using System.Linq; // Para LINQ (Language Integrated Query) para manipulación de datos
    using System.Text; // Para manipulación de cadenas
    using System.Threading.Tasks; // Para construcciones de programación asincrónica

    namespace PazYSalvoAPP.Business.Services
    {
        public class FacturaService : IFacturaService // La clase FacturaService implementa la interfaz IFacturaService
        {
            private readonly IGenericRepository<Factura> _modelRepository; // Campo privado readonly para almacenar el repositorio inyectado

            public FacturaService(IGenericRepository<Factura> modelRepository) // Constructor para inyección de dependencias
            {
                _modelRepository = modelRepository; // Asigna el repositorio inyectado al campo
            }

            public async Task<bool> Actualizar(Factura model) // Método asincrono para actualizar un objeto Factura
            {
                return await _modelRepository.Actualizar(model); // Llama al método actualizar del repositorio y devuelve su resultado
            }

            public async Task<bool> Eliminar(int id) // Método asincrono para eliminar una Factura por ID (aún no implementado)
            {
                throw new NotImplementedException(); // marcador de posición para una implementación futura
            }

            public async Task<bool> Insertar(Factura model) // Método asincrono para insertar un objeto Factura
            {
                return await _modelRepository.Insertar(model); // Llama al método insertar del repositorio y devuelve su resultado
            }

            public async Task<Factura> Leer(int id) // Método asincrono para leer una Factura por ID
            {
                return await _modelRepository.Leer(id); // Llama al método leer por ID del repositorio y devuelve su resultado
            }

            public async Task<IQueryable<Factura>> LeerTodos() // Método asincrono para leer todos los objetos Factura
            {
                return await _modelRepository.LeerTodos(); // Llama al método leer todos del repositorio y devuelve un IQueryable para filtrado posterior
            }

            public async Task<Factura> ObtenerFacturasPorServicio(int id) // Método asincrono para obtener Facturas vinculadas a un ID de Servicio específico
            {
                var facturasPorServicio = await _modelRepository.LeerTodos(); // Obtiene todas las Facturas
                Factura factura = facturasPorServicio.Where(f => f.ServicioAdquiridoId == id).FirstOrDefault(); // Filtra por ID de Servicio y devuelve la primera coincidencia

                return factura; // Devuelve la Factura filtrada o null si no se encuentra ninguna coincidencia
            }
        }
    }

    ```




