using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdateCityValidator : IValidator<UpdateCity>
    {
		private readonly CityAggregate _CityAggregate;
		private readonly CityAggregate _BrotherCityIdAggregate;
	
		public UpdateCityValidator(
			CityAggregate CityAggregate
			,CityAggregate BrotherCityIdAggregate
)
		{
			_CityAggregate = CityAggregate;
			_BrotherCityIdAggregate = BrotherCityIdAggregate;
	
		}
		public ValidationResult Validate(UpdateCity commit)
		{
			CityRow row;
			if (!_CityAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find City to be updated: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't update object v.{row.RowVersion} with commit v.{commit.RowVersion}");
				
			if (commit.BrotherCityId != Guid.Empty)
			if (!_BrotherCityIdAggregate.ById.ContainsKey(commit.BrotherCityId))
				return new ValidationResult("Wrong BrotherCityId: " + commit.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

