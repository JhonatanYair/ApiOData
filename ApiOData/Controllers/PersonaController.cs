
using ApiOData.Datos;
using ApiOData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ApiOData.Controllers
{
    public class PersonaController : ODataController
    {

        private readonly PersonaDBContext _context;

        public  PersonaController(PersonaDBContext context)
        {
            _context = context;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<Persona> Get(ODataQueryOptions<Persona> options)
        {
            if (options == null || options.Filter == null)
            {
                // Handle the case where options or Filter is null
                return _context.Persona.AsQueryable();
            }
            IQueryable results = options.Filter.ApplyTo(_context.Persona.AsQueryable(), new ODataQuerySettings());
            return results as IQueryable<Persona>;
        }

        [HttpPost]
        public ActionResult Post([FromBody]  Persona  Persona)
        {
           _context.Persona.Add( Persona);
           _context.SaveChanges();
           return Created( Persona);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Persona updatedPersona)
        {
            var Persona = _context.Persona.SingleOrDefault(d => d.Id == key);

            if (Persona == null)
            {
                return NotFound();
            }

            // Obtener los campos de Persona usando reflexión
            var campos = typeof(Persona).GetProperties();

            foreach (var campo in campos)
            {
                // Ignorar campos "Id" o "Uuid"
                if (campo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    campo.Name.Equals("Uuid", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var valorActualizado = campo.GetValue(updatedPersona);
                if (valorActualizado != null)
                {
                    campo.SetValue(Persona, valorActualizado);
                }
            }

            _context.SaveChanges();

            return Ok(Persona);
        }
 
        public ActionResult Delete([FromRoute] int key)
        {
            var  Persona = _context.Persona.SingleOrDefault(d => d.Id == key);

            if ( Persona != null)
            {
                _context.Persona.Remove(Persona);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }    
}

