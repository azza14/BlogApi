﻿using BlogApi.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.DTO
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public RoleEnum Role { get; set; }
        [Required]

        public string Password { get; set; }

    }
}
