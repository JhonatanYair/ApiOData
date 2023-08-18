using System;
using System.Collections.Generic;
using System.Linq;
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

        public GeneroController(PersonaDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Genero>> Get()
        {
            var generos = _context.Genero;
            Console.WriteLine();
            return Ok(generos);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genero genero)
        {
            _context.Genero.Add(genero);

            return Created(genero);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Genero updatedgenero)
        {
            var genero = _context.Genero.SingleOrDefault(d => d.Id == key);

            if (genero == null)
            {
                return NotFound();
            }

            genero.Genero1=updatedgenero.Genero1;
            _context.SaveChanges();

            return Updated(genero);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var customer = _context.Genero.SingleOrDefault(d => d.Id == key);

            if (customer != null)
            {
                _context.Genero.Remove(customer);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }
}
