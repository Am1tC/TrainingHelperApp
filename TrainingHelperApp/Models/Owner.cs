﻿using System.ComponentModel.DataAnnotations;

namespace TrainingHelperApp.Models
{
    public class Owner
    {
     
        public string OwnerId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Owner() { }
        public Owner(Models.Owner owner)
        {
            this.OwnerId = owner.OwnerId;
            this.Password = owner.Password;
            this.FirstName = owner.FirstName;
            this.LastName = owner.LastName;

        }
        public Models.Owner GetModel()
        {
            Models.Owner o = new Models.Owner();
            o.OwnerId = this.OwnerId;
            o.Password = this.Password;
            o.FirstName = this.FirstName;
            o.LastName = this.LastName;
            return o;
        }

    }
}
