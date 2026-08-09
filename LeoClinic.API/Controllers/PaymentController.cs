using LeoClinic.Application.DTOs.Payment;
using LeoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeoClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentDto dto)
        {
            try
            {
                var intent = await _paymentService.CreatePaymentIntentAsync(dto);
                return Ok(intent);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmPayment(int id, [FromBody] ConfirmPaymentDto dto)
        {
            try
            {
                var payment = await _paymentService.ConfirmPaymentAsync(id, dto.PaymentMethodId);
                return Ok(payment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            var payload = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"].ToString();

            try
            {
                await _paymentService.HandleWebhookAsync(payload, signatureHeader);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment is null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var payments = await _paymentService.GetByPatientAsync(patientId);
            return Ok(payments);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
        {
            var payment = await _paymentService.GetByAppointmentAsync(appointmentId);
            if (payment is null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        [HttpPost("{id}/refund")]
        public async Task<IActionResult> Refund(int id)
        {
            try
            {
                var payment = await _paymentService.RefundAsync(id);
                if (payment is null)
                    return NotFound();

                return Ok(payment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
