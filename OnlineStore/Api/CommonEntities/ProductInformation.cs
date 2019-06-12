using System;
using System.Collections.Generic;

namespace CommonEntities
{
	public class ProductInformation : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int ProductNameMaxLength { get; } = 50;
		public static int ImageLocalSourceMaxLength { get; } = 100;
		public static int DescriptionMaxLength { get; } = 500;

		public static int ProductNameMinLength { get; } = 1;
		public static int ImageLocalSourceMinLength { get; } = 1;
		public static int DescriptionMinLength { get; } = 0;

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
				(bool, string) validParameter = ValidName(value);
				if (validParameter.Item1)
					_productName = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(ProductName)}");

			}
		}
		public string ImageLocalSource
		{
			get => _imageLocalSource;
			set
			{
				(bool, string) validParameter = ValidImageSource(value);
				if (validParameter.Item1)
					_imageLocalSource = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(ImageLocalSource)}");

			}
		}
		public string Description
		{
			get => _description;
			set
			{
				(bool, string) validParameter = ValidDescription(value);
				if (validParameter.Item1)
					_description = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(Description)}");

			}
		}
		public List<ProductCategory> ProductCategories { get; set; }



		#endregion

		#region Конструктор

		public ProductInformation()
		{

		}
		
		public ProductInformation(string productName, string imageLocalSource, string description, List<ProductCategory> productCategories)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			ProductCategories = productCategories;
		}
		public ProductInformation(int productListId, string productName, string imageLocalSource, string description, List<ProductCategory> productCategories) : base(productListId)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			ProductCategories = productCategories;
		}

		#endregion

		#region Проверка корректности полей модели

		public static (bool, string) ValidName(string productName)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(productName))
				result = (false, $"{nameof(productName)} must not be empty");
			else if (productName.Length < ProductNameMinLength)
				result = (false, $"{nameof(ProductName)}  must not be shorter than {ProductNameMinLength} characters");
			else if (productName.Length > ProductNameMaxLength)
				result = (false, $"{nameof(ProductName)}  must not exceed {ProductNameMaxLength} characters");

			return result;
		}
		public static (bool, string) ValidImageSource(string imageSource)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(imageSource))
				result = (false, $"{nameof(ImageLocalSource)} must not be empty");
			else if (imageSource.Length < ImageLocalSourceMinLength)
				result = (false, $"{nameof(ImageLocalSource)} must not be shorter than {ImageLocalSourceMinLength} characters");
			else if (imageSource.Length > ImageLocalSourceMaxLength)
				result = (false, $"{nameof(ImageLocalSource)} must not exceed {ImageLocalSourceMaxLength} characters");

			return result;
		}
		public static (bool, string) ValidDescription(string description)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(description))
				result = (false, $"{nameof(Description)} must not be empty");
			else if (description.Length > DescriptionMaxLength)
				result = (false, $"{nameof(Description)}  must not exceed {DescriptionMaxLength} characters");

			return result;
		}

		#endregion
	}
}
