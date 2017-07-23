using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDPerthBot.Model;
using DDDPerthBot.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace DDDPerthBot.Services.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/speakers")]
    public class SpeakerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpeakerController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        [HttpGet]
        [Route("q")]
        [ProducesResponseType(typeof(IList<Speaker>), 200)]
        public async Task<IActionResult> Search([FromQuery]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            var cleaned = _unitOfWork.Speakers.All().Where(x => Clean(x.Name).Contains(Clean(name))).ToList();
            return Ok(cleaned);
        }

        private static string Clean(string val)
        {
            return new string(val.ToCharArray().Where(x => char.IsLetter(x)).ToArray()).ToLower();
        }
    }
}