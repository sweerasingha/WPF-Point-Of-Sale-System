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
    public class ProductTests
    {
        [Fact]
        public void Product_Id_Should_Have_KeyAttribute()
        {
            var propertyInfo = typeof(Product).GetProperty("Id");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(KeyAttribute));
        }

        [Fact]
        public void Product_Name_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Product).GetProperty("Name");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Product_Name_Should_Have_StringLengthAttribute_With_MaxLength_100()
        {
            var propertyInfo = typeof(Product).GetProperty("Name");
            var stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();

            stringLengthAttribute.Should().NotBeNull();
            stringLengthAttribute.MaximumLength.Should().Be(100);
        }

        [Fact]
        public void Product_Description_Should_Have_StringLengthAttribute_With_MaxLength_255()
        {
            var propertyInfo = typeof(Product).GetProperty("Description");
            var stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();

            stringLengthAttribute.Should().NotBeNull();
            stringLengthAttribute.MaximumLength.Should().Be(255);
        }

        [Fact]
        public void Product_Price_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Product).GetProperty("Price");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Product_Price_Should_Have_RangeAttribute_With_MinValue_Zero()
        {
            var propertyInfo = typeof(Product).GetProperty("Price");
            var rangeAttribute = propertyInfo.GetCustomAttribute<RangeAttribute>();

            rangeAttribute.Should().NotBeNull();
            rangeAttribute.Minimum.Should().Be(0);
        }

        [Fact]
        public void Product_Quantity_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Product).GetProperty("Quantity");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Product_Quantity_Should_Have_RangeAttribute_With_MinValue_Zero()
        {
            var propertyInfo = typeof(Product).GetProperty("Quantity");
            var rangeAttribute = propertyInfo.GetCustomAttribute<RangeAttribute>();

            rangeAttribute.Should().NotBeNull();
            rangeAttribute.Minimum.Should().Be(0);
        }
    }
}
