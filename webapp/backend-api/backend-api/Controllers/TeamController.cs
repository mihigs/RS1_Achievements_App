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
using Newtonsoft.Json;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<TeamMember> _teamMemberRepository;
        private readonly IWorkContext _workContext;

        public TeamController(
            IRepository<Team> teamRepository,
            IWorkContext workContext,
            IRepository<TeamMember> teamMemberRepository
            )
        {
            _teamRepository = teamRepository;
            _workContext = workContext;
            _teamMemberRepository = teamMemberRepository;
        }
        /// <summary>
        /// Will return a list of all teams in the database.
        /// </summary>
        [HttpGet("list")]
        public IActionResult GetTeams()
        {
            var response = new ApiResponse();

            try
            {
                var teams = _teamRepository.GetAll();
                response.Result = teams;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getTeam", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        /// <summary>
        /// Will return all details related to a selected team.
        /// </summary>
        /// <param name="id">id of the team</param>
        /// <returns>Selected team</returns>
        [HttpGet("{id}")]
        public IActionResult GetTeamById(long id)
        {
            var response = new ApiResponse();

            try
            {
                var team = _teamRepository.Query()
                    .Where(x => x.Id == id)
                    .Include(x => x.TeamMembers)
                    .Include(x => x.AvailableAchievements)
                    .FirstOrDefault();
                response.Result = team;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getTeam", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("create-team")]
        public IActionResult CreateTeam(CreateTeamDto model)
        {
            var response = new ApiResponse();
            var existingTeam = _teamRepository.Query().Where(e => e.Name == model.Name).FirstOrDefault();
            if(existingTeam != null)
            {
                response.Errors.Add(new ApiError("getTeam", "already exists"));
            }

            var newTeam = new Team()
            {
                Name = model.Name,
                CreatedBy = _workContext.GetCurrentUserId(),
                Description = model.Description,
                TeamIconUrl = model.TeamIconUrl != null ? model.TeamIconUrl : ""
            };

            try
            {
                _teamRepository.Add(newTeam);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("getTeam", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("join-team")]
        public IActionResult JoinTeam(long teamId)
        {
            var response = new ApiResponse();
            var existingTeam = _teamRepository.Query().Where(e => e.Id == teamId).FirstOrDefault();
            if (existingTeam == null)
            {
                response.Errors.Add(new ApiError("joinTeam", "doesnt exist"));
                return NotFound(response);
            }

            var teamMemberRecord = new TeamMember()
            {
                TeamId = teamId,
                UserId = _workContext.GetCurrentUserId()
            };

            try
            {
                _teamMemberRepository.Add(teamMemberRecord);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("joinTeam", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
        [HttpPost("leave-team")]
        public IActionResult LeaveTeam(long teamId)
        {
            var response = new ApiResponse();
            var existingTeam = _teamRepository.Query().Where(e => e.Id == teamId).FirstOrDefault();
            if (existingTeam == null)
            {
                response.Errors.Add(new ApiError("leaveTeam", "team doesnt exist"));
                return NotFound(response);
            }

            var userId = _workContext.GetCurrentUserId();
            var teamMemberRecord = _teamMemberRepository.Query().Where(x => x.TeamId == teamId && x.UserId == userId).FirstOrDefault();

            if(teamMemberRecord == null)
            {
                response.Errors.Add(new ApiError("leaveTeam", "user not a part of selected team"));
                return NotFound(response);
            }

            try
            {
                _teamMemberRepository.Remove(teamMemberRecord);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("leaveTeam", ex.Message));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
            return Ok(response);
        }
    }
}
