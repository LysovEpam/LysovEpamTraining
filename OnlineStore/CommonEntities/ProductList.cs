using System;
using System.Collections.Generic;

namespace CommonEntities
{
	public class ProductList : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int ProductNameMaxLength { get; } = 50;
		public static int ImageLocalSourceMaxLength { get; } = 50;
		public static int DescriptionMaxLength { get; } = 50;

		#endregion

		#region Свойства класса

		private string _productName;
		private string _imageLocalSource;
		private string _description;


		public string ProductName
		{
			get => _productName;
			set
			{
				int nameMinLength = 2;

				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(ProductName)} must not be empty", $"{nameof(ProductName)}");
				if (value.Length < nameMinLength)
					throw new ArgumentException($"The parameter {nameof(ProductName)} must not be shorter than {nameMinLength} characters", $"{nameof(ProductName)}");
				if (value.Length > ProductNameMaxLength)
					throw new ArgumentException($"The parameter {nameof(ProductName)} must not exceed {ProductNameMaxLength} characters", $"{nameof(ProductName)}");

				_productName = value;
			}
		}
		public string ImageLocalSource
		{
			get => _imageLocalSource;
			set
			{

				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(ImageLocalSource)} must not be empty", $"{nameof(ImageLocalSource)}");
				if (value.Length > ImageLocalSourceMaxLength)
					throw new ArgumentException($"The parameter {nameof(ImageLocalSource)} must not exceed {ImageLocalSourceMaxLength} characters", $"{nameof(ImageLocalSource)}");

				_imageLocalSource = value;
			}
		}
		public string Description
		{
			get => _description;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(Description)} must not be empty", $"{nameof(Description)}");
				if (value.Length > DescriptionMaxLength)
					throw new ArgumentException($"The parameter {nameof(Description)} must not exceed {DescriptionMaxLength} characters", $"{nameof(Description)}");

				_description = value;
			}
		}
		public List<int> IdProductCategories { get; }
		public List<ProductCategory> ProductCategories { get; }
		


		#endregion

		#region Конструктор

		public ProductList()
		{

		}
		public ProductList(string productName, string imageLocalSource, string description, List<int> idProductCategories)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			IdProductCategories = idProductCategories;
			ProductCategories = null;
		}
		public ProductList(string productName, string imageLocalSource, string description, List<ProductCategory> productCategories)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			IdProductCategories = new List<int>();

			foreach (ProductCategory productCategory in productCategories)
			{
				if (productCategory.IdEntity != null)
					IdProductCategories.Add(productCategory.IdEntity.Value);
			}

			ProductCategories = productCategories;
		}
		public ProductList(int productListId, string productName, string imageLocalSource, string description, List<ProductCategory> productCategories) : base(productListId)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			IdProductCategories = new List<int>();

			foreach (ProductCategory productCategory in productCategories)
			{
				if (productCategory.IdEntity != null)
					IdProductCategories.Add(productCategory.IdEntity.Value);
			}

			ProductCategories = productCategories;
		}

		#endregion
	}
}
