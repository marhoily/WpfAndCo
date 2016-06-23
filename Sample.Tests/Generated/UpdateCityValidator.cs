using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    [IoC]
    public sealed class UpdateCityValidator : IValidator<UpdateCityCommand>
    {
		private readonly CityAggregate _cityAggregate;
		private readonly CityAggregate _brotherCityIdAggregate;
	
		public UpdateCityValidator(
			CityAggregate cityAggregate
			,CityAggregate brotherCityIdAggregate
)
		{
			_cityAggregate = cityAggregate;
			_brotherCityIdAggregate = brotherCityIdAggregate;
	
		}
		public ValidationResult Validate(UpdateCityCommand command)
		{
			CityRow row;
			if (!_cityAggregate.ById.TryGetValue(command.Id, out row))
				return new ValidationResult("Did not find City to be updated: " + command.Id);
			if (row.RowVersion != command.RowVersion)
				return new ValidationResult($"Can't update object v.{row.RowVersion} with command v.{command.RowVersion}");
				
			if (command.BrotherCityId != Guid.Empty)
			if (!_brotherCityIdAggregate.ById.ContainsKey(command.BrotherCityId))
				return new ValidationResult("Wrong BrotherCityId: " + command.BrotherCityId);
		
			return ValidationResult.Success;
		}
    }
}

