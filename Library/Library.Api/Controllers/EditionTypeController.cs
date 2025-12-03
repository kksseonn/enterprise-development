using Library.Application.Contracts.EditionType;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EditionTypeController : ReadControllerBase<EditionTypeDto, Guid>
{
    public EditionTypeController(IEditionTypeReadService service, ILogger<EditionTypeController> logger) : base(service, logger)
    {
    }
}
