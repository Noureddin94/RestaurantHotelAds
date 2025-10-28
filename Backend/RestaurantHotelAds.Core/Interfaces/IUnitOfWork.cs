using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRepository Hotels { get; }
        IRoomRepository Rooms { get; }
        IRoomAdvertisementRepository RoomAdvertisements { get; }
        IAdRequestRepository AdRequests { get; }
        IRestaurantRepository Restaurants { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
