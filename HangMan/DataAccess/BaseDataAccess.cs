using HangMan.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangMan.DataAccess
{
    public class BaseDataAccess
    {
        internal HangManDbContext _context;
        public BaseDataAccess(HangManDbContext context)
        {
            _context = context;
        }
    }
}
