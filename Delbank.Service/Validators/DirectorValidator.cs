using Delbank.Domain.Entities.SQL;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Service.Validators
{
    public  class DirectorValidator : AbstractValidator<DirectorEntitySQL>
    {
        public DirectorValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .NotEmpty()
                                .WithMessage("Nome do diretor não pode ser vazio. Tente novamente!")
                                .MaximumLength(80)
                                .WithMessage("Limite máximo de 80 caracteres atingido. Tente novamente!")
                                .WithErrorCode("422");

            RuleFor(x => x.Surname).NotEmpty()
                                .NotEmpty()
                                .WithMessage("Sobrenome do diretor não pode ser vazio. Tente novamente!")
                                .MaximumLength(80)
                                .WithMessage("Limite máximo de 80 caracteres atingido. Tente novamente!")
                                .WithErrorCode("422");
        }
    }
}
