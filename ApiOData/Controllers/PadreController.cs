
using ApiOData.Datos;
using ApiOData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ApiOData.Controllers
{
    public class PadreController : ODataController
    {

        private readonly PersonaDBContext _context;

        public  PadreController(PersonaDBContext context)
        {
            _context = context;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<Padre> Get(ODataQueryOptions<Padre> options)
        {
            if (options == null || options.Filter == null)
            {
                // Handle the case where options or Filter is null
                return _context.Padre.AsQueryable();
            }
            IQueryable results = options.Filter.ApplyTo(_context.Padre.AsQueryable(), new ODataQuerySettings());
            return results as IQueryable<Padre>;
        }

        [HttpPost]
        public ActionResult Post([FromBody]  Padre  Padre)
        {
           _context.Padre.Add( Padre);
           _context.SaveChanges();
           return Created( Padre);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Padre updatedPadre)
        {
            var Padre = _context.Padre.SingleOrDefault(d => d.Id == key);

            if (Padre == null)
            {
                return NotFound();
            }

            // Obtener los campos de Padre usando reflexión
            var campos = typeof(Padre).GetProperties();

            foreach (var campo in campos)
            {
                // Ignorar campos "Id" o "Uuid"
                if (campo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    campo.Name.Equals("Uuid", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var valorActualizado = campo.GetValue(updatedPadre);
                if (valorActualizado != null)
                {
                    campo.SetValue(Padre, valorActualizado);
                }
            }

            _context.SaveChanges();

            return Ok(Padre);
        }
 
        public ActionResult Delete([FromRoute] int key)
        {
            var  Padre = _context.Padre.SingleOrDefault(d => d.Id == key);

            if ( Padre != null)
            {
                _context.Padre.Remove(Padre);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }    
}

