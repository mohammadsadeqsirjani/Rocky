using FluentValidation;
using Rocky.Dto.Common;

namespace Rocky.Validators.Common
{
    public class EntityAddDtoValidator<TEntityAddDto> : AbstractValidator<TEntityAddDto>
        where TEntityAddDto : EntityAddDto
    {
        public EntityAddDtoValidator()
        {

        }
    }
}
