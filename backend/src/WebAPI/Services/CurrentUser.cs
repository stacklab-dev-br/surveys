using StackLab.Survey.Application.Common.Interfaces;
using System.Security.Claims;

namespace StackLab.Survey.WebAPI.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? Id => int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("id"), out int id) ? id : null;
    public string? Login => _httpContextAccessor.HttpContext?.User?.FindFirstValue("login");
    public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");

}
