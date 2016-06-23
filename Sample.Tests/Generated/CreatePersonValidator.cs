using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class CreatePersonValidator : IValidator<CreatePerson>
    {
		private readonly CityAggregate _cityIdAggregate;
		private readonly CityAggregate _favoriteCityIdAggregate;
	
		public CreatePersonValidator(
			CityAggregate cityIdAggregate, CityAggregate favoriteCityIdAggregate)
		{
			_cityIdAggregate = cityIdAggregate;
			_favoriteCityIdAggregate = favoriteCityIdAggregate;
	
		}
		public ValidationResult Validate(CreatePerson commit)
		{
			if (!_cityIdAggregate.ById.ContainsKey(commit.CityId))
				return new ValidationResult(
					"Wrong CityId: " + commit.CityId);
			if (commit.FavoriteCityId != Guid.Empty)
			if (!_favoriteCityIdAggregate.ById.ContainsKey(commit.FavoriteCityId))
				return new ValidationResult(
					"Wrong FavoriteCityId: " + commit.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

