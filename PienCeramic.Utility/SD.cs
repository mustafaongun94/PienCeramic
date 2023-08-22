using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PienCeramic.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        public const string StatusPending = "Askıda";
		public const string StatusApproved = "Onaylandı";
		public const string StatusProcess = "Hazırlanıyor";
		public const string StatusShipped = "Sevk";
		public const string StatusCancelled = "İptal";
		public const string StatusRefunded = "İade";

        public const string PaymentStatusPending = "Askıda";
		public const string PaymentStatusApproved = "Onaylandı";
		public const string PaymentStatusDelayedPayment = "GecikmişÖdemeOnaylandı";
		public const string PaymentStatusRejected = "Reddedildi";



	}
}
