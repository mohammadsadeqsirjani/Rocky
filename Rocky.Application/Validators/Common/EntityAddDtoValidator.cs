using FluentValidation;
using Rocky.Application.ViewModels.Dtos.Common;

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
