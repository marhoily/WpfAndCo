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
			PersonRow row;
			if (!_PersonAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find Person to be updated: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't update object v.{row.RowVersion} with commit v.{commit.RowVersion}");
				
			if (!_CityIdAggregate.ById.ContainsKey(commit.CityId))
				return new ValidationResult("Wrong CityId: " + commit.CityId);
			if (commit.FavoriteCityId != Guid.Empty)
			if (!_FavoriteCityIdAggregate.ById.ContainsKey(commit.FavoriteCityId))
				return new ValidationResult("Wrong FavoriteCityId: " + commit.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

