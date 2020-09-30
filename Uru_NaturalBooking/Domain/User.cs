﻿using DomainException;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                User user = (User)obj;
                return Mail.Equals(user.Name);
            }
        }
    }

}
