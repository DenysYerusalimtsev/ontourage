using Ontourage.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным полем")]
        public string FirstName { get; set; }

        [Display(Name = "Фалимия")]
        [Required(ErrorMessage = "Фамилия является обязательным полем")]
        public string LastName { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Пол является обязательным полем")]
        public string Sex { get; set; }
        public IEnumerable<string> ChooseSex { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Дата рождения является обязательным полем")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Паспортные данные")]
        [Required(ErrorMessage = "Паспортные данные является обязательным полем")]
        public string Passport { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона является обязательным полем")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email является обязательным полем")]
        public string Email { get; set; }

        [Display(Name = "Тип скидки")]
        [Required(ErrorMessage = "Тип скидки является обязательным полем")]
        public int DiscountId { get; set; }
        public IEnumerable<Discount> Discounts { get; set; }

        [Display(Name = "Уровень путешественника")]
        public int UserLevel { get; set; }

        public HeaderViewModel Header { get; set; }

        public void BindFromModel(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Sex = client.Sex;
            DateOfBirth = client.DateOfBirth;
            Passport = client.Passport;
            PhoneNumber = client.PhoneNumber;
            Email = client.Email;
            DiscountId = client.DiscountId;
            UserLevel = client.UserLevel;
        }

        public Client CreateFromViewModel()
        {
            return new Client(Id, FirstName, LastName, Sex, DateOfBirth, Passport, PhoneNumber,
            Email, DiscountId, UserLevel);
        }
    }
}
