using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class CreateCityValidator : IValidator<CreateCity>
    {
		private readonly CityAggregate _BrotherCityIdAggregate;
	
		public CreateCityValidator(
			CityAggregate BrotherCityIdAggregate)
		{
			_BrotherCityIdAggregate = BrotherCityIdAggregate;
	
		}
		public ValidationResult Validate(CreateCity commit)
		{
			if (commit.BrotherCityId != Guid.Empty)
			if (!_BrotherCityIdAggregate.ById.ContainsKey(commit.BrotherCityId))
				return new ValidationResult(
					"Wrong BrotherCityId: " + commit.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

