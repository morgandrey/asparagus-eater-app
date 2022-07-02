using AsparagusEaterApp.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AsparagusEaterApp.Controllers;

public class AsparagusController : Controller
{
    private readonly ILogger<AsparagusController> _logger;
    private readonly IUserRepository _userRepository;

    public AsparagusController(ILogger<AsparagusController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allUsers = await _userRepository.GetAll();
        return View(allUsers);
    }
}