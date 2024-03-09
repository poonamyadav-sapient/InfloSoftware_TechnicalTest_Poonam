using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("active")]
    public ViewResult List_Active()
    {
        var Active_Items = _userService.GetAll().Where(p => p.IsActive).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive
        }).ToList();

        var model = new UserListViewModel
        {
            Items = Active_Items
        };

        return View("List", model);
    }

    [HttpGet("nonactive")]
    public ViewResult List_InActive()
    {
        var nonActive_Items = _userService.GetAll().Where(p => !p.IsActive).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive
        }).ToList();

        var model = new UserListViewModel
        {
            Items = nonActive_Items
        };

        return View("List", model);
    }
}
