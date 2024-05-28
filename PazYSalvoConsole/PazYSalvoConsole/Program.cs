using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.WriteLine("Entity Framework y SQLite!");


using (var db = new PazYSalvoContext())
{
    db.Database.EnsureCreated();
};

// CREAR LAS DISTINTAS ENTIDADES
public class Estado
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set;}
}

// CREAR EL CONTEXTO
public class PazYSalvoContext : DbContext
{
    // NOMBRE DE NUESTRA BD: 
    public string dataBase = "PazYSalvoAPP.db";
    DbSet<Estado> Estado { get; set; }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(connectionString: "Server=DESKTOP-9GLEDTL;Database=PazYSalvoAPP;User Id=john;", sqlServerOptionsAction: op => 
    {
        op.MigrationsAssembly(
            Assembly.GetExecutingAssembly().FullName
        );
    });

    base.OnConfiguring(optionsBuilder);
}

    // DISEÑO DE ENTIDADES
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Estado>().ToTable("Estados");
        modelBuilder.Entity<Estado>(e => { 
            e.HasKey(e => e.Id); // Definir llave primaria
        });
    }
}

// 

