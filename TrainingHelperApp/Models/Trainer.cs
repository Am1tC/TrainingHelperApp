﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrainingHelperApp.Models;
using TrainingHelper.Services;

namespace TrainingHelperApp.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }

        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }


        public string Gender { get; set; } = null!;


        public string PhoneNum { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Picture { get; set; }
        public bool IsActive { get; set; }

        public string PhotoUrl
        {
            get
            {
                return TrainingHelperWebAPIProxy.ImageBaseAddress + Picture;
            }
        }
        //public virtual ICollection<Training> Training { get; set; } = new List<Training>();

        public Trainer() { }
        public Trainer(Models.Trainer tr)
        {
            this.TrainerId = tr.TrainerId;
            this.FirstName = tr.FirstName;
            this.LastName = tr.LastName;
            this.BirthDate = tr.BirthDate;
            this.Gender = tr.Gender;
            this.PhoneNum = tr.PhoneNum;
            this.Email = tr.Email;
            this.Picture = tr.Picture;
            this.IsActive = tr.IsActive;
        }

        public Models.Trainer GetModel()
        {
            Models.Trainer tr = new Models.Trainer();
            tr.TrainerId= this.TrainerId;
            tr.FirstName = this.FirstName;
            tr.LastName = this.LastName;
            tr.BirthDate = this.BirthDate;
            tr.Gender = this.Gender;
            tr.PhoneNum = this.PhoneNum;
            tr.Email = this.Email;
            tr.Picture = this.Picture;
            tr.IsActive = this.IsActive;
            return tr;
        }

    }
}
