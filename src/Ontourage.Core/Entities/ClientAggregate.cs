using System;

namespace Ontourage.Core.Entities
{
    public class ClientAggregate
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Passport { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Discount Discount { get; set; }

        public int UserLevel { get; set; }

        public ClientAggregate(int id, string firstName, string lastName, string sex, DateTime dateOfBirth,
            string passport, string phoneNumber, string email, Discount discount, int userLevel)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            DateOfBirth = dateOfBirth;
            Passport = passport;
            PhoneNumber = phoneNumber;
            Email = email;
            Discount = discount;
            UserLevel = userLevel;
        }
    }
}
