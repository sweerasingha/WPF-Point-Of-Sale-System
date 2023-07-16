using FluentAssertions;
using POS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.xUnitTest
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Id_Should_Have_KeyAttribute()
        {
            var propertyInfo = typeof(Customer).GetProperty("Id");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(KeyAttribute));
        }

        [Fact]
        public void Customer_Name_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Customer).GetProperty("Name");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Customer_Name_Should_Have_StringLengthAttribute_With_MaxLength_100()
        {
            var propertyInfo = typeof(Customer).GetProperty("Name");
            var stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();

            stringLengthAttribute.Should().NotBeNull();
            stringLengthAttribute.MaximumLength.Should().Be(100);
        }

        [Fact]
        public void Customer_PhoneNumber_Should_Have_StringLengthAttribute_With_MaxLength_20()
        {
            var propertyInfo = typeof(Customer).GetProperty("PhoneNumber");
            var stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();

            stringLengthAttribute.Should().NotBeNull();
            stringLengthAttribute.MaximumLength.Should().Be(20);
        }

        [Fact]
        public void Customer_Email_Should_Have_StringLengthAttribute_With_MaxLength_100()
        {
            var propertyInfo = typeof(Customer).GetProperty("Email");
            var stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();

            stringLengthAttribute.Should().NotBeNull();
            stringLengthAttribute.MaximumLength.Should().Be(100);
        }

        [Fact]
        public void Customer_Email_Should_Have_EmailAddressAttribute()
        {
            var propertyInfo = typeof(Customer).GetProperty("Email");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(EmailAddressAttribute));
        }
    }
}
