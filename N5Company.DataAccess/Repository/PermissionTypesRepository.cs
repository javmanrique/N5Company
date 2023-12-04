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
    public class PermissionTypesRepository : GenericRepository<PermissionTypes>, IPermissionTypesRepository
    {
        public PermissionTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
