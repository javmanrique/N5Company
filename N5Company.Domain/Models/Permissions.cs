using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5Company.Domain.Models;

public partial class Permissions
{
	public int Id { get; set; }

	[Required]
	public string EmployeeForename { get; set; } = null!;
	[Required]
	public string EmployeeSurname { get; set; } = null!;
	[Required]
	public int PermissionType { get; set; }
	[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
	public DateTime PermissionDate { get; set; } = DateTime.Now;
	[NotMapped]
	public string acofar {  get; set; }
	public virtual PermissionTypes PermissionTypeNavigation { get; set; } = null!;
}