using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Domain.Models
{
	public class KafkaMessageDto
	{
		public Guid Id { get; set; }
		public string Operation { get; set; }
	}
}
