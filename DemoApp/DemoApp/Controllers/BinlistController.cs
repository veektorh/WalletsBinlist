using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinlistController : ControllerBase
    {
        private readonly IBinlistManager binlistManager;

        public BinlistController(IBinlistManager binlistManager)
        {
            this.binlistManager = binlistManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Lookup(string id)
        {
            var result = await binlistManager.GetBinDetails(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("count")]
        public async Task<ActionResult> HitCount(string id)
        {
            var result =  await binlistManager.GetCount();

            return Ok(result);
        }
    }
}