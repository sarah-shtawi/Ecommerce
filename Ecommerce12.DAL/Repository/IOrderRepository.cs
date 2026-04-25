using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public  interface IOrderRepository 
    {
       public Task<Order> CreateOrder(Order Request);
        Task<Order> GetOrderBySessionId(string sessionId);
        Task<Order> UpdateAsync(Order order);
        Task<List<Order>> GetOrdersByStatus(OrderStatusEnum Status);
        Task<bool> HasUserDeliverdOrderForProduct(string userId, int productId);
        Task<Order?> GetOrderById(int orderId);
    }
}
