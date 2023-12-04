using  N5Company.Domain.Interfaces;
using  N5Company.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace  N5Company.DataAccess.Repository
{
    public class PermissionsRepository : GenericRepository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
