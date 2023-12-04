using N5Company.DataAccess.Repository;
using N5Company.Domain.Interfaces;
using N5Company.Domain.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			this._context = context;
			Permissions = new PermissionsRepository(this._context);
			PermissionTypes = new PermissionTypesRepository(this._context);
		}

		public IPermissionsRepository Permissions { get; private set; }
		public IPermissionTypesRepository PermissionTypes { get; private set; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}
	}
}
