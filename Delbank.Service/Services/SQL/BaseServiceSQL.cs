using AutoMapper;
using Delbank.Domain.Entities;
using Delbank.Domain.Interfaces.Repositories.SQL;
using Delbank.Domain.Interfaces.Services.SQL;
using FluentValidation;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Service.Services.SQL
{
    public class BaseServiceSQL<TEntity> : IBaseServiceSQL<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepositorySQL<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseServiceSQL(IBaseRepositorySQL<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Guid guid = Guid.NewGuid();
            entity.Id = guid;

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task Delete(Guid id)
        {
            await _baseRepository.Delete(id);
        }

        public async Task Desactive(Guid id)
        {
            await _baseRepository.Desactive(id);
        }

        public async Task<IList<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class
        {
            var entities = await _baseRepository.Select();

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s)).ToList();

            return outputModels;
        }

        public async Task<TOutputModel> GetById<TOutputModel>(Guid id) where TOutputModel : class
        {
            var entity = await _baseRepository.Select(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel, Guid id)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            entity.Id = id;

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
