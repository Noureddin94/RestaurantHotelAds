using Microsoft.EntityFrameworkCore.Storage;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace RestaurantHotelAds.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Hotels = new HotelRepository(_context);
            Rooms = new RoomRepository(_context);
            AdRequests = new AdRequestRepository(_context);
            RoomAdvertisements = new RoomAdvertisementRepository(_context);
            Users = new UserRepository(_context);
            Restaurants = new RestaurantRepository(_context);
            Advertisements = new AdvertisementRepository(_context);
        }

        public IHotelRepository Hotels { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IAdRequestRepository AdRequests { get; private set; }
        public IRoomAdvertisementRepository RoomAdvertisements { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRestaurantRepository Restaurants { get; private set; }
        public IAdvertisementRepository Advertisements { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
