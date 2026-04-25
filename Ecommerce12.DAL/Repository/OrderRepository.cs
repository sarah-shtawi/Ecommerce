using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order Request)
        {
            await _context.orders.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }

        public async Task<Order> GetOrderBySessionId(string sessionId)
        {
            return await _context.orders.FirstOrDefaultAsync(o=> o.SessionId == sessionId);
        }
        public async Task<Order> UpdateAsync(Order order)
        {
            _context.orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }


        public async Task<List<Order>> GetOrdersByStatus(OrderStatusEnum Status)
        {
           return  await  _context.orders.Where(o => o.orderStatusEnum == Status )
                                         .Include(o=>o.User)
                                         .ToListAsync();
        }

        public async Task <Order?> GetOrderById(int orderId)
        {
            return await _context.orders
                .Include(o=>o.User)
                .Include(o=>o.orderItems)
                .ThenInclude(o=>o.Product)
                .FirstOrDefaultAsync(o=>o.Id == orderId);
        }

        public async Task <bool> HasUserDeliverdOrderForProduct(string userId , int productId)
        {
            return await _context.orders
                .Where(o=>o.UserId == userId && o.orderStatusEnum == OrderStatusEnum.Delivered)
                .SelectMany(o => o.orderItems)
                .AnyAsync(oi =>oi.ProductId == productId);
        }

    }
}

