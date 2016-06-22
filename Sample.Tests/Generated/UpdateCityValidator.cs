using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdateCityValidator
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
			if (!_CityAggregate.ById.ContainsKey(commit.Id))
				return new ValidationResult("Did not find City to be updated: " + commit.Id);
			if (commit.BrotherCityId != Guid.Empty)
			if (!_BrotherCityIdAggregate.ById.ContainsKey(commit.BrotherCityId))
				return new ValidationResult("Wrong BrotherCityId: " + commit.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

