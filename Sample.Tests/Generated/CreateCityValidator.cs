using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class CreateCityValidator : IValidator<CreateCityCommand>
    {
		private readonly CityAggregate _brotherCityIdAggregate;
	
		public CreateCityValidator(
			CityAggregate brotherCityIdAggregate)
		{
			_brotherCityIdAggregate = brotherCityIdAggregate;
	
		}
		public ValidationResult Validate(CreateCityCommand command)
		{
			if (command.BrotherCityId != Guid.Empty)
			if (!_brotherCityIdAggregate.ById.ContainsKey(command.BrotherCityId))
				return new ValidationResult(
					"Wrong BrotherCityId: " + command.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

