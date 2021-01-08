using FluentValidation;

namespace Rocky.Application.Validators.Common
{
    public class EntityValidator<TEntityAddDto> : AbstractValidator<TEntityAddDto>
        where TEntityAddDto : class
    {
        public EntityValidator()
        {

        }
    }
}
