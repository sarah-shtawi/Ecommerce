using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Models
{
    public enum OrderStatusEnum 
    {
      Pending = 1 ,
      Cancelled = 2 , 
      Approved = 3 , 
      Shipped = 4 , 
      Delivered = 5 
    }
    public enum PaymentMethodEnum
    {
        Visa = 1 , 
        Cash = 2 
    }
    public enum PaymentStatusEnum
    {
        UnPaid = 1,
        Paid = 2
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatusEnum orderStatusEnum { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ShippedDate {  get; set; } 
        public PaymentMethodEnum paymentMethodEnum {  get; set; }
        public PaymentStatusEnum paymentStatus { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentId { get; set; }
        public decimal? AmountPaid {  get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderItems> orderItems { get; set; }
    }
}
