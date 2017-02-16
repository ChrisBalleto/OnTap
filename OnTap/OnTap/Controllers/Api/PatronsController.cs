using OnTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using OnTap.Dtos;
using System.Web.Http;
using AutoMapper;

namespace OnTap.Controllers.Api
{
    public class PatronsController : ApiController
    {
        private ApplicationDbContext _context;
        public PatronsController()
        {
            _context = new ApplicationDbContext();

        }
        public IEnumerable<PatronDto> GetPatrons()
        {
            return _context.Patrons.ToList().Select(Mapper.Map<Patron, PatronDto>);

        }
        public IHttpActionResult GetPatron(int id)
        {
            var patron = _context.Patrons.SingleOrDefault(c => c.Id == id);

            if (patron == null)
                return NotFound();

            return Ok(Mapper.Map<Patron, PatronDto>(patron));
        }
        [HttpPost]
        public IHttpActionResult CreatePatron(PatronDto patronDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var patron = Mapper.Map<PatronDto, Patron>(patronDto);
            _context.Patrons.Add(patron);
            _context.SaveChanges();

            patronDto.Id = patron.Id;

            return Created(new Uri(Request.RequestUri + "/" + patron.Id), patronDto);
        }

        [HttpPut]
        public void UpdatePatron(int id, PatronDto patronDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var patronInDb = _context.Patrons.SingleOrDefault(c => c.Id == id);

            if (patronDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map<PatronDto, Patron>(patronDto, patronInDb);
            

            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeletePatron(int id)
        {
            var patronInDb = _context.Patrons.SingleOrDefault(c => c.Id == id);

            if (patronInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Patrons.Remove(patronInDb);
            _context.SaveChanges();

        }


    }
}
