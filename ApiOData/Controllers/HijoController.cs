
using ApiOData.Datos;
using ApiOData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ApiOData.Controllers
{
    public class HijoController : ODataController
    {

        private readonly PersonaDBContext _context;

        public  HijoController(PersonaDBContext context)
        {
            _context = context;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<Hijo> Get(ODataQueryOptions<Hijo> options)
        {
            if (options == null || options.Filter == null)
            {
                // Handle the case where options or Filter is null
                return _context.Hijo.AsQueryable();
            }
            IQueryable results = options.Filter.ApplyTo(_context.Hijo.AsQueryable(), new ODataQuerySettings());
            return results as IQueryable<Hijo>;
        }

        [HttpPost]
        public ActionResult Post([FromBody]  Hijo  Hijo)
        {
           _context.Hijo.Add( Hijo);
           _context.SaveChanges();
           return Created( Hijo);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Hijo updatedHijo)
        {
            var Hijo = _context.Hijo.SingleOrDefault(d => d.Id == key);

            if (Hijo == null)
            {
                return NotFound();
            }

            // Obtener los campos de Hijo usando reflexión
            var campos = typeof(Hijo).GetProperties();

            foreach (var campo in campos)
            {
                // Ignorar campos "Id" o "Uuid"
                if (campo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    campo.Name.Equals("Uuid", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var valorActualizado = campo.GetValue(updatedHijo);
                if (valorActualizado != null)
                {
                    campo.SetValue(Hijo, valorActualizado);
                }
            }

            _context.SaveChanges();

            return Ok(Hijo);
        }
 
        public ActionResult Delete([FromRoute] int key)
        {
            var  Hijo = _context.Hijo.SingleOrDefault(d => d.Id == key);

            if ( Hijo != null)
            {
                _context.Hijo.Remove(Hijo);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }    
}

