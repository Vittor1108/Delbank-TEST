using Delbank.Domain.Entities.SQL;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Service.Validators
{
    public class DvdValidator : AbstractValidator<DvdEntitySQL>
    {
        public DvdValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                                 .NotNull()
                                 .WithMessage("Titulo do DVD não pode ser vazio. Tente Novamente!")
                                 .MaximumLength(120)
                                 .WithMessage("Tamanho máximo para o titulo do CD é de 120 caracteres. Tente novamente!")
                                 .WithErrorCode("422");

            RuleFor(x => x.Genre).NotNull()
                                 .NotEmpty()
                                 .WithMessage("Informe o genero do DVD e tente novamente!")
                                 .WithErrorCode("422");

            RuleFor(x => x.Published).NotEmpty()
                                     .NotNull()
                                     .WithErrorCode("422")
                                     .WithMessage("Informe a data de publicação do DVD e tente novamente!");            
        }
    }
}
