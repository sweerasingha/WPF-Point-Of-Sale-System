using FluentAssertions;
using POS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.xUnitTest
{
    public class SaleTests
    {
        [Fact]
        public void Sale_Id_Should_Have_KeyAttribute()
        {
            var propertyInfo = typeof(Sale).GetProperty("Id");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(KeyAttribute));
        }

        [Fact]
        public void Sale_Id_Should_Have_DatabaseGeneratedAttribute_With_IdentityOption()
        {
            var propertyInfo = typeof(Sale).GetProperty("Id");
            var databaseGeneratedAttribute = propertyInfo.GetCustomAttribute<DatabaseGeneratedAttribute>();

            databaseGeneratedAttribute.Should().NotBeNull();
            databaseGeneratedAttribute.DatabaseGeneratedOption.Should().Be(DatabaseGeneratedOption.Identity);
        }

        [Fact]
        public void Sale_SaleDate_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Sale).GetProperty("SaleDate");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Sale_CustomerId_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Sale).GetProperty("CustomerId");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Sale_CustomerId_Should_Have_ForeignKeyAttribute_Referencing_Customer()
        {
            var propertyInfo = typeof(Sale).GetProperty("CustomerId");
            var foreignKeyAttribute = propertyInfo.GetCustomAttribute<ForeignKeyAttribute>();

            foreignKeyAttribute.Should().NotBeNull();
            foreignKeyAttribute.Name.Should().Be("Customer");
        }

        [Fact]
        public void Sale_TotalAmount_Should_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Sale).GetProperty("TotalAmount");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().Contain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Sale_TotalAmount_Should_Have_ColumnAttribute_WithType_Decimal_With_Precision_18_2()
        {
            var propertyInfo = typeof(Sale).GetProperty("TotalAmount");
            var columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            columnAttribute.Should().NotBeNull();
            columnAttribute.TypeName.Should().Be("decimal(18,2)");
        }

        [Fact]
        public void Sale_IsCanceled_Should_Not_Have_RequiredAttribute()
        {
            var propertyInfo = typeof(Sale).GetProperty("IsCanceled");
            var attributes = propertyInfo.GetCustomAttributes(true).ToArray();

            attributes.Should().NotContain(attr => attr.GetType() == typeof(RequiredAttribute));
        }

        [Fact]
        public void Sale_Should_Have_Virtual_Customer_Property()
        {
            var propertyInfo = typeof(Sale).GetProperty("Customer");

            propertyInfo.GetGetMethod().IsVirtual.Should().BeTrue();
            propertyInfo.GetSetMethod().IsVirtual.Should().BeTrue();
        }

        [Fact]
        public void Sale_Should_Have_Virtual_SaleProducts_Property()
        {
            var propertyInfo = typeof(Sale).GetProperty("SaleProducts");

            propertyInfo.GetGetMethod().IsVirtual.Should().BeTrue();
            propertyInfo.GetSetMethod().IsVirtual.Should().BeTrue();
        }

        [Fact]
        public void Sale_SaleProducts_Should_Be_Initialized_In_Constructor()
        {
            var sale = new Sale();

            sale.SaleProducts.Should().NotBeNull();
            sale.SaleProducts.Should().BeOfType<List<SaleProduct>>();
        }

        [Fact]
        public void Sale_TotalAmount_Should_Be_Initialized_In_Constructor()
        {
            var sale = new Sale();

            sale.TotalAmount.Should().Be(0);
        }
    }
}
