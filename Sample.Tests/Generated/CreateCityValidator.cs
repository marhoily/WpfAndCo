using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class CreateCityValidator : IValidator<CreateCity>
    {
		private readonly CityAggregate _brotherCityIdAggregate;
	
		public CreateCityValidator(
			CityAggregate brotherCityIdAggregate)
		{
			_brotherCityIdAggregate = brotherCityIdAggregate;
	
		}
		public ValidationResult Validate(CreateCity commit)
		{
			if (commit.BrotherCityId != Guid.Empty)
			if (!_brotherCityIdAggregate.ById.ContainsKey(commit.BrotherCityId))
				return new ValidationResult(
					"Wrong BrotherCityId: " + commit.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

