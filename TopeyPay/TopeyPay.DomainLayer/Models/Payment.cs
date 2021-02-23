﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopeyPay.Domain.Models
{
    public class Payment
    {

        [Key]
        public long PaymentId { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string SecurityCode { get; set; }
        [Required]
        public  decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
