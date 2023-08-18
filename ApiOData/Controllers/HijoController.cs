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

        public HijoController(PersonaDBContext context)
        {
            _context = context;
        }

        [EnableQuery]
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
        public ActionResult Post([FromBody] Hijo Hijo)
        {
            _context.Hijo.Add(Hijo);

            return Created(Hijo);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Hijo updatedHijo)
        {
            var Hijo = _context.Hijo.SingleOrDefault(d => d.Id == key);

            if (Hijo == null)
            {
                return NotFound();
            }

            Hijo.Carrera = updatedHijo.Carrera;
            Hijo.IdPersona = updatedHijo.IdPersona;
            Hijo.IdPadre = updatedHijo.IdPadre;
            _context.SaveChanges();

            return Updated(Hijo);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var customer = _context.Hijo.SingleOrDefault(d => d.Id == key);

            if (customer != null)
            {
                _context.Hijo.Remove(customer);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }
}
