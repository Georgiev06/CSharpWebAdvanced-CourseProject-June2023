using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.UsersOrder = new HashSet<UserOrder>();
            this.OrdersBooks = new HashSet<OrderBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderBook> OrdersBooks { get; set; }

        public virtual ICollection<UserOrder> UsersOrder { get; set; } = null!;
    }
}
