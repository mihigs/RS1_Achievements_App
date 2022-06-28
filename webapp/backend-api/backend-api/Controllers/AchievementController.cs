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
    public class AchievementController : ControllerBase
    {
        private readonly IRepository<Achievement> _achievementRepository;
        private readonly IWorkContext _workContext;

        public AchievementController(
            IRepository<Achievement> achievementRepository,
            IWorkContext workContext
            )
        {
            _achievementRepository = achievementRepository;
            _workContext = workContext;
        }
        /// <summary>
        /// Will return a list of all achievements in the database.
        /// </summary>
        [HttpGet("list")]
        public IActionResult GetAchievements()
        {
            var response = new ApiResponse();

            try
            {
                var achievements = _achievementRepository.GetAll();
                response.Result = achievements;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getAchievement", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        /// <summary>
        /// Will return all details related to a selected achievement.
        /// </summary>
        /// <param name="id">id of the achievement</param>
        /// <returns>Selected achievement</returns>
        [HttpGet("{id}")]
        public IActionResult GetAchievementById(string id)
        {
            var response = new ApiResponse();

            try
            {
                var achievement = _achievementRepository.GetById(id);
                response.Result = achievement;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getAchievement", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("create-achievement")]
        public IActionResult CreateAchievement(CreateAchievementDto model)
        {
            var response = new ApiResponse();
            var existingAchievement = _achievementRepository.Query().Where(e => e.Name == model.Name).FirstOrDefault();
            if(existingAchievement != null)
            {
                response.Errors.Add(new ApiError("getAchievement", "already exists"));
            }

            var newAchievement = new Achievement()
            {
                Name = model.Name,
                Description = model.Description,
                EventId = model.EventId == 0 ? null : model.EventId,
                TeamId = model.TeamId == 0 ? null : model.TeamId,
                CreatedBy = _workContext.GetCurrentUserId(),
                Tier = model.Tier,
                IconUrl = model.IconUrl != null ? model.IconUrl : ""
            };

            try
            {
                _achievementRepository.Add(newAchievement);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getAchievement", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
    }
}
