using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace N5Company.Domain.Models;

public partial class PermissionTypes
{
	public int Id { get; set; }
	[Required]
	public string Description { get; set; } = null!;

	public virtual ICollection<Permissions> Permissions { get; } = new List<Permissions>();
}
