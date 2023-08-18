

using ApiOData.Datos;
using ApiOData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ApiOData.Controllers
{
    public class GeneroController : ODataController
    {

        private readonly PersonaDBContext _context;

        public  GeneroController(PersonaDBContext context)
        {
            _context = context;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<Genero> Get(ODataQueryOptions<Genero> options)
        {
            if (options == null || options.Filter == null)
            {
                // Handle the case where options or Filter is null
                return _context.Genero.AsQueryable();
            }
            IQueryable results = options.Filter.ApplyTo(_context.Genero.AsQueryable(), new ODataQuerySettings());
            return results as IQueryable<Genero>;
        }

        [HttpPost]
        public ActionResult Post([FromBody]  Genero  Genero)
        {
           _context.Genero.Add( Genero);
           _context.SaveChanges();
           return Created( Genero);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Genero updatedGenero)
        {
            var Genero = _context.Genero.SingleOrDefault(d => d.Id == key);

            if (Genero == null)
            {
                return NotFound();
            }

            // Obtener los campos de Genero usando reflexión
            var campos = typeof(Genero).GetProperties();

            foreach (var campo in campos)
            {
                // Ignorar campos "Id" o "Uuid"
                if (campo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    campo.Name.Equals("Uuid", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var valorActualizado = campo.GetValue(updatedGenero);
                if (valorActualizado != null)
                {
                    campo.SetValue(Genero, valorActualizado);
                }
            }

            _context.SaveChanges();

            return Ok(Genero);
        }
 
        public ActionResult Delete([FromRoute] int key)
        {
            var  Genero = _context.Genero.SingleOrDefault(d => d.Id == key);

            if ( Genero != null)
            {
                _context.Genero.Remove(Genero);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }    
}

