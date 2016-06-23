using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdateCityValidator : IValidator<UpdateCity>
    {
		private readonly CityAggregate _cityAggregate;
		private readonly CityAggregate _brotherCityIdAggregate;
	
		public UpdateCityValidator(
			CityAggregate cityAggregate
			,CityAggregate brotherCityIdAggregate
)
		{
			_cityAggregate = cityAggregate;
			_brotherCityIdAggregate = brotherCityIdAggregate;
	
		}
		public ValidationResult Validate(UpdateCity commit)
		{
			CityRow row;
			if (!_cityAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find City to be updated: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't update object v.{row.RowVersion} with commit v.{commit.RowVersion}");
				
			if (commit.BrotherCityId != Guid.Empty)
			if (!_brotherCityIdAggregate.ById.ContainsKey(commit.BrotherCityId))
				return new ValidationResult("Wrong BrotherCityId: " + commit.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

