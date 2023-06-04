using FluentValidation;
using Grade_Book_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Models.Validators
{
    public class RegisterStudentDtoValidator : AbstractValidator<RegisterStudentDto>
    {
        public RegisterStudentDtoValidator(GradeBookDbContext dbContext)
        {
            RuleFor(x => x.ContactEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.ContactEmail)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Students.Any(s => s.ContactEmail == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That emial is taken");
                    }
                });
        }
    }
}
