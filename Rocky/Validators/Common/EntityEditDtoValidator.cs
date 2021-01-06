using FluentValidation;
using Rocky.Dto.Common;

namespace Rocky.Validators.Common
{
    public class EntityEditDtoValidator<TEntityEditDto> : AbstractValidator<TEntityEditDto>
        where TEntityEditDto : EntityEditDto
    {
        public EntityEditDtoValidator()
        {

        }
    }
}
