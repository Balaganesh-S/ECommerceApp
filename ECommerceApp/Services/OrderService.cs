using AutoMapper;
using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace ECommerceApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly ECommerceAppContext dbContext;
        public OrderService(IProductRepository productRepository, ECommerceAppContext dbContext, IMapper mapper, IOrderItemRepository orderItemRepository, IPaymentRepository paymentRepository,IAddressRepository addressRepository,ICartRepository cartRepository, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.orderRepository = orderRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;
            this.addressRepository = addressRepository;
            this.paymentRepository = paymentRepository;
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.productRepository = productRepository;
        }

        public async Task<ResponseDto<OrderResponseDto>> PlaceOrder(int addressId, string paymentMethod, string pGName, string pGPaymentId, string pGStatus, string pGResponseMessage)
        {
            var userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            User user = await userRepository.GetUserByEmailAsync(userEmail);
            Cart cart = await cartRepository.GetCartByUserEmailAsync(userEmail);

            if (cart == null || cart.Items.Count == 0)
            {
                throw new KeyNotFoundException("Cart is Empty!");
            }

            // Retrieve Address
            Address address = await addressRepository.GetAddressById(addressId);
            if (address == null)
            {
                throw new KeyNotFoundException("The provided address does not exist.");
            }
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            // Create Order
            Order order = new Order
            {
                Email = userEmail,
                OrderDate = DateTime.Now,
                TotalAmount = 0, // Will update later
                OrderStatus = "Accepted",
                Address = address
            };
            try
            {

                await orderRepository.SaveOrderAsync(order); // ✅ Save Order First

                // Ensure order has an ID after saving
                if (order.Id == 0)
                {
                    throw new Exception("Failed to generate OrderId after saving order.");
                }

                // Create Payment AFTER Order is saved
                Payment payment = new Payment
                {
                    OrderId = order.Id,  // ✅ Assign OrderId explicitly
                    PaymentMethod = paymentMethod,
                    PGPaymentId = pGPaymentId,
                    PGStatus = pGStatus,
                    PGResponseMessage = pGResponseMessage,
                    PGName = pGName
                };

                await paymentRepository.SavePaymentAsync(payment); // ✅ Save Payment

                // Create OrderItems
                List<OrderItem> orderItems = new List<OrderItem>();
                decimal totalAmount = 0;

                foreach (CartItem cartItem in cart.Items)
                {
                    if (await productRepository.GetProductQuantityByID((int)cartItem.ProductId) < cartItem.Quantity) {
                        throw new Exception("The product doesn't have enough quantity");
                    }
                    await productRepository.DecreaseProductQuantityAsync((int)cartItem.ProductId, cartItem.Quantity);
                    OrderItem orderItem = new OrderItem
                    {
                        ProductId = (int)cartItem.ProductId,
                        OrderedProductPrice = (decimal)cartItem.ProductPrice,
                        Product = cartItem.Product,
                        Quantity = cartItem.Quantity,
                        OrderId = order.Id // ✅ Assign OrderId explicitly
                        
                    };
                    totalAmount += orderItem.Quantity * orderItem.OrderedProductPrice;
                    orderItems.Add(orderItem);
                }

                // Update total order amount and save again
                order.TotalAmount = totalAmount;
                //await orderRepository.UpdateOrderAsync(order); // ✅ Update Order TotalAmount

                // Save order items
                foreach (var orderItem in orderItems)
                {
                    await orderItemRepository.SaveOrderItemAsync(orderItem);
                }

                // Clear cart after placing order
                await cartRepository.ClearCartAsync(userEmail);
                

                await transaction.CommitAsync();
                return new ResponseDto<OrderResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Order placed successfully.",
                    Data = null
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            
        }


        public async Task<ResponseDto<List<OrderResponseDto>>> GetAllOrdersByUser()
        {
            var userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            User user = await userRepository.GetUserByEmailAsync(userEmail);
            List<Order> orders = await orderRepository.GetAllOrdersByUserEmail(userEmail);
            List<OrderResponseDto> ordersDto= new List<OrderResponseDto>();
            foreach (var order in orders) {
                OrderResponseDto orderDto = new OrderResponseDto();
                orderDto.Id = order.Id;
                orderDto.Email = order.Email;
                orderDto.OrderItems = mapper.Map<List<OrderItemResponseDto>>(order.OrderItems);
                orderDto.OrderDate = order.OrderDate;
                orderDto.PaymentResponse = mapper.Map<PaymentResponseDto>(order.Payment);
                orderDto.TotalAmount = order.TotalAmount;
                orderDto.OrderStatus = order.OrderStatus;
                orderDto.AddressResponseDto = mapper.Map<AddressResponseDto>(order.Address);
                ordersDto.Add(orderDto);
            }
            return new ResponseDto<List<OrderResponseDto>> { 
                StatusCode = HttpStatusCode.OK,
                Message = "Fetched Successfull",
                Data = ordersDto
            };

        }

    }
}
