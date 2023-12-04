using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using N5Company.Api.Controllers;
using N5Company.Api.Helpers;
using N5Company.DataAccess.UnitOfWork;
using N5Company.Domain.Interfaces;
using N5Company.Domain.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace N5Company.Test
{
	public class PermissionsControllerTests
	{
		[Fact]
		public void Constructor_WithValidDependencies_Success()
		{
			var loggerMock = new Mock<ILogger<PermissionsController>>();
			var unitOfWorkMock = new Mock<IUnitOfWork>();
			var elasticClientMock = new Mock<IElasticClient>();
			var producerServiceMock = new Mock<KafkaProducerService>();

			var controller = new PermissionsController(
				loggerMock.Object,
				unitOfWorkMock.Object,
				elasticClientMock.Object,
				producerServiceMock.Object
			);

			// Assert
			Assert.NotNull(controller);
		}

		[Fact]
		public void GetPermissions_ReturnsOkResult()
		{
			// Arrange
			var loggerMock = new Mock<ILogger<PermissionsController>>();
			var unitOfWorkMock = new Mock<IUnitOfWork>();
			var elasticClientMock = new Mock<IElasticClient>();
			var producerServiceMock = new Mock<KafkaProducerService>();

			var controller = new PermissionsController(
				loggerMock.Object,
				unitOfWorkMock.Object,
				elasticClientMock.Object,
				producerServiceMock.Object
			);

			// Act
			var result = controller.GetAll();

			// Assert
			var okResult = Assert.IsType<OkResult>(result);
		}

		[Fact]
		public async void Create_ReturnsCreatedAtActionResult()
		{
			// Arrange
			var loggerMock = new Mock<ILogger<PermissionsController>>();
			var unitOfWorkMock = new Mock<IUnitOfWork>();
			var elasticClientMock = new Mock<IElasticClient>();
			var producerServiceMock = new Mock<KafkaProducerService>();

			var permissionToCreate = new Permissions
			{
				EmployeeForename = "John",
				EmployeeSurname = "Doe",
				PermissionType = 1,
				PermissionDate = DateTime.Now
			};

			var controller = new PermissionsController(
				loggerMock.Object,
				unitOfWorkMock.Object,
				elasticClientMock.Object,
				producerServiceMock.Object
			);

			// Act
			var result = await controller.Create(permissionToCreate);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var createdPermission = Assert.IsType<Permissions>(createdAtActionResult.Value);
			Assert.NotNull(createdPermission.Id);
		}

		[Fact]
		public async void Modify_ReturnsCreatedAtActionResult()
		{
			// Arrange
			var loggerMock = new Mock<ILogger<PermissionsController>>();
			var unitOfWorkMock = new Mock<IUnitOfWork>();
			var elasticClientMock = new Mock<IElasticClient>();
			var producerServiceMock = new Mock<KafkaProducerService>();

			var permissionToCreate = new Permissions
			{
				EmployeeForename = "Jane",
				EmployeeSurname = "Doe",
				PermissionType = 1,
			};

			var controller = new PermissionsController(
				loggerMock.Object,
				unitOfWorkMock.Object,
				elasticClientMock.Object,
				producerServiceMock.Object
			);

			var result = await controller.Update(permissionToCreate);

			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var createdPermission = Assert.IsType<Permissions>(createdAtActionResult.Value);
			Assert.NotNull(createdPermission.Id);
		}
	}
}