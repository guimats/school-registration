using FluentValidation;
using SchoolRegistration.Domain.DTOs.Requests;

namespace SchoolRegistration.Service.Validators.Requests
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(r => r.StudentId)
                .NotEmpty().WithMessage("Aluno deve ser preenchido");

            RuleFor(r => r.SchoolClassId)
                .NotEmpty().WithMessage("Turma deve ser preenchida");
        }
    }
}
