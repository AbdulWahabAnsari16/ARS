﻿using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class AdminLogin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
