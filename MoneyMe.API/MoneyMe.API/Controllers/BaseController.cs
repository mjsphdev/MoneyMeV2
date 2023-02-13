using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.API.Application.Shared.Models;

namespace MoneyMe.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public IActionResult OkResult<TData>(TData? data, dynamic? pagination = null)
        {
            return Ok(new AppResponse<TData>(data, pagination));
        }
    }
}
