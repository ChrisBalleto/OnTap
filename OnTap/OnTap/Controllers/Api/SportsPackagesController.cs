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
    public class SportsPackagesController : ApiController
    {
        private ApplicationDbContext _context;
        public SportsPackagesController()
        {
            _context = new ApplicationDbContext();

        }
        public IEnumerable<SportsPackageDto> GetSportsPackages()
        {
            return _context.SportsPackages.ToList().Select(Mapper.Map<SportsPackage, SportsPackageDto>);

        }
        public IHttpActionResult GetSportsPackage(int id)
        {
            var sportsPackage = _context.SportsPackages.SingleOrDefault(c => c.Id == id);

            if (sportsPackage == null)
                return NotFound();

            return Ok(Mapper.Map<SportsPackage, SportsPackageDto>(sportsPackage));
        }
        [HttpPost]
        public IHttpActionResult CreateSportsPackage(SportsPackageDto sportsPackageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sportsPackage = Mapper.Map<SportsPackageDto, SportsPackage>(sportsPackageDto);
            _context.SportsPackages.Add(sportsPackage);
            _context.SaveChanges();

            sportsPackageDto.Id = sportsPackage.Id;

            return Created(new Uri(Request.RequestUri + "/" + sportsPackage.Id), sportsPackageDto);
        }

        [HttpPut]
        public void UpdateSportsPackage(int id, SportsPackageDto sportsPackageDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var sportsPackageInDb = _context.SportsPackages.SingleOrDefault(c => c.Id == id);

            if (sportsPackageDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map<SportsPackageDto, SportsPackage>(sportsPackageDto, sportsPackageInDb);


            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteSportsPackage(int id)
        {
            var sportsPackageInDb = _context.SportsPackages.SingleOrDefault(c => c.Id == id);

            if (sportsPackageInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.SportsPackages.Remove(sportsPackageInDb);
            _context.SaveChanges();

        }
    }
}
