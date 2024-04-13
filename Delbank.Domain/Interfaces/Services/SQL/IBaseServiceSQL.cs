using Delbank.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Interfaces.Services.SQL
{
    public interface IBaseServiceSQL<TEntity> where TEntity : BaseEntity
    {
        Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
         where TValidator : AbstractValidator<TEntity>
         where TInputModel : class
         where TOutputModel : class;

        Task Delete(Guid id);
        Task<IList<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class;

        Task<TOutputModel> GetById<TOutputModel>(Guid id) where TOutputModel : class;
        Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel, Guid id)
           where TValidator : AbstractValidator<TEntity>
           where TInputModel : class
           where TOutputModel : class;

        Task Desactive(Guid id);
    }
}
