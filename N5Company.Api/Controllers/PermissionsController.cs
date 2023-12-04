using N5Company.Domain.Interfaces;
using N5Company.Domain;
using N5Company.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Security;
using N5Company.Api.Helpers;

namespace N5Company.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PermissionsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<PermissionsController> _logger;
		private readonly IElasticClient _elasticClient;
		private readonly KafkaProducerService _producerService;


		public PermissionsController(ILogger<PermissionsController> logger, IUnitOfWork unitOfWork, 
									IElasticClient elasticClient, KafkaProducerService producerService)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_elasticClient = elasticClient;
			_producerService = producerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				_logger.LogInformation("GetAll is Ok");
				var searchResponse = _elasticClient.Search<Permissions>(s => s
					.Query(q => q.MatchAll())
				);
				var permissions = searchResponse.Documents;

				var id = Guid.NewGuid().ToString();
				var operation = "GetAll";

				var kafkaDto = new { Id = id, Operation = operation };
				var kafkaMessage = Newtonsoft.Json.JsonConvert.SerializeObject(kafkaDto);

				await _producerService.ProduceAsync("Permissions", kafkaMessage);

				return Ok(permissions); ;
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
				var permissions = _unitOfWork.Permissions.GetById(id);
				return Ok(permissions);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: GetById " + ex.Message);
				return BadRequest();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(Permissions model)
		{
			try
			{
				_logger.LogInformation("Create is Ok");
				if (!ModelState.IsValid)
				{
					return BadRequest(model);
				}

				var permissions = _unitOfWork.Permissions.Create(model);
				var indexResponse = _elasticClient.IndexDocument(model);
				//-----------------------------
				var id = Guid.NewGuid().ToString();
				var operation = "Create";

				// Crear un DTO y convertirlo a JSON
				var kafkaDto = new { Id = id, Operation = operation };
				var kafkaMessage = Newtonsoft.Json.JsonConvert.SerializeObject(kafkaDto);

				// Enviar mensaje a Kafka
				await _producerService.ProduceAsync("Permissions", kafkaMessage);

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: Create " + ex.Message);
				return BadRequest(new { error = "Error al agregar el permiso" });
			}
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> Update(Permissions model)
		{
			try
			{
				_logger.LogInformation("Update is Ok");
				_unitOfWork.Permissions.Update(model);
				var indexResponse = _elasticClient.IndexDocument(model);
				//-----------------------------
				var id = Guid.NewGuid().ToString();
				var operation = "Modify";

				// Crear un DTO y convertirlo a JSON
				var kafkaDto = new { Id = id, Operation = operation };
				var kafkaMessage = Newtonsoft.Json.JsonConvert.SerializeObject(kafkaDto);

				await _producerService.ProduceAsync("Permissions", kafkaMessage);

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: Update " + ex.Message);
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_logger.LogInformation("Delete is Ok");
				_unitOfWork.Permissions.Remove(id);
				return Ok(new { message = "registro eliminado" });
			}
			catch (Exception ex)
			{
				_logger.LogError("Error: Delete " + ex.Message);
				return BadRequest();
			}
		}

	}
}
