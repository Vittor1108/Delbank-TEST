using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Interfaces.Services.SQL
{
    public interface IDvdServiceSQL
    {
        Task<TOutputModel> CreateDvd<TInputModel, TOutputModel>(TInputModel dto);
        Task<TOutputModel> FindOneDvd<TOutputModel>(Guid id);
        Task DeleteDvd(Guid id);
        Task<TOutputModel> UpdateDvd<TInputModel, TOutputModel>(TInputModel dto, Guid id);
    }
}
