using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdatePersonValidator
    {
		private readonly PersonAggregate _PersonAggregate;
		private readonly CityAggregate _CityIdAggregate;
		private readonly CityAggregate _FavoriteCityIdAggregate;
	
		public UpdatePersonValidator(
			PersonAggregate PersonAggregate
			,CityAggregate CityIdAggregate
			,CityAggregate FavoriteCityIdAggregate
)
		{
			_PersonAggregate = PersonAggregate;
			_CityIdAggregate = CityIdAggregate;
			_FavoriteCityIdAggregate = FavoriteCityIdAggregate;
	
		}
		public ValidationResult Validate(UpdatePerson commit)
		{
			if (!_PersonAggregate.ById.ContainsKey(commit.Id))
				return new ValidationResult("Did not find Person to be updated: " + commit.Id);
			if (!_CityIdAggregate.ById.ContainsKey(commit.CityId))
				return new ValidationResult("Wrong CityId: " + commit.CityId);
			if (commit.FavoriteCityId != Guid.Empty)
			if (!_FavoriteCityIdAggregate.ById.ContainsKey(commit.FavoriteCityId))
				return new ValidationResult("Wrong FavoriteCityId: " + commit.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

