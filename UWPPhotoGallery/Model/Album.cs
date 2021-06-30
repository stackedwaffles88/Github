using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPPhotoGallery.Model
{
    public class Album
    {
        public string Name { get; set; }
        public string CoverPhotoFile { get; set; }
        public BitmapImage CoverImage { get; set; }
    }

    /*public class Product
    {
        private int productID;
        private string productCategory;
        private string subCategory;
        private string productName;
        private string productDescription;
        private decimal productPrice;
        private double productWeight;
        private int units;
        private DateTime manufacturedDate;
        private DateTime expiryDate;

        public int ProductID
        {
            get { return productID; }
        }
        public string ProductCategory
        {
            get { return productCategory; }
        }
        public string SubCategory
        {
            get { return subCategory; }
        }
        public string ProductName
        {
            get { return productName; }
        }
        public string ProductDescription
        {
            get { return productDescription; }
        }
        public decimal ProductPrice
        {
            get { return productPrice; }
        }
        public double ProductWeight
        {
            get { return productWeight; }
            
        }
        public int Units
        {
            get { return units; }
            set { units = value; }
        }
        public decimal Total
        {
            get { return Units * ProductPrice; }
        }
        public DateTime ManufacturedDate
        {
            get { return manufacturedDate;  }
        }
        public DateTime ExpirtyDate
        {
            get { return expiryDate; }
        }
        public Product(int productID, string farm, string productCategory,
            string subCategory, string productName, string productDescription,
                decimal productPrice, double productWeight, int units, DateTime mandate, DateTime expdate)
        {
            this.productID = productID;
            this.productCategory = productCategory;
            this.subCategory = subCategory;
            this.productName = productName;
            this.productDescription = productDescription;
            this.productPrice = productPrice;
            this.productWeight = productWeight;
            this.units = units;
            this.manufacturedDate = mandate;
            this.expiryDate = expdate;
        }
        
    }*/
}
