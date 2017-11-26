using Ontourage.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class ClientAggregateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фалимия")]
        public string LastName { get; set; }

        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Паспортные данные")]
        public string Passport { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Тип скидки")]
        public Discount Discount { get; set; }

        [Display(Name = "Уровень путешественника")]
        public int UserLevel { get; set; }

        public ClientAggregateViewModel(ClientAggregate client)
        {
            BindFromModel(client);
        }

        public void BindFromModel(ClientAggregate client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Sex = client.Sex;
            DateOfBirth = client.DateOfBirth;
            Passport = client.Passport;
            PhoneNumber = client.PhoneNumber;
            Email = client.Email;
            Discount = client.Discount;
            UserLevel = client.UserLevel;
        }

        public ClientAggregate CreateFromViewModel()
        {
            return new ClientAggregate(Id, FirstName, LastName, Sex, DateOfBirth,
            Passport, PhoneNumber, Email, Discount, UserLevel);
        }
    }
}
