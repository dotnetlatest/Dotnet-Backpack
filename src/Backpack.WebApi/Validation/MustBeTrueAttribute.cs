﻿using System.ComponentModel.DataAnnotations;

namespace Backpack.WebApi.Validation
{
    public class MustBeTrueAttribute : ValidationAttribute 
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool) value;
        }
    }
}