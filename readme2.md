# Proyecto 1 - Pasos Iniciales

## Crear Proyecto

1. Crear una solución en blanco llamada PazYSalvoAPP.

## Crear Capas
### Capa de presentación, Modelo de datos, Datos y Lógica de negocio

1. Clic derecho en la solución.
2. Agregar > Nuevo Proyecto.
3. Selecciona "Biblioteca de clases".
4. Nombra los proyectos como Proyecto.Models, Proyecto.DLL, y Proyecto.BLL.

Para la capa de presentación, crea un proyecto MVC .NET Core.

## Capa de Datos - Configuraciones Iniciales
Esta capa conecta con la base de datos. Sigue estos pasos:

1. Define la capa de datos como proyecto de inicio.
2. Administra paquetes Nuget e instala:
EntityFrameworkCore
EntityFrameworkCore.SqlServer
EntityFrameworkCore.Tools
3. Agrega una carpeta para el contexto y configura la conexión a la base de datos:

Scaffold-DbContext "Server=(local); DataBase=PazYSalvo; Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir context

Una vez conectada, tu contexto de base de datos estará listo para usar.

## Referencia entre Capas

1. Capa de datos referencia a Models.
2. Capa de negocio referencia a Data y Models.
3. Capa de presentación referencia a Business y Models.

## Configurar la Cadena de Conexión
En la capa de presentación, abre appsettings.json y agrega la cadena de conexión:

"ConnectionStrings": {
    "connString": "Data Source=MI_SERVIDOR;Initial Catalog=PazSalvo;Integrated Security=true;Encrypt=True;TrustServerCertificate=true;"
}

Luego, en program.cs, inyecta la cadena de conexión:

builder.Services.AddDbContext<PazSalvoContext>(c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
});


## Configuración del Data Access Layer
1. Crea una carpeta Repositories.
2. Crea la interfaz IGenericRepository para las operaciones CRUD:


public interface IGenericRepository<T> where T : class
{
    Task<bool> Insertar(T model);
    Task<bool> Actualizar(T model);
    Task<T> Leer(int id);
    Task<IQueryable> LeerTodos();
    Task<bool> Eliminar(int id);
}

3. Implementa la interfaz en tu repositorio, por ejemplo, FacturaRepository:

public class FacturaRepository : IGenericRepository<Factura>
{
    private readonly PazSalvoContext _context;

    public FacturaRepository(PazSalvoContext context)
    {
        _context = context;
    }

    public async Task<bool> Insertar(Factura model)
    {
        _context.Facturas.Add(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Actualizar(Factura model)
    {
        _context.Facturas.Update(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Factura> Leer(int id)
    {
        return await _context.Facturas.FindAsync(id);
    }

    public async Task<IQueryable> LeerTodos()
    {
        return _context.Facturas;
    }

    public async Task<bool> Eliminar(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura != null)
        {
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}

# Capa Lógica de Negocio
1. Crea una carpeta Services.
2. Define una interfaz para cada servicio y su implementación, por ejemplo:

public interface IFacturaService
{
    Task<bool> Insertar(Factura model);
    Task<bool> Actualizar(Factura model);
    Task<Factura> Leer(int id);
    Task<IQueryable<Factura>> LeerTodos();
    Task<bool> Eliminar(int id);
    Task<Factura> ObtenerFacturasPorServicio(int id);
}

public class FacturaService : IFacturaService
{
    private readonly IGenericRepository<Factura> _repository;

    public FacturaService(IGenericRepository<Factura> repository)
    {
        _repository = repository;
    }

    public Task<bool> Insertar(Factura model) => _repository.Insertar(model);
    public Task<bool> Actualizar(Factura model) => _repository.Actualizar(model);
    public Task<Factura> Leer(int id) => _repository.Leer(id);
    public Task<IQueryable<Factura>> LeerTodos() => _repository.LeerTodos();
    public Task<bool> Eliminar(int id) => _repository.Eliminar(id);

    public async Task<Factura> ObtenerFacturasPorServicio(int id)
    {
        var facturas = await _repository.LeerTodos();
        return facturas.FirstOrDefault(f => f.ServicioAdquiridoId == id);
    }
}

### Duver Alexander Soto Alzate
