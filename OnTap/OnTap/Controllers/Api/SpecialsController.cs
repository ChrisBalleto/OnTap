using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnTap.Models;
using OnTap.Dtos;
using AutoMapper;

namespace OnTap.Controllers.Api
{
    public class SpecialsController : ApiController
    {
        private ApplicationDbContext _context;
        public SpecialsController()
        {
            _context = new ApplicationDbContext();

        }
        public IEnumerable<SpecialDto> GetSpecials()
        {
            return _context.Specials.ToList().Select(Mapper.Map<Special, SpecialDto>);

        }
        public IHttpActionResult GetSpecial(int id)
        {
            var special = _context.Specials.SingleOrDefault(c => c.Id == id);

            if (special == null)
                return NotFound();

            return Ok(Mapper.Map<Special, SpecialDto>(special));
        }
        [HttpPost]
        public IHttpActionResult CreateSpecial(SpecialDto patronDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var special = Mapper.Map<SpecialDto, Special>(patronDto);
            _context.Specials.Add(special);
            _context.SaveChanges();

            patronDto.Id = special.Id;

            return Created(new Uri(Request.RequestUri + "/" + special.Id), patronDto);
        }

        [HttpPut]
        public void UpdateSpecial(int id, SpecialDto patronDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var specialInDb = _context.Specials.SingleOrDefault(c => c.Id == id);

            if (patronDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map<SpecialDto, Special>(patronDto, specialInDb);


            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteSpecial(int id)
        {
            var specialInDb = _context.Specials.SingleOrDefault(c => c.Id == id);

            if (specialInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Specials.Remove(specialInDb);
            _context.SaveChanges();

        }
    }
}
