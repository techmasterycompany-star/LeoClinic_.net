using Clinic.DTOs;
using Clinic.Exceptions;
using Clinic.Services;
using Microsoft.AspNetCore.Mvc;


namespace Clinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IStripeService _stripeService;

        public PaymentController(
            IPaymentService paymentService,
            IStripeService stripeService)
        {
            _paymentService = paymentService;
            _stripeService = stripeService;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment(MakePaymentDto dto)
        {
            try
            {
                var payment = await _paymentService.MakePaymentAsync(dto);

                return Ok(payment);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("history/{patientId}")]
        public async Task<IActionResult> GetPaymentHistory(int patientId)
        {
            var history = await _paymentService.GetPaymentHistoryAsync(patientId);

            return Ok(history);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(MakePaymentDto dto)
        {
            var checkoutUrl = await _stripeService.CreateCheckoutSessionAsync(
                dto.Amount,
                dto.PaymentMethod,
                dto.AppointmentId,
                dto.PatientId);

            return Ok(new
            {
                CheckoutUrl = checkoutUrl
            });
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            return Ok(new
            {
                Message = "Payment completed successfully."
            });
        }

        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return Ok(new
            {
                Message = "Payment was cancelled."
            });
        }
    }
}
