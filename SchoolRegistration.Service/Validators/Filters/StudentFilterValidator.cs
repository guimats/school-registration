using FluentValidation;
using SchoolRegistration.Domain.DTOs.Requests;

namespace SchoolRegistration.Service.Validators.Filters
{
    public class StudentFilterValidator : AbstractValidator<StudentFilterRequest>
    {
        public StudentFilterValidator()
        {
            RuleFor(f => f.Name)
                .MaximumLength(120).WithMessage("Nome deve ter no máximo 120 caracteres");
        }
    }
}
