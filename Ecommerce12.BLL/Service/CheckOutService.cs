using Ecommerce12.DAL.DTO_s.Request.CheckOut;
using Ecommerce12.DAL.DTO_s.Response.CheckOut;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _senderEmail;
        private readonly IProductRepository _productRepository;

        public CheckOutService(ICartRepository cartRepository , IOrderRepository orderRepository,IOrderItemRepository orderItemRepository , UserManager<ApplicationUser> userManager, IEmailSender senderEmail,IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userManager = userManager;
            _senderEmail = senderEmail;
            _productRepository = productRepository;
        }


        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request , string userId )
        {
            // get user cart
            var cartUser = await _cartRepository.getCartForUser(userId);
            if (!cartUser.Any()) 
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "cart is empty"
                };
            }
            // price of products in cart 
            decimal totalAmount = 0;
            foreach (var item in cartUser)
            {
                if(item.Count > item.Product.Quantity)
                {
                    return new CheckOutResponse
                    {
                        Success = false , 
                        Message = "not enough stock"
                    };
                }
                totalAmount += item.Count * item.Product.Price;
            }

            // create order 
            Order order = new Order()
            {
                UserId = userId ,
                paymentMethodEnum = request.PaymentMethod,
                AmountPaid = totalAmount,
                paymentStatus = PaymentStatusEnum.UnPaid,
            };
            // Payment Senario 
            if(request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckOutResponse
                {
                    Success = true ,
                    Message = "cash"
                };
            }

            else if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"https://localhost:7277/api/CheckOut/success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"https://localhost:7277/checkout/cancel",
                    Metadata = new Dictionary <string, string>
                    {
                        { "UserId" , userId  },
                    }
                };
               foreach (var item in cartUser)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Translations.FirstOrDefault(t => t.Language == "en").Name
                            },
                                UnitAmount = (long)item.Product.Price * 100,
                        },
                        Quantity = item.Count,
                    });
                }

               // create session 
                var service = new SessionService();
                var session = service.Create(options);
                order.SessionId = session.Id;
                order.paymentStatus = PaymentStatusEnum.Paid;

                // add order in data base 
                await _orderRepository.CreateOrder(order);
                return new CheckOutResponse 
                {
                   Success = true , 
                   Message = "payment session created",
                   URL = session.Url
                };
            }
            else
            {
                return new CheckOutResponse 
                {
                    Success = false , 
                    Message = "Invalied Payment Method"
                };
            }
        }

        public async Task <CheckOutResponse> HandleSuccessAsync(string sessionId)
        {
             var service = new SessionService();
             var session = service.Get(sessionId);
             var userId = session.Metadata["UserId"];
            // get session
             var order = await _orderRepository.GetOrderBySessionId(sessionId);

            // update info in order 
             order.PaymentId = session.PaymentIntentId;
             order.orderStatusEnum = OrderStatusEnum.Approved;

             // update in data base 
             await _orderRepository.UpdateAsync(order);

             // add cart items in orderItems table & clear cart 
             var cartitems = await _cartRepository.getCartForUser(userId) ;
            // list to add orderitem 
             var orderItems = new List<OrderItems>();
             var ProductUpdated = new List<(int productId , int quantity)>();
             foreach (var item in cartitems)
             {
                var orderItem = new OrderItems
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count,
                    TotalPrice = item.Count * item.Product.Price
                };
                orderItems.Add(orderItem);
                ProductUpdated.Add((orderItem.ProductId,orderItem.Quantity));
             }


             await _orderItemRepository.createRangeAsync(orderItems);
             await _cartRepository.ClearCart(userId);
             await _productRepository.DecreaseQuantitiesAsync(ProductUpdated);
             // find user in data base to send email 
             var user = await _userManager.FindByIdAsync(userId);
             await _senderEmail.SendEmailAsync(user.Email, "Payment Successfull" , "<h2>Thank You </h2> ");

             return new CheckOutResponse
             {
                Success= true ,
                Message = "Payment Completed Successfully"
             };

        }
    }
}
