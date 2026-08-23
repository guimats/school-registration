using FluentValidation;
using SchoolRegistration.Domain.DTOs.Requests;

namespace SchoolRegistration.Service.Validators.Requests
{
    public class StudentRequestValidator : AbstractValidator<StudentRequest>
    {
        public StudentRequestValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Nome deve ser preenchido")
                .MaximumLength(120).WithMessage("Nome deve ter no máximo 120 caracteres");

            RuleFor(s => s.Email)
               .NotEmpty().WithMessage("Email deve ser preenchido")
               .EmailAddress().WithMessage("Email deve ser válido")
               .MaximumLength(120).WithMessage("Email deve ter no máximo 120 caracteres");

            RuleFor(s => s.Birthdate)
                .NotEmpty().WithMessage("Data de nascimento deve ser preenchida");
        }
    }
}
