using backend_api.Data;
using backend_api.DTOs;
using backend_api.Extensions;
using backend_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IRepository<Event> _organisedEventRepository;
        private readonly IWorkContext _workContext;

        public EventController(
            IRepository<Event> organisedEventRepository,
            IWorkContext workContext
            )
        {
            _organisedEventRepository = organisedEventRepository;
            _workContext = workContext;
        }
        /// <summary>
        /// Will return a list of all organisedEvents in the database.
        /// </summary>
        [HttpGet("list")]
        public IActionResult GetEvents()
        {
            var response = new ApiResponse();

            try
            {
                var organisedEvents = _organisedEventRepository.GetAll();
                response.Result = organisedEvents;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getEvent", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        /// <summary>
        /// Will return all details related to a selected organisedEvent.
        /// </summary>
        /// <param name="id">id of the organisedEvent</param>
        /// <returns>Selected organisedEvent</returns>
        [HttpGet("{id}")]
        public IActionResult GetEventById(string id)
        {
            var response = new ApiResponse();

            try
            {
                var organisedEvent = _organisedEventRepository.GetById(id);
                response.Result = organisedEvent;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getEvent", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("create-event")]
        public IActionResult CreateEvent(CreateEventDto model)
        {
            var response = new ApiResponse();
            var existingEvent = _organisedEventRepository.Query().Where(e => e.Name == model.Name).FirstOrDefault();
            if(existingEvent != null)
            {
                response.Errors.Add(new ApiError("getEvent", "already exists"));
            }

            var newEvent = new Event()
            {
                Name = model.Name,
                EventDate = model.EventDate,
                CreatedBy = _workContext.GetCurrentUserId(),
                Description = model.Description
            };

            try
            {
                _organisedEventRepository.Add(newEvent);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getEvent", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
    }
}
