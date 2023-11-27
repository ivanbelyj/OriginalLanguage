using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Sentences.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;

namespace OriginalLanguage.Api.Controllers.Sentences;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sentences")]
public class SentencesController : ControllerBase
{
    private readonly ISentencesService sentencesService;
    private readonly IMapper mapper;

    public SentencesController(ISentencesService sentencesService, IMapper mapper)
    {
        this.sentencesService = sentencesService;
        this.mapper = mapper;
    }

    [ProducesResponseType(typeof(IEnumerable<SentenceResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<SentenceResponse>> GetSentences(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        return mapper.Map<IEnumerable<SentenceResponse>>(
            await sentencesService.GetSentences(offset, limit));
    }

    [ProducesResponseType(typeof(SentenceResponse), 200)]
    [HttpGet("{id}")]
    public async Task<SentenceResponse> GetSentence(
        [FromRoute] int id)
    {
        return mapper.Map<SentenceResponse>(await sentencesService.GetSentence(id));
    }

    [ProducesResponseType(typeof(SentenceResponse), 200)]
    [HttpPost("")]
    public async Task<SentenceResponse> AddSentence(
        [FromBody] AddSentenceRequest request)
    {
        return mapper.Map<SentenceResponse>(
            await sentencesService
                .AddSentence(mapper.Map<AddSentenceModel>(request)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSentence(
        [FromRoute] int id,
        [FromBody] UpdateSentenceRequest request)
    {
        await sentencesService.UpdateSentence(id,
            mapper.Map<UpdateSentenceModel>(request));

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSentence([FromRoute] int id)
    {
        await sentencesService.DeleteSentence(id);

        return Ok();
    }
}
