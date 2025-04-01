using ECommerceApp.Data;
using ECommerceApp.Domain;

namespace ECommerceApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ECommerceAppContext dbContext;
        public PaymentRepository(ECommerceAppContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task SavePaymentAsync(Payment payment)
        {
            await dbContext.Payment.AddAsync(payment);
            await dbContext.SaveChangesAsync();
        }
    }
}
