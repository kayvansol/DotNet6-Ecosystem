using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(PermissionAttribute))]
    public class BaseController : Controller
    {
        
    }
}
