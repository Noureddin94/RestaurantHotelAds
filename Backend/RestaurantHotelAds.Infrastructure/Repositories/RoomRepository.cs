using Microsoft.EntityFrameworkCore;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        //private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<IEnumerable<Room>> GetByHotelIdAsync(Guid hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .OrderBy(r => r.RoomNumber)
                .ToListAsync();
        }

        public async Task<Room?> GetByIdWithHotelAsync(Guid id)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        //public async Task<Room> AddAsync(Room room)
        //{
        //    room.Id = Guid.NewGuid();
        //    _context.Rooms.Add(room);
        //    await _context.SaveChangesAsync();
        //    return room;
        //}

        //public async Task<Room> UpdateAsync(Room room)
        //{
        //    _context.Rooms.Update(room);
        //    await _context.SaveChangesAsync();
        //    return room;
        //}

        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var room = await _context.Rooms.FindAsync(id);
        //    if (room == null) return false;

        //    room.IsDeleted = true;
        //    room.DeletedAt = DateTime.UtcNow;

        //    _context.Rooms.Update(room);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> ExistsByRoomNumberAsync(Guid hotelId, string roomNumber)
        {
            return await _context.Rooms
                .AnyAsync(r => r.HotelId == hotelId && r.RoomNumber == roomNumber);
        }
    }
}
