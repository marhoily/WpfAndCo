using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class CreateCityValidator : IValidator<CreateCityComand>
    {
		private readonly CityAggregate _brotherCityIdAggregate;
	
		public CreateCityValidator(
			CityAggregate brotherCityIdAggregate)
		{
			_brotherCityIdAggregate = brotherCityIdAggregate;
	
		}
		public ValidationResult Validate(CreateCityComand comand)
		{
			if (comand.BrotherCityId != Guid.Empty)
			if (!_brotherCityIdAggregate.ById.ContainsKey(comand.BrotherCityId))
				return new ValidationResult(
					"Wrong BrotherCityId: " + comand.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

