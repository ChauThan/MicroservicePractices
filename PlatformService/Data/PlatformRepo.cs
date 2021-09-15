using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAll()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetById(int id)
        {
            return _context.Platforms.SingleOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}