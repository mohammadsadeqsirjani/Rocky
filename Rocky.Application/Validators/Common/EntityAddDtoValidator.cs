using FluentValidation;
using Rocky.Application.Dtos.Common;

namespace Rocky.Application.Validators.Common
{
    public class EntityAddDtoValidator<TEntityAddDto> : AbstractValidator<TEntityAddDto>
        where TEntityAddDto : EntityAddDto
    {
        public EntityAddDtoValidator()
        {

        }
    }
}
