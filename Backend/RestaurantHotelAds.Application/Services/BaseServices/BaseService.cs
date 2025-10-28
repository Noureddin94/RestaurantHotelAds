using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantHotelAds.Application.Services.BaseServices;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Application.Mappings;
using AutoMapper;

namespace RestaurantHotelAds.Application.Services.BaseServices
{
    public abstract class BaseService<T, TDto> : IBaseService<TDto>
        where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<T>();
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto?>(entity);
        }

        public virtual async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            var addedEntity = await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TDto>(addedEntity);
        }

        public virtual async Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception($"{typeof(T).Name} not found");

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TDto>(existing);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
