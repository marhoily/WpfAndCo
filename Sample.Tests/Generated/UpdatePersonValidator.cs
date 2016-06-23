using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdatePersonValidator : IValidator<UpdatePerson>
    {
		private readonly PersonAggregate _personAggregate;
		private readonly CityAggregate _cityIdAggregate;
		private readonly CityAggregate _favoriteCityIdAggregate;
	
		public UpdatePersonValidator(
			PersonAggregate personAggregate
			,CityAggregate cityIdAggregate
			,CityAggregate favoriteCityIdAggregate
)
		{
			_personAggregate = personAggregate;
			_cityIdAggregate = cityIdAggregate;
			_favoriteCityIdAggregate = favoriteCityIdAggregate;
	
		}
		public ValidationResult Validate(UpdatePerson commit)
		{
			PersonRow row;
			if (!_personAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find Person to be updated: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't update object v.{row.RowVersion} with commit v.{commit.RowVersion}");
				
			if (!_cityIdAggregate.ById.ContainsKey(commit.CityId))
				return new ValidationResult("Wrong CityId: " + commit.CityId);
			if (commit.FavoriteCityId != Guid.Empty)
			if (!_favoriteCityIdAggregate.ById.ContainsKey(commit.FavoriteCityId))
				return new ValidationResult("Wrong FavoriteCityId: " + commit.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

