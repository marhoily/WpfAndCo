using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class CreatePersonValidator : IValidator<CreatePerson>
    {
		private readonly CityAggregate _CityIdAggregate;
		private readonly CityAggregate _FavoriteCityIdAggregate;
	
		public CreatePersonValidator(
			CityAggregate CityIdAggregate, CityAggregate FavoriteCityIdAggregate)
		{
			_CityIdAggregate = CityIdAggregate;
			_FavoriteCityIdAggregate = FavoriteCityIdAggregate;
	
		}
		public ValidationResult Validate(CreatePerson commit)
		{
			if (!_CityIdAggregate.ById.ContainsKey(commit.CityId))
				return new ValidationResult(
					"Wrong CityId: " + commit.CityId);
			if (commit.FavoriteCityId != Guid.Empty)
			if (!_FavoriteCityIdAggregate.ById.ContainsKey(commit.FavoriteCityId))
				return new ValidationResult(
					"Wrong FavoriteCityId: " + commit.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

