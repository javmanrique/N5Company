using N5Company.Domain.Interfaces;
using N5Company.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace N5Company.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PermissionTypesController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<PermissionsController> _logger;

		public PermissionTypesController(ILogger<PermissionsController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				_logger.LogInformation("GetAll is Ok");
				var permissiontypes = _unitOfWork.PermissionTypes.GetAll();
				return Ok(permissiontypes);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: GetAll " + ex.Message);
				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				_logger.LogInformation("GetById is Ok");
				var permissiontypes = _unitOfWork.PermissionTypes.GetById(id);
				return Ok(permissiontypes);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: GetById " + ex.Message);
				return BadRequest();
			}
		}

		[HttpPost]
		public IActionResult Create(PermissionTypes model)
		{
			try
			{
				_logger.LogInformation("Create is Ok");
				if (!ModelState.IsValid)
				{
					return BadRequest(model);
				}
				var permissiontypes = _unitOfWork.PermissionTypes.Create(model);
				return Ok(permissiontypes);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: Create " + ex.Message);
				return BadRequest();
			}
		}

		[HttpPut]
		[Route("update")]
		public IActionResult Update(PermissionTypes model)
		{
			try
			{
				_unitOfWork.PermissionTypes.Update(model);
				return Ok(new { message = "registro actualizado" });
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_unitOfWork.PermissionTypes.Remove(id);
				return Ok(new { message = "registro eliminado" });
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
