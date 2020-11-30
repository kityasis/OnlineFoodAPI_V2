using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace OnlineFood.Infrastructure.Repositories
{
    public class SettingRepository : ISettingRepository, IDisposable
    {
        private readonly OnlineFoodContext _context;
        public SettingRepository(OnlineFoodContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public UserSetting GetSetting()
        {
            return _context.UserSettings.AsNoTracking().FirstOrDefault();
        }
        public void Insert(UserSetting entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.UserSettings.Add(entity);
            _context.SaveChanges();
        }
        public void Update(UserSetting entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.UserSettings.Update(entity);
            _context.SaveChanges();
        }
    }
}
