using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class CreatePersonValidator : IValidator<CreatePersonComand>
    {
		private readonly CityAggregate _cityIdAggregate;
		private readonly CityAggregate _favoriteCityIdAggregate;
	
		public CreatePersonValidator(
			CityAggregate cityIdAggregate, CityAggregate favoriteCityIdAggregate)
		{
			_cityIdAggregate = cityIdAggregate;
			_favoriteCityIdAggregate = favoriteCityIdAggregate;
	
		}
		public ValidationResult Validate(CreatePersonComand comand)
		{
			if (!_cityIdAggregate.ById.ContainsKey(comand.CityId))
				return new ValidationResult(
					"Wrong CityId: " + comand.CityId);
			if (comand.FavoriteCityId != Guid.Empty)
			if (!_favoriteCityIdAggregate.ById.ContainsKey(comand.FavoriteCityId))
				return new ValidationResult(
					"Wrong FavoriteCityId: " + comand.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

