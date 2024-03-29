﻿namespace Demo.Entities
{
    using System;

    public class User
    {
        public User() => Id = Guid.NewGuid();

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
