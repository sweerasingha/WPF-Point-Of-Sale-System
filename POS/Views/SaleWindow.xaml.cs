﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using POS.DataAccess;
using POS.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace POS.Views
{
    public partial class SaleWindow : Window
    {
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<SaleProduct> SaleProducts { get; set; }

        public Sale Sale { get; private set; }
        public int SelectedCustomerId { get; set; }

        public SaleWindow(ObservableCollection<Customer> customers, ObservableCollection<Product> products)
        {
            InitializeComponent();
            Customers = customers;
            SaleProducts = new ObservableCollection<SaleProduct>();

            // Create a SaleProduct for each product in the products collection
            foreach (var product in products)
            {
                if (product != null) // Ensure the product object is not null before creating the SaleProduct
                {
                    SaleProducts.Add(new SaleProduct { Product = product, Quantity = 0 });
                }
            }

            Sale = new Sale();
            DataContext = this;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (SaleDatePicker.SelectedDate == null || CustomerComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please enter valid sale information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }*/
            Sale.SaleDate = DateTime.Now;

            //Sale.SaleDate = SaleDatePicker.SelectedDate.Value;
            Sale.CustomerId = ((Customer)CustomerComboBox.SelectedItem)?.Id ?? 0;

            // Calculate the total amount and add SaleProducts with a quantity greater than 0 to the Sale
            Sale.TotalAmount = 0;
            Sale.SaleProducts.Clear();

            using (var context = new POSDbContext())
            {
                if (SaleProducts != null)
                {
                    foreach (var saleProduct in SaleProducts.Where(sp => sp != null && sp.Quantity > 0))
                    {
                        if (saleProduct.Product != null)
                        {
                            // Check if the sale quantity is greater than the available quantity or if the product is out of stock
                            if (saleProduct.Quantity > saleProduct.Product.Quantity || saleProduct.Product.Quantity == 0)
                            {
                                MessageBox.Show($"The product '{saleProduct.Product.Name}' is out of stock. Please reduce the quantity or remove it from the sale.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            decimal total = saleProduct.Product.Price * saleProduct.Quantity;
                            Sale.TotalAmount += total;
                            Sale.SaleProducts.Add(new SaleProduct { ProductId = saleProduct.Product.Id, Quantity = saleProduct.Quantity, Total = total });

                            // Update the product's quantity in the database
                            var productToUpdate = context.Products.FirstOrDefault(p => p.Id == saleProduct.Product.Id);
                            if (productToUpdate != null)
                            {
                                productToUpdate.Quantity -= saleProduct.Quantity;
                            }
                        }
                        else
                        {
                            MessageBox.Show("One or more products in the sale are invalid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The SaleProducts collection is not properly initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                context.Sales.Add(Sale);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
                    {
                        MessageBox.Show("There was an error adding the sale. The sale ID is not unique. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("There was an error adding the sale. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            DialogResult = true;
        }












        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
