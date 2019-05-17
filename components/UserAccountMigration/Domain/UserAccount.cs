//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;
using System;

namespace UserAccountMigration.Domain
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst(1)]
    public class UserAccount
    {
        public UserAccount() { }

        [FieldOrder(1)]
        public string UserName { get; set; }

        [FieldOrder(2)]
        public string FirstName { get; set; }

        [FieldOrder(3)]
        public string MiddleName { get; set; }

        [FieldOrder(4)]
        public string LastName { get; set; }

        [FieldOrder(5)]
        public string EmailAddress { get; set; }

        [FieldOrder(6)]
        public string PhoneNumber { get; set; }

        [FieldOrder(7)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime? BirthDate { get; set; }

        [FieldOrder(8)]
        public string Last4SSN { get; set; }

        [FieldOrder(9)]
        public string Groups { get; set; }

        [FieldOrder(10)]
        public string GeneralId { get; set; }

        [FieldOrder(11)]
        public string Password { get; set; }

        public void Validate(bool isPasswordRequired = false)
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                throw new ArgumentNullException("UserName");
            }

            if (string.IsNullOrWhiteSpace(this.FirstName))
            {
                throw new ArgumentNullException("FirstName");
            }

            if (string.IsNullOrWhiteSpace(this.LastName))
            {
                throw new ArgumentNullException("FirstName");
            }

            if (string.IsNullOrWhiteSpace(this.EmailAddress))
            {
                throw new ArgumentNullException("EmailAddress");
            }

            if (this.BirthDate == null)
            {
                throw new ArgumentNullException("BirthDate");
            }

            if (string.IsNullOrWhiteSpace(this.Last4SSN))
            {
                throw new ArgumentNullException("Last4SSN");
            }

            if (string.IsNullOrWhiteSpace(this.Groups))
            {
                throw new ArgumentNullException("Groups");
            }

            if (isPasswordRequired && string.IsNullOrWhiteSpace(this.Password))
            {
                throw new ArgumentNullException("Password");
            }
        }
    }
}
