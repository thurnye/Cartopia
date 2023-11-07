using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RetWeb.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetWeb.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        /// <summary>
        /// The ApplicationUserId
        /// </summary>
        [ForeignKey("UserId")]
        [ValidateNever]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShippingDate { get; set; }

        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; } 

        public string? PaymentStatus {  get; set; } 

        public string? TrackingNumber { get; set; } 

        public string? Carrier { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateOnly PaymentDueDate { get; set; }

        public string? PaymentIntentId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
		[DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
		public string? Street { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
		[DisplayName("Postal Code")]
		public string? PostalCode { get; set; }

    }
}
