﻿namespace DigitalDevices.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
    }
}
