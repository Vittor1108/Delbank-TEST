using AutoMapper;
using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Interfaces.Repositories.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Service.Services.SQL
{
    public class DirectorServiceSQL
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepositorySQL<DirectorEntitySQL> _baseRepository;

        public DirectorServiceSQL(IMapper mapper, IBaseRepositorySQL<DirectorEntitySQL> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }
    }
}
