using System;

namespace UserManagement.Web.Models.Log;

public class LogListViewModel
{
    public List<LogItemModel> logItems { get; set; } = new();
}

public class LogItemModel
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string? ActionType { get; set; }
    public long UserId { get; set; }
    public string? Details { get; set; }
}
