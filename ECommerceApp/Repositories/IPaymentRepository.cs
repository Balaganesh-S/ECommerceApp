using ECommerceApp.Domain;

namespace ECommerceApp.Repositories
{
    public interface IPaymentRepository
    {
        public Task SavePaymentAsync(Payment payment);
    }
}
