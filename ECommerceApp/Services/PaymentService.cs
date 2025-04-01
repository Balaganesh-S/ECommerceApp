using ECommerceApp.Repositories;

namespace ECommerceApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository) { 
            this.paymentRepository = paymentRepository;
        }
    }
}
