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
    
    public class PersonaController : ODataController
    {
        private readonly PersonaDBContext _context;

        public PersonaController(PersonaDBContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[EnableQuery]
        //public ActionResult<IEnumerable<Persona>> Get()
        //{
        //    try
        //    {
        //        var personas = _context.Persona;
        //        return Ok(personas);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de la excepción
        //        Console.WriteLine($"Error al obtener personas: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
        //    }
        //}

        [EnableQuery(MaxExpansionDepth = 4)]
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
        public ActionResult Post([FromBody] Persona persona)
        {
            _context.Persona.Add(persona);

            return Created(persona);
        }

        [HttpPut]
        public ActionResult Put([FromRoute] int key, [FromBody] Persona updatedPersona)
        {
            var persona = _context.Persona.SingleOrDefault(d => d.Id == key);

            if (persona == null)
            {
                return NotFound();
            }

            persona.Apellido = updatedPersona.Apellido;
            persona.Nombre = updatedPersona.Nombre;
            persona.IdGenero = updatedPersona.IdGenero;
            persona.Edad = updatedPersona.Edad;

            _context.SaveChanges();

            return Updated(persona);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var customer = _context.Persona.SingleOrDefault(d => d.Id == key);

            if (customer != null)
            {
                _context.Persona.Remove(customer);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }
}
