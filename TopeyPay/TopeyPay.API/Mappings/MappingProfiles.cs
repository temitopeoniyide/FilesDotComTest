using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopeyPay.Domain.Models;
using TopeyPay.DTOs;

namespace TopeyPay.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentDTO>(); //Map from Payment Object to PaymentDTO Object
        }
    }
}
