using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetWeb.Utility
{
    /// <summary>
    /// Static Roles
    /// </summary>
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
    }
}


//Customer is the basic customer who comes to the website, creates an account and places an order

//Company is similar to customer but they do not have to make the payment right away, they have upto 30 days to make the payment  after the order has been placed. 
//They can only be registered by and admin or employee user.

//Employee has access to modify the shipping of the product and other details.

//Admin performs the CRUD on all contents and managements

