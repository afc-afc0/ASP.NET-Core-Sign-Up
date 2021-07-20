using FluentValidation;
using Server3.Models;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Server3.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            //Rules for userName
            RuleFor(userModel => userModel.userId).NotEmpty().WithMessage("Username cant be empty");

            //Rules for email
            RuleFor(userModel => userModel.email).NotEmpty().WithMessage("Email section cant be empty");
            RuleFor(userModel => userModel.email).EmailAddress().WithMessage("Provide valid email adress");
            RuleFor(userModel => userModel.email).Equal(userModel => userModel.confirmatationEmail).WithMessage("Email adresses arent same");

            //Rules for password
            RuleFor(userModel => userModel.password).NotEmpty().WithMessage("Password cant be empty.");
            RuleFor(userModel => userModel.password).Equal(userModel => userModel.confirmationPassword);
            //Regex for password : lowercase, uppercase letters, numbers, special characters
            //and at least one lowercase letter, one uppercase letter, one number and one special character
            RuleFor(userModel => userModel.password).Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%.;^&*-]).{8,30}$")
                .WithMessage("Password must contain lowercase, uppercase letters, numbers, special characters and at least one lowercase letter, one uppercase letter, one number and one special character");
        }
    }
}