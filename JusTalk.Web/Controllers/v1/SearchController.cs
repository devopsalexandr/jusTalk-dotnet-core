using System;
using System.Threading.Tasks;
using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel;
using JusTalk.Web.Contracts.v1;
using JusTalk.Web.Contracts.v1.Requests.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace JusTalk.Web.Controllers.v1
{
    [Authorize]
    public class SearchController : ApiController
    {
        private readonly IMemberSearchService _memberSearchService;
        
        private readonly IMapper _mapper;
        
        public SearchController(IMemberSearchService memberSearchService, IMapper mapper)
        {
            _memberSearchService = memberSearchService ?? throw new ArgumentNullException(nameof(memberSearchService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost(ApiRoutes.Search.Index)]
        public async Task<IActionResult> Index([FromBody] SearchRequest request, [FromQuery] int page = 1, int count = 10)
        {
            var memberSearchFilters = _mapper.Map<MemberSearchFilters>(request); 
            var members = await _memberSearchService.Find(memberSearchFilters, page, count);
            
            return OkWithResult(members);
        }
    }
}