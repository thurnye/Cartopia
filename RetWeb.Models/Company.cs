using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RetWeb.Models
{
    public class Company
    {

        /// <summary>
        /// Id of the Product
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the Product
        /// </summary>
        [Required]
        [DisplayName("Company Name")]
        public string? Name { get; set; }

        /// <summary>
        /// StreetAdress of the company
        /// </summary>
        public string? StreetAddress { get; set; }

        /// <summary>
        ///  City
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        ///  State/Province
        /// </summary>
        public string? StateOrProvince { get; set; }
        
        /// <summary>
        ///  State/Province
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        ///  State/Province
        /// </summary>
        public string? PhoneNumber { get; set; }


        public bool IsDeleted { get; set; } = false;
    }
}
