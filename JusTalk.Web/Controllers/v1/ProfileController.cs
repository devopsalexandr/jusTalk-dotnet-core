using System;
using System.Threading.Tasks;
using AutoMapper;
using JusTalk.DomainModel;
using JusTalk.Web.Contracts.v1;
using JusTalk.Web.Contracts.v1.Responses.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    [Authorize]
    public class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;
        
        private readonly IMapper _mapper;
        
        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(ApiRoutes.Profile.Index)]
        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetProfileAsync();
            return Ok(_mapper.Map<UserProfileResponse>(profile));
        }

        // [HttpPut(ApiRoutes.Profile.Update)]
        // public async Task<IActionResult> Update([FromBody] UpdateAuthUserRequest request)
        // {
        //     return Ok();
        // }
    }
}