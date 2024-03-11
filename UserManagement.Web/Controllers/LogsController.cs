
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Web.Models.Log;

namespace UserManagement.WebMS.Controllers;

[Route("logs")]
public class LogsController : Controller
{
    private readonly DataContext _dataContext;

    public LogsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public ViewResult List()
    {
        var logsdata = _dataContext.Log?.ToList() ?? new List<Log>();
        var logViewModels = logsdata.Select(log => new LogItemModel
        {
            Id = log.Id,
            Timestamp = log.Timestamp,
            ActionType = log.ActionType,
            UserId = log.UserId,
            Details = log.Details,
        }).ToList();

        var viewModel = new LogListViewModel
        {
            logItems = logViewModels,
        };
        return View("List", viewModel);
    }

    [HttpGet("Details/{id}")]
    public IActionResult Details(int  id)
    {
        var log = _dataContext.Log?.FirstOrDefault(x => x.UserId == id);
        if (log == null)
        {
            return NotFound();
        }
        var logViewModel = new LogItemModel
        {
            Id = log.Id,
            Timestamp = log.Timestamp,
            ActionType = log.ActionType,
            UserId = log.UserId,
            Details = log.Details,
        };
        return View(logViewModel);
    }
}

