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
    /// Found Device list information.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IRepository repo;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public DeviceController(ILogger<DeviceController> logger, IRepository repository)
        {
            _logger = logger;
            repo = repository;
            repo.SetTable("Device");
        }

        /// <summary>
        /// Returns a list of Found Device on current network.
        /// </summary>
        /// <returns>
        /// Returns all found network devices.
        /// </returns>
        /// <remarks>
        /// a list of devices from the database
        /// </remarks>
        /// <reponse code="200">The full list of devices. </reponse>
        /// <response code="404">The specified devices does not exist, or the current user does not have access to it.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IEnumerable<FoundDevice>))]
        [ProducesResponseType(StatusCodes.Status404NotFound,
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var allDevices = await repo.ListAsync<FoundDevice>().ConfigureAwait(false);
                var results = allDevices.ToList().OrderBy(x => x.Id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                var details = new ProblemDetails
                {
                    Detail = ex.Message, Type = ex.GetType().Name,
                    Title = " Error loading Devices"
                };
                return NotFound(details);
            }
        }
    }
}
