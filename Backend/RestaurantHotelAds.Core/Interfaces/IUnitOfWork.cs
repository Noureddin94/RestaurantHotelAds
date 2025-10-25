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

        

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
