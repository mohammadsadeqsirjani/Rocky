using FluentValidation;
using Rocky.Application.Dtos.Common;

namespace Rocky.Application.Validators.Common
{
    public class EntityEditDtoValidator<TEntityEditDto> : AbstractValidator<TEntityEditDto>
        where TEntityEditDto : EntityEditDto
    {
        public EntityEditDtoValidator()
        {

        }
    }
}
