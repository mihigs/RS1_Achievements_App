using backend_api.Data;
using backend_api.DTOs;
using backend_api.Extensions;
using backend_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<AchievementsUser> _achievementsUserRepository;

        public AchievementController(
            IRepository<Achievement> achievementRepository,
            IWorkContext workContext,
            IRepository<AchievementsUser> achievementsUserRepository
            )
        {
            _achievementRepository = achievementRepository;
            _workContext = workContext;
            _achievementsUserRepository = achievementsUserRepository;
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
        public IActionResult GetAchievementById(long id)
        {
            var response = new ApiResponse();

            try
            {
                //var achievement = _achievementRepository.GetById(id);
                var achievement = _achievementRepository.Query()
                    .Where(x => x.Id == id)
                    .Include(x => x.Event)
                    .Include(x => x.Team)
                    .FirstOrDefault();
                achievement.AchievedBy = _achievementsUserRepository.Query().Where(x => x.AchievementId == id).Select(x => x.User).ToList();
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
                EventId = !model.EventId.HasValue ? null : model.EventId.Value,
                TeamId = !model.TeamId.HasValue ? null : model.TeamId.Value,
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
        [HttpDelete("{id}")]
        public IActionResult RemoveAchievementById(long id)
        {
            var response = new ApiResponse();

            var existingAch = _achievementRepository.GetById(id);

            if (existingAch == null)
            {
                response.Errors.Add(new ApiError("removeAchievement", "not found"));
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }

            try
            {
                _achievementRepository.Remove(existingAch);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("removeAchievement", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("assign-achievement")]
        public IActionResult AssignAchievement(AssignAchievementDto model)
        {
            var response = new ApiResponse();
            var existingAchievement = _achievementsUserRepository.Query().Where(e => e.UserId == model.UserId && e.AchievementId == model.AchievementId).FirstOrDefault();
            if (existingAchievement != null)
            {
                response.Errors.Add(new ApiError("assignAchievement", "already exists"));
            }

            var newAchievement = new AchievementsUser()
            {
                UserId = model.UserId,
                AchievementId = model.AchievementId
            };

            try
            {
                _achievementsUserRepository.Add(newAchievement);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("assignAchievement", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("filter")]
        public IActionResult FilterAchievements(FilterAchievementsDto model)
        {
            var response = new ApiResponse();

            try
            {
                var achievementsQuery = _achievementRepository.Query().Include(x => x.AchievedBy).AsQueryable();

                if (model.TeamId.HasValue)
                {
                    achievementsQuery = achievementsQuery.Include(x => x.Team).Where(x => x.TeamId == model.TeamId.Value);
                }
                if (model.EventId.HasValue)
                {
                    achievementsQuery = achievementsQuery.Include(x => x.Event).Where(x => x.EventId == model.EventId.Value);
                }

                if (model.UserId != String.Empty)
                {
                    var achievementsUserQuery = _achievementsUserRepository.Query().Where(x => x.UserId == model.UserId).Select(x => x.AchievementId).ToList();
                    achievementsQuery = achievementsQuery.Where(x => achievementsUserQuery.Contains(x.Id));
                }

                var result = achievementsQuery.ToList();

                response.Result = result;
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
