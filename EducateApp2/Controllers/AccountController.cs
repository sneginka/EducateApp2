using EducateApp2.Models;
using EducateApp2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducateApp2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager; //сервис по управлению пользователями
            //сервис SignInManager, который позволяет аутентифицировать пользователя
            //и устанавливать или удалять его куки
            _signInManager = signInManager;
        }
        [HttpGet]
        //метод срабатывает при открытии страницы регистрации
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //теперь ввели значения и нажали кнопку "Зарегистрироваться"
        //методом post данные передаются через модель для представления
        //метод принимает описанную модель для представления, значения с формы регистрации
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Создание экземпляра класса User, установить его свойствам значения из модели
                User user = new User { 
                    LastName = model.LastName, 
                    FirstName = model.FirstName, 
                    Patronymic = model.Patronymic, 
                    Email = model.Email, 
                    UserName = model.Email 
                };

                // добавляем пользователя
                //С помощью метода _userManager.CreateAsync пользователь добавляется в базу данных.
                //В качестве параметра передается сам пользователь и его пароль.
                //Данный метод возвращает объект IdentityResult, с помощью которого можно узнать
                //успешность выполненной операции. Вполне возможно, что переданные значения
                //не удовлетворяют требованиям, и тогда пользователь не будет добавлен в базу данных.
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    //В случае удачного добавления с помощью метода _signInManager.SignInAsync()
                    //устанавливаем аутентификационные куки для добавленного пользователя.
                    //В этот метод передается объект пользователя, который аутентифицируется,
                    //и логическое значение, указывающее, надо ли сохранять куки в течение
                    //продолжительного времени.
                    
                    await _signInManager.SignInAsync(user, false);

                    //И далее выполняем переадресацию на главную страницу приложения.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Если добавление прошло неудачно, то добавляем к состоянию модели
                    //с помощью метода ModelState все возникшие при добавлении ошибки,
                    //и отправленная модель возвращается в представление.
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model); //возвращение модели в представление
        }
    }
}
