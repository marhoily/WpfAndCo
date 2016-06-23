using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class UpdatePersonValidator : IValidator<UpdatePersonCommand>
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
		public ValidationResult Validate(UpdatePersonCommand command)
		{
			PersonRow row;
			if (!_personAggregate.ById.TryGetValue(command.Id, out row))
				return new ValidationResult("Did not find Person to be updated: " + command.Id);
			if (row.RowVersion != command.RowVersion)
				return new ValidationResult($"Can't update object v.{row.RowVersion} with command v.{command.RowVersion}");
				
			if (!_cityIdAggregate.ById.ContainsKey(command.CityId))
				return new ValidationResult("Wrong CityId: " + command.CityId);
			if (command.FavoriteCityId != Guid.Empty)
			if (!_favoriteCityIdAggregate.ById.ContainsKey(command.FavoriteCityId))
				return new ValidationResult("Wrong FavoriteCityId: " + command.FavoriteCityId);
		
			return ValidationResult.Success;
		}
    }
}

