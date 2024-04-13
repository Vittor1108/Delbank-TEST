using AutoMapper;
using Delbank.Domain.Entities.NoSQL;
using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Interfaces.Repositories.NoSQL;
using Delbank.Domain.Interfaces.Repositories.SQL;
using Delbank.Domain.Interfaces.Services.SQL;
using Delbank.Messaging.Interfaces;
using Delbank.Service.Validators;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Delbank.Service.Services.SQL
{
    public class DvdServiceSQL : IDvdServiceSQL
    {
        private readonly IMapper _mapper;
        private readonly IDvdRepositorySQL _dvdRepository;
        private readonly IDvdNoSQLRepository _dvdNoSqlRepository;
        private readonly IBaseRepositorySQL<DirectorEntitySQL> _directorRepository;
        private readonly IBaseRepositorySQL<DvdEntitySQL> _baseRepository;
        private readonly IPublisher _publisher;
        private readonly IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions _memoryCacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200),
            Size = 10
        };

        public DvdServiceSQL(IMapper mapper, IDvdRepositorySQL dvdRepository, IBaseRepositorySQL<DvdEntitySQL> baseRepository,
                            IPublisher publisher, IBaseRepositorySQL<DirectorEntitySQL> directorRepository, IMemoryCache memoryCache, IDvdNoSQLRepository dvdNoSQLRepository)
        {
            _mapper = mapper;
            _dvdRepository = dvdRepository;
            _baseRepository = baseRepository;
            _publisher = publisher;
            _directorRepository = directorRepository;
            _memoryCache = memoryCache;
            _dvdNoSqlRepository = dvdNoSQLRepository;
        }

        public async Task<TOutputModel> FindOneDvd<TOutputModel>(Guid id)
        {
            DvdEntitySQL dvdEntity = new();


            if (_memoryCache.TryGetValue("DVDs", out List<DvdEntitySQL> dvds))
            {
                dvdEntity = dvds.FirstOrDefault(x => x.Id == id);

                if (dvdEntity is null)
                {
                    DvdNoSqlEntity dvdNoSql = await _dvdNoSqlRepository.FindOneDvd(id);
                    dvdEntity = _mapper.Map<DvdEntitySQL>(dvdNoSql);
                    SetDvdCache(dvdEntity);
                }

            }
            else
            {
                DvdNoSqlEntity dvdNoSql = await _dvdNoSqlRepository.FindOneDvd(id);
                dvdEntity = _mapper.Map<DvdEntitySQL>(dvdNoSql);
                SetDvdCache(dvdEntity);
            }

            TOutputModel response = _mapper.Map<TOutputModel>(dvdEntity);
            return response;

        }

        public async Task DeleteDvd(Guid id)
        {
            await _baseRepository.Desactive(id);
            string jsonBody = JsonSerializer.Serialize(id);
            _publisher.SendMessage(jsonBody, "Delete");
            DesactiveDvdCache(id);
        }

        private void DesactiveDvdCache(Guid id)
        {
            if (_memoryCache.TryGetValue("DVDs", out List<DvdEntitySQL> dvds))
            {
                DvdEntitySQL dvd = dvds.FirstOrDefault(x => x.Id == id);

                if (dvd is not null)
                {
                    dvd.DeletedAt = DateTime.Now;
                    dvd.Active = false;
                    _memoryCache.Set("DVDs", dvds, _memoryCacheOptions);
                }
            }
        }

        public async Task<TOutputModel> CreateDvd<TInputModel, TOutputModel>(TInputModel dto)
        {


            DvdEntitySQL dvdEntity = _mapper.Map<DvdEntitySQL>(dto);
            await _directorRepository.Select(dvdEntity.FkDirector);

            Validate(dvdEntity, Activator.CreateInstance<DvdValidator>());

            await _baseRepository.Insert(dvdEntity);

            DvdNoSqlEntity dvdNoSql = _mapper.Map<DvdNoSqlEntity>(dvdEntity);
            string jsonBody = JsonSerializer.Serialize(dvdNoSql);

            _publisher.SendMessage(jsonBody, "Insert");

            SetDvdCache(dvdEntity);

            TOutputModel response = _mapper.Map<TOutputModel>(dvdEntity);
            return response;
        }

        private void SetDvdCache(DvdEntitySQL dvd)
        {
           

            if (_memoryCache.TryGetValue("DVDs", out List<DvdEntitySQL> dvds))
            {
                dvds.Add(dvd);
                _memoryCache.Set("DVDs", dvds, _memoryCacheOptions);
            }
            else
            {
                List<DvdEntitySQL> listDvdEntities = new()
                {
                    dvd
                };
                _memoryCache.Set("DVDs", listDvdEntities, _memoryCacheOptions);
            }
        }

        public async Task<TOutputModel> UpdateDvd<TInputModel, TOutputModel>(TInputModel dto, Guid id)
        {
            DvdEntitySQL dvdDto = _mapper.Map<DvdEntitySQL>(dto);
            await _directorRepository.Select(dvdDto.FkDirector);

            DvdEntitySQL dvdEntity = await _baseRepository.Select(id);

            dvdEntity.UpdatedAt = DateTime.Now;
            dvdEntity.Title = dvdDto.Title;
            dvdEntity.Genre = dvdDto.Genre;
            dvdEntity.Published = dvdDto.Published;
            dvdEntity.Copies = dvdDto.Copies;
            dvdEntity.Avaliable = dvdDto.Avaliable;
            dvdEntity.FkDirector = dvdDto.FkDirector;

            Validate(dvdEntity, Activator.CreateInstance<DvdValidator>());

            await _baseRepository.Update(dvdEntity);

            DvdNoSqlEntity dvdNoSql = _mapper.Map<DvdNoSqlEntity>(dvdEntity);
            string jsonBody = JsonSerializer.Serialize(dvdNoSql);

            _publisher.SendMessage(jsonBody, "Update");

            UpdateCache(dvdEntity);

            TOutputModel response = _mapper.Map<TOutputModel>(dvdEntity);
            return response;
        }

        private void Validate(DvdEntitySQL obj, AbstractValidator<DvdEntitySQL> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }

        private void UpdateCache(DvdEntitySQL dvd)
        {
            if (_memoryCache.TryGetValue("DVDs", out List<DvdEntitySQL> dvds))
            {
                DvdEntitySQL dvdUpated = dvds.Find(x => x.Id == dvd.Id);

                if (dvdUpated is not null)
                {
                    dvdUpated.UpdatedAt = dvd.UpdatedAt;
                    dvdUpated.Title = dvd.Title;
                    dvdUpated.Genre = dvd.Genre;
                    dvdUpated.Published = dvd.Published;
                    dvdUpated.Copies = dvd.Copies;
                    dvdUpated.Avaliable = dvd.Avaliable;
                    dvdUpated.FkDirector = dvd.FkDirector;

                    _memoryCache.Set("DVDs", dvds, _memoryCacheOptions);
                }
            }
        }
    }
}
