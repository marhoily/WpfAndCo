using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class DeleteCityValidator
    {
		private readonly CityAggregate _CityAggregate;
	
		public DeleteCityValidator(
			CityAggregate CityAggregate
)
		{
			_CityAggregate = CityAggregate;
	
		}
		public ValidationResult Validate(DeleteCity commit)
		{
			if (!_CityAggregate.ById.ContainsKey(commit.Id))
				return new ValidationResult("Did not find City to be Deleted: " + commit.Id);
		
			return ValidationResult.Success;
		}
    }
}

