using System;
using FluentValidation;

namespace CommonEntities
{
	public class ProductCategory : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int CategoryNameMaxLength { get; } = 50;
		public static int DescriptionMaxLength { get; } = 500;

		#endregion

		#region Свойства класса

		private string _categoryName;
		private string _description;


		public string CategoryName
		{
			get => _categoryName;
			set
			{
				(bool, string) validParameter = ValidCategoryName(value);

				if (validParameter.Item1)
					_categoryName = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(CategoryName)}");

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


		#endregion

		#region Конструктор

		public ProductCategory()
		{

		}
		public ProductCategory(string category, string description)
		{
			CategoryName = category;
			Description = description;
		}

		public ProductCategory(int id, string category, string description) : base(id)
		{
			CategoryName = category;
			Description = description;
		}

		#endregion

		#region Проверка корректности полей модели

		public static (bool, string) ValidCategoryName(string categoryName)
		{
			(bool, string) result = (true, null);


			if (string.IsNullOrEmpty(categoryName))
				result = (false, $"{nameof(categoryName)} must not be empty");
			else if (categoryName.Length > CategoryNameMaxLength)
				result = (false, $"{nameof(CategoryName)}  must not exceed {CategoryNameMaxLength} characters");

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
