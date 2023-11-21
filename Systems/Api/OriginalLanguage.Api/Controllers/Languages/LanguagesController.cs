using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Languages.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Languages;
using OriginalLanguage.Services.Languages.Models;

namespace OriginalLanguage.Api.Controllers.Languages;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/languages")]
[ApiController]
[ApiVersion("1.0")]
public class LanguagesController : ControllerBase
{
    private readonly ILanguagesService languageService;
    private readonly IMapper mapper;

    public LanguagesController(ILanguagesService languageService, IMapper mapper)
    {
        this.languageService = languageService;
        this.mapper = mapper;
    }

    [ProducesResponseType(typeof(LanguageResponse), 200)]
    [HttpGet("{id}")]
    public async Task<LanguageResponse> GetLanguage([FromRoute] int id)
    {
        return mapper.Map<LanguageResponse>(await languageService.GetLanguage(id));
    }

    [ProducesResponseType(typeof(IEnumerable<LanguageResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<LanguageResponse>> GetLanguages(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        return (await languageService.GetLanguages(offset, limit))
            .Select(mapper.Map<LanguageResponse>);
    }

    [HttpPost("")]
    public async Task<LanguageResponse> AddLanguage(
        [FromBody] AddLanguageRequest request)
    {
        var languageModel = await languageService
            .AddLanguage(mapper.Map<AddLanguageModel>(request));
        return mapper.Map<LanguageResponse>(languageModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLanguage([FromRoute] int id,
        [FromBody] UpdateLanguageRequest request)
    {
        await languageService.UpdateLanguage(id,
            mapper.Map<UpdateLanguageModel>(request));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLanguage([FromRoute] int id)
    {
        await languageService.DeleteLanguage(id);
        return Ok();
    }
}
