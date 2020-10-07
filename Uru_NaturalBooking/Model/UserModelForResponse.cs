﻿using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UserModelForResponse : ModelBase<User, UserModelForResponse>
    {
        public string UserName { get; set; }
        public string Mail { get; set; }

        

        public override User ToEntity() 
        {
            throw new NotImplementedException();
        }

        protected override UserModelForResponse SetModel(User entity)
        {
            UserName = entity.UserName;
            Mail = entity.Mail;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is UserModelForResponse response &&
                   UserName.Equals(response.UserName) &&
                   Mail.Equals(response.Mail);
        }
    }

}
