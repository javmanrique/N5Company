using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IPermissionsRepository Permissions { get; }
		IPermissionTypesRepository PermissionTypes { get; }
	}
}
