using Library.Application.Contracts.Borrow;
using Microsoft.AspNetCore.Mvc;
namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с выдачами книг (Borrow)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BorrowController(
    IBorrowCrudService service,
    ILogger<BorrowController> logger)
    : CrudControllerBase<BorrowDto, BorrowCrudDto, Guid>(service, logger)
{
}