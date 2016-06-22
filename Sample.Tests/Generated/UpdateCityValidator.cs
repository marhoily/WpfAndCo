using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdateCityValidator
    {
		private readonly CityAggregate _CityAggregate;
	
		public UpdateCityValidator(
			CityAggregate CityAggregate
)
		{
			_CityAggregate = CityAggregate;
	
		}
		public ValidationResult Validate(UpdateCity commit)
		{
			if (!_CityAggregate.ById.ContainsKey(commit.Id))
				return new ValidationResult("Did not find City to be updated: " + commit.Id);
		
			return ValidationResult.Success;
		}
    }
}

