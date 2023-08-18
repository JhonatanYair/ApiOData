using ApiOData.Datos;
using ApiOData.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Reflection.Emit;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<PersonaDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("cnpersona"));
        });

        builder.Services.AddControllers().AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        
    }

    private static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
        builder.EntitySet<Persona>("Persona");
        builder.EntitySet<Hijo>("Hijo");
        builder.EntitySet<Padre>("Padre");
        builder.EntitySet<Genero>("Genero");
        //builder.EntityType<Hijo>();
        //builder.EntityType<Padre>();
        return builder.GetEdmModel();
    }

}