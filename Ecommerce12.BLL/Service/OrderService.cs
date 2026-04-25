using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.Order;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task <List<OrderResponse>> GetOrders(OrderStatusEnum Status)
        {
            var orders = await _orderRepository.GetOrdersByStatus(Status);
            return orders.Adapt<List<OrderResponse>>();
             
        }
        public async Task <Order> GetOrderById(int orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }


        public async Task<BaseResponse> UpdateOrderStatus(int orderId , OrderStatusEnum newStatus)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            order.orderStatusEnum = newStatus;
            if(newStatus == OrderStatusEnum.Delivered)
            {
                order.paymentStatus = PaymentStatusEnum.Paid;
            }
            else if (newStatus == OrderStatusEnum.Cancelled)
            {
                if (order.orderStatusEnum == OrderStatusEnum.Shipped)
                {
                    return new BaseResponse
                    {
                        Success= false , 
                        Message = "can't cancelled order"
                    };
                }
            }
             await _orderRepository.UpdateAsync(order);
            return new BaseResponse 
            {
               Success = true,
               Message = "order status updated"
            };
           
        }




    }
}
