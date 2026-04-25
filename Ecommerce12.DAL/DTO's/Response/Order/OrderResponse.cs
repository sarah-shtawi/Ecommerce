using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.Order
{
    public  class OrderResponse
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatusEnum orderStatusEnum { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatusEnum paymentStatus { get; set; }

        public decimal? AmountPaid { get; set; }

        public string UserName { get; set; }

    }
}
