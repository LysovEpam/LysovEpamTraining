using System;
using FluentValidation;

namespace CommonEntities
{
	public abstract class BaseDbEntity 
	{
		private int? _idEntity;

		public int? IdEntity
		{
			get => _idEntity;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"Parameter {nameof(IdEntity)} mast be more zero", $"{nameof(IdEntity)}");

				_idEntity = value;
			}
		}

		protected BaseDbEntity()
		{
			_idEntity = null;
		}

		protected BaseDbEntity(int idEntity)
		{
			IdEntity = idEntity;
		}
	}
}
