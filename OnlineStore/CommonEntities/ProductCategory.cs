using System;

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
				int categoryMinLength = 2;

				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(CategoryName)} must not be empty", $"{nameof(CategoryName)}");
				if (value.Length < categoryMinLength)
					throw new ArgumentException($"The parameter {nameof(CategoryName)} must not be shorter than {categoryMinLength} characters", $"{nameof(CategoryName)}");
				if (value.Length > CategoryNameMaxLength)
					throw new ArgumentException($"The parameter {nameof(CategoryName)} must not exceed {CategoryNameMaxLength} characters", $"{nameof(CategoryName)}");

				_categoryName = value;
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


	}
}
