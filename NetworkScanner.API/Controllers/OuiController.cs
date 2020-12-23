using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetworkScanner.Domain.Entities;
using NetworkScanner.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkScanner.API.Controllers
{
    /// <summary>
    /// Load full list of OUI information from local database.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OuiController : ControllerBase
    {
        private readonly ILogger<OuiController> _logger;
        private readonly IRepository repo;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public OuiController(ILogger<OuiController> logger, IRepository repository)
        {
            _logger = logger;
            repo = repository;
            repo.SetTable("Oui");
        }

        /// <summary>
        /// Returns a list of OUI on current network.
        /// </summary>
        /// <response code="200">Returns items</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IEnumerable<OuiLookup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await repo.ListAsync<OuiLookup>().ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                var details = new ProblemDetails
                {
                    Detail = ex.Message,
                    Type = ex.GetType().Name,
                    Title = " Error loading Devices"
                };
                return NotFound(details);
            }
        }

        /// <summary>
        /// Searches for OUI values based on provided MAC address.
        /// </summary>
        /// <param name="search" example="C0-64-C6"> last 4 parts of mac address.</param>
        /// <response code="200">Returns NIC manufacture(s) based on provided search hex value </response>
        /// <response code="400">If the item throws an error return details </response>
        [HttpGet]
        [Route("/hex/Find({search})")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IEnumerable<OuiLookup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetHexAsync(string search)
        {
            try
            {
                var all = await repo.ListAsync<OuiLookup>().ConfigureAwait(false);
                var results = all.Where(y => y.HexValue.Equals(search));
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                var details = new ProblemDetails
                {
                    Detail = ex.Message,
                    Type = ex.GetType().Name,
                    Title = " Error loading Devices"
                };
                return NotFound(details);
            }
        }

        /// <summary>
        /// Searches for OUI values based on provided MAC address.
        /// </summary>
        /// <param name="search" example="C064C6"> last 4 parts of mac address.
        /// </param>
        /// <returns> IEnumerable of results for Base16 IP Addresses. </returns>
        /// <response code="200">Returns NIC manufacture(s) based on provided search Base16 value </response>
        /// <response code="400">If the item throws an error return details </response>
        [HttpGet]
        [Route("/Base16/Find({search})")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IEnumerable<OuiLookup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetBase16Async(string search)
        {
            try
            {
                var all = await repo.ListAsync<OuiLookup>().ConfigureAwait(false);
                var results = all.Where(y => y.Base16Value.Equals(search));
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                var details = new ProblemDetails
                {
                    Detail = ex.Message,
                    Type = ex.GetType().Name,
                    Title = " Error loading Devices"
                };
                return NotFound(details);
            }
        }
    }
}
