using System;
using System.Threading.Tasks;
using AutoMapper;
using JusTalk.DomainModel.Managers.Common;
using JusTalk.Web.Contracts.v1;
using JusTalk.Web.Contracts.v1.Responses.Profile;
using JusTalk.Web.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;
        
        private readonly IMapper _mapper;
        
        public UserController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(ApiRoutes.Profile.Show)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Show([FromRoute] string id)
        {
            var user = await _userManager.GetById(id);
            
            if (user == null)
                return NotFound();
            
            return OkWithResult(_mapper.Map<PublicProfileResponse>(user));
        }
    }
}