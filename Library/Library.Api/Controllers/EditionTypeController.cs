using Library.Application.Contracts.EditionType;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EditionTypeController(
    IEditionTypeReadService service,
    ILogger<EditionTypeController> logger
) : ReadControllerBase<EditionTypeDto, Guid>(service, logger)
{
    
}
