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
    public class BarGamesController : ApiController
    {
        private ApplicationDbContext _context;
        public BarGamesController()
        {
            _context = new ApplicationDbContext();

        }
        public IEnumerable<BarGameDto> GetBarGames()
        {
            return _context.BarGames.ToList().Select(Mapper.Map<BarGame, BarGameDto>);

        }
        public IHttpActionResult GetBarGame(int id)
        {
            var barGame = _context.BarGames.SingleOrDefault(c => c.Id == id);

            if (barGame == null)
                return NotFound();

            return Ok(Mapper.Map<BarGame, BarGameDto>(barGame));
        }
        [HttpPost]
        public IHttpActionResult CreateBarGame(BarGameDto barGameDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var barGame = Mapper.Map<BarGameDto, BarGame>(barGameDto);
            _context.BarGames.Add(barGame);
            _context.SaveChanges();

            barGameDto.Id = barGame.Id;

            return Created(new Uri(Request.RequestUri + "/" + barGame.Id), barGameDto);
        }

        [HttpPut]
        public void UpdateBarGame(int id, BarGameDto barGameDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var barGameInDb = _context.BarGames.SingleOrDefault(c => c.Id == id);

            if (barGameDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map<BarGameDto, BarGame>(barGameDto, barGameInDb);


            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteBarGame(int id, int id2)
        {
            var barGameInDb = _context.BarGames.SingleOrDefault(c => c.Id == id);
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id2);

            if (barGameInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.BarGames.Remove(barGameInDb);
            _context.SaveChanges();

        }
    }
}
