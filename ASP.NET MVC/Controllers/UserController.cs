using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Server3.Models;
using Server3.Validators;
using System.Configuration;
using static DataLibrary.Logic.UserProcessor;


namespace Server3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("DB Str = " + ConfigurationManager.ConnectionStrings["GameDB"]);
            return View();
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] UserModel userModel)
        {
            UserValidator validator = new UserValidator();
            ValidationResult validationResult = validator.Validate(userModel);

            if (validationResult.IsValid == false)
            {
                PrintValidationFailures(validationResult);
                return View();
            }

            InsertUser(userModel.userId, userModel.email, userModel.password);

            return Json(User);
        }

        [HttpGet]
        public IActionResult SignInWithEmail([FromBody] UserModel userModel)
        {
            bool result = ValidatePasswordWithEmail(userModel.email, userModel.password);

            return Json(result);
        }

        [HttpGet]
        public IActionResult SignInWithUserId([FromBody] UserModel userModel)
        {
            bool result = ValidatePasswordWithUserId(userModel.userId, userModel.password);

            return Json(result);
        }

        private void PrintValidationFailures(ValidationResult validationResult)
        {
            foreach (ValidationFailure failure in validationResult.Errors)
            {
                System.Diagnostics.Debug.WriteLine("Property name = " + failure.PropertyName + " , Error message = " + failure.ErrorMessage);
            }
        }

        private void PrintUserModel(UserModel userModel)
        {
            System.Diagnostics.Debug.WriteLine("name = " + userModel.userId + " , passwordHash = " + userModel.password + " , confirmation password hash = " + userModel.confirmationPassword
    + " , email = " + userModel.email + " , confirmation email = " + userModel.confirmatationEmail);
        }

    }

}