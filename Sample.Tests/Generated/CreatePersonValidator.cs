using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class CreatePersonValidator : IValidator<CreatePersonCommand>
    {
		private readonly CityAggregate _cityIdAggregate;
		private readonly CityAggregate _favoriteCityIdAggregate;
	
		public CreatePersonValidator(
			CityAggregate cityIdAggregate, CityAggregate favoriteCityIdAggregate)
		{
			_cityIdAggregate = cityIdAggregate;
			_favoriteCityIdAggregate = favoriteCityIdAggregate;
	
		}
		public ValidationResult Validate(CreatePersonCommand command)
		{
			if (_cityIdAggregate.Get(command.CityId) == null)
				return new ValidationResult(
					"Wrong CityId: " + command.CityId);
			if (command.FavoriteCityId != Guid.Empty)
			if (_favoriteCityIdAggregate.Get(command.FavoriteCityId) == null)
				return new ValidationResult(
					"Wrong FavoriteCityId: " + command.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

