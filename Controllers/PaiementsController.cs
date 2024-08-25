using GymApplication.Repository;
using GymApplication.Repository.Models;
using GymApplication.UtilityService;
using Microsoft.AspNetCore.Mvc;

namespace GymApplication.Controllers
{
    [ApiController]
    [Route("api/paiements")]
    public class PaiementsController : ControllerBase
    {
        private readonly StripeService _stripeService;
        private readonly GymDbContext _context;

        public PaiementsController(StripeService stripeService, GymDbContext context)
        {
            _stripeService = stripeService;
            _context = context;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaiementCreateRequest request)
        {
            var paymentIntent = await _stripeService.CreatePaymentIntent(request.Montant);

            // Create a new Paiement record
            var paiement = new Paiement
            {
                IdUtilisateur = request.IdUtilisateur,
                Montant = request.Montant,
                Date = DateTime.UtcNow,
                MethodePaiement = "Stripe",
                StripePaymentIntentId = paymentIntent.Id,
                FkAbonnement = request.FkAbonnement,
                CreatedAt = DateTime.UtcNow
            };

            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();

            return Ok(new { ClientSecret = paymentIntent.ClientSecret, PaiementId = paiement.IdPaiement });
        }
    }

    public class PaiementCreateRequest
    {
        public int IdUtilisateur { get; set; }
        public decimal Montant { get; set; }
        public int FkAbonnement { get; set; }
    }
}
