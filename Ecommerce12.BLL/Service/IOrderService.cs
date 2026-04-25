using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.Order;
using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  interface IOrderService
    {
        Task<List<OrderResponse>> GetOrders(OrderStatusEnum Status);

        Task<Order> GetOrderById(int orderId);

        Task<BaseResponse> UpdateOrderStatus(int orderId, OrderStatusEnum newStatus);
    }
}
