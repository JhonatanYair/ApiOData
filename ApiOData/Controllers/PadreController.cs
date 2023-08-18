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

        public PadreController(PersonaDBContext context)
        {
            _context = context;
        }

        //[EnableQuery]
        //public ActionResult<IEnumerable<Padre>> Get()
        //{
        //    var padres = _context.Padre;
        //    return Ok(padres);
        //}

        [EnableQuery]
        public IQueryable<Padre> Get(ODataQueryOptions<Padre> options)
        {
            if (options == null || options.Filter == null)
            {
                // Handle the case where options or Filter is null
                return _context.Padre.AsQueryable();
            }
            IQueryable results = options.Filter.ApplyTo(_context.Persona.AsQueryable(), new ODataQuerySettings());
            return results as IQueryable<Padre>;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Padre Padre)
        {
            _context.Padre.Add(Padre);

            return Created(Padre);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Padre updatedPadre)
        {
            var Padre = _context.Padre.SingleOrDefault(d => d.Id == key);

            if (Padre == null)
            {
                return NotFound();
            }

            Padre.Ocupacion = updatedPadre.Ocupacion;
            Padre.IdPersona = updatedPadre.IdPersona;
            Padre.IsWork = updatedPadre.IsWork;
            _context.SaveChanges();

            return Updated(Padre);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var customer = _context.Padre.SingleOrDefault(d => d.Id == key);

            if (customer != null)
            {
                _context.Padre.Remove(customer);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }
}
