using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Persistence.Repositories;

namespace EmptyRoomAlert.Foundation.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        private ISettingsRepository _settings;

        [Inject]
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }



        public ISettingsRepository Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new SettingsRepository(_context);
                }
                return _settings;
            }
        }
        


        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
