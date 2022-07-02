using AsparagusEaterApp.DataAccess.Models;
using AsparagusEaterApp.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AsparagusEaterApp.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(string userName, string userEmail)
    {
        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userName))
        {
            return View();
        }

        var findUserByEmail = await _userRepository.GetFirstOrDefaultAsync(x => x.UserEmail == userEmail);

        if (findUserByEmail == null)
        {
            var user = new User
            {
                UserName = userName,
                UserEmail = userEmail,
                UserEatLastDate = DateTime.Now,
                UserTimesEat = 1
            };
            await _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();
        }
        else
        {
            findUserByEmail.UserEatLastDate = DateTime.Now;
            findUserByEmail.UserTimesEat++;
            _userRepository.UpdateUser(findUserByEmail);
            await _userRepository.SaveChangesAsync();
        }
        
        return RedirectToAction("Index", controllerName: "Asparagus");
    }
}