﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class NegativeNumberException : DomainException
    {
        public override string ErrorCode => "negative_number";

        public NegativeNumberException(string message) : base(message)
        {

        }
    }
}