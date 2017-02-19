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
    public class TapBeersController : ApiController
    {
        private ApplicationDbContext _context;
        public TapBeersController()
        {
            _context = new ApplicationDbContext();

        }
        public IEnumerable<TapBeerDto> GetTapBeers()
        {
            return _context.TapBeers.ToList().Select(Mapper.Map<TapBeer, TapBeerDto>);

        }
        public IHttpActionResult GetTapBeer(int id)
        {
            var tapBeer = _context.TapBeers.SingleOrDefault(c => c.Id == id);

            if (tapBeer == null)
                return NotFound();  

            return Ok(Mapper.Map<TapBeer, TapBeerDto>(tapBeer));
        }
        [HttpPost]
        public IHttpActionResult CreateTapBeer(TapBeerDto tapBeerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tapBeer = Mapper.Map<TapBeerDto, TapBeer>(tapBeerDto);
            _context.TapBeers.Add(tapBeer);
            _context.SaveChanges();

            tapBeerDto.Id = tapBeer.Id;

            return Created(new Uri(Request.RequestUri + "/" + tapBeer.Id), tapBeerDto);
        }

        [HttpPut]
        public void UpdateTapBeer(int id, TapBeerDto tapBeerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var tapBeerInDb = _context.TapBeers.SingleOrDefault(c => c.Id == id);

            if (tapBeerDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map<TapBeerDto, TapBeer>(tapBeerDto, tapBeerInDb);


            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteTapBeer(int id)
        {
            var tapBeerInDb = _context.TapBeers.SingleOrDefault(c => c.Id == id);

            if (tapBeerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.TapBeers.Remove(tapBeerInDb);
            _context.SaveChanges();

        }
    }
}
