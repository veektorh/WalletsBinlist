using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinlistApi.Interfaces.Managers;
using BinlistApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BinlistApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinlistController : ControllerBase
    {
        private readonly IBinlistManager binlistManager;
        private readonly IBinMessagePublisher binMessagePublisher;

        public BinlistController(IBinlistManager binlistManager, IBinMessagePublisher binMessagePublisher)
        {
            this.binlistManager = binlistManager;
            this.binMessagePublisher = binMessagePublisher;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult> Lookup(string id)
        {
            var result = await binlistManager.GetBinDetails(id);

            result.id = id;
            binMessagePublisher.PublishBin(result);

            return Ok(result);
        }
    }
}