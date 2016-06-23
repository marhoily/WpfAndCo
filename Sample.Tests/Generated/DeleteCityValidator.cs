using System.ComponentModel.DataAnnotations;
using System.Linq;
using Generator;

namespace Sample.Generated {
    [IoC]
    public sealed class DeleteCityValidator : IValidator<DeleteCityCommand>
    {
		private readonly PersonAggregate _personAggregate;
		private readonly CityAggregate _cityAggregate;
		public DeleteCityValidator(PersonAggregate personAggregate, CityAggregate cityAggregate)
		{
			_personAggregate = personAggregate;
			_cityAggregate = cityAggregate;
		}
		public ValidationResult Validate(DeleteCityCommand command)
		{
			var row = _cityAggregate.Get(command.Id);
			if (row == null)
				return new ValidationResult("Did not find City to be deleted: " + command.Id);
			if (row.RowVersion != command.RowVersion)
				return new ValidationResult($"Can't delete object v.{row.RowVersion} with command v.{command.RowVersion}");
			if (_personAggregate.Any(p => p.CityId == command.Id))
				return new ValidationResult(
					$"Can not delete City {command.Id} " +
					$"because other objects depend on it: {_personAggregate.Where(p => p.CityId == command.Id).Join(p => p.Id)}");
			return ValidationResult.Success;
		}
    }
}

