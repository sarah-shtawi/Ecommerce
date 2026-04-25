using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public  class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task createRangeAsync(List<OrderItems> orderItems)
        {
            await _context.OrderItems.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();
        }


    }
}
