﻿using Baraka_Savdo.Domain.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baraka_Savdo.Domain.Entities.Users
{
    public class User : Human
    {
        [MaxLength(13)]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [MaxLength(9)]
        public string? PassportSeriaNumber { get; set; } 

        public string PasswordHash { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public DateTime LastActivity { get; set; }

        public IdentityRole IdentityRole { get; set; }
    }
}
