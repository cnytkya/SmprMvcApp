using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.Common
{
    public class Roles
    {
        //const anahtar sözcüğü, C# dilinde sabit (constant) bir değeri tanımlamak için kullanılır. const ile tanımlanan bir değişkenin değeri, derleme (compile) zamanı sırasında belirlenir ve daha sonra değiştirilemez.
        public const string Role_Customer = "Customer";

        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string Role_Company = "Company";

		public const string StatusPending = "Pending";
		public const string StatusApproved = "Approved";
		public const string StatusInProcess = "Processing";
		public const string StatusShipped = "Shipped";
		public const string StatusCancelled = "Cancelled";
		public const string StatusRefunded = "Refunded";

		public const string PaymentStatusPending = "Pending";
		public const string PaymentStatusApproved = "Approved";
		public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
		public const string PaymentStatusRejected = "Rejected";
	}
}