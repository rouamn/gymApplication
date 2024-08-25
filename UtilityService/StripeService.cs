using System;
using Stripe;
using System.Threading.Tasks;

namespace GymApplication.UtilityService

{
    public class StripeService
    {
        public StripeService()
        {
            StripeConfiguration.ApiKey = "your_stripe_secret_key";
        }

        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe expects the amount in cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent;
        }

    }
}
