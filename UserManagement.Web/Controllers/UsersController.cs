using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly DataContext _dataContext;
    public UsersController(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

    [HttpGet]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateofBirth = p.DateofBirth,
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
            DateofBirth = p.DateofBirth,
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
            DateofBirth = p.DateofBirth,
            IsActive = p.IsActive
        }).ToList();

        var model = new UserListViewModel
        {
            Items = nonActive_Items
        };

        return View("List", model);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("add")]
    public IActionResult Add(UserListItemViewModel userListItemViewModel)
    {
        if (ModelState.IsValid)
        {
            var newUser = new User
            {
                Forename = userListItemViewModel.Forename!,
                Surname = userListItemViewModel.Surname!,
                Email = userListItemViewModel.Email!,
                DateofBirth = userListItemViewModel.DateofBirth,
                IsActive = userListItemViewModel.IsActive
            };
            _dataContext.Add(newUser);
            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }
        return View(userListItemViewModel);
    }

    [HttpGet("View/{id}")]
    public IActionResult View(int id)
    {
        var user = _userService.GetAll().FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        var viewModel = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            DateofBirth = user.DateofBirth,
            IsActive = user.IsActive

        };
        return View(viewModel);
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var user = _userService.GetAll().FirstOrDefault(x => x.Id == id);
        if(user == null)
        {
            return NotFound(nameof(user));
        }

        var viewModel = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            DateofBirth = user.DateofBirth,
            IsActive = user.IsActive
        };
        return View(viewModel);
    }
    [HttpPost("Edit/{id}")]
    public IActionResult Edit(int id, UserListItemViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _userService.GetAll().FirstOrDefault(x => x.Id == id);
        if(user == null)
        {
            return NotFound();
        }

        //Update User Properties
        user.Forename = model.Forename!;
        user.Surname = model.Surname!;
        user.Email = model.Email!;
        user.DateofBirth = model.DateofBirth!;
        user.IsActive = model.IsActive;

        _dataContext.Update(user);
        return RedirectToAction("List");
    }
    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var user = _userService.GetAll().FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound(nameof(user));
        }

        var viewModel = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            DateofBirth = user.DateofBirth,
            IsActive = user.IsActive
        };
        return View(viewModel);
    }
    [HttpPost("Delete/{id}")]
    public IActionResult ConfirmDelete(int id)
    {

        var user = _userService.GetAll().FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound();
        }

          
        _dataContext.Delete(user);
        return RedirectToAction("List");
    }
}
