using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        /// <summary>
        /// Get all payments
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get payment by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process payment
        /// </summary>
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Refund payment
        /// </summary>
        [HttpPost("{id}/refund")]
        public async Task<IActionResult> RefundPayment(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get payment status
        /// </summary>
        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetPaymentStatus(int id)
        {
            throw new NotImplementedException();
        }
    }
}
