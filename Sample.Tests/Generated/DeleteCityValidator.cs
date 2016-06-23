using System.ComponentModel.DataAnnotations;
using System.Linq;
using Generator;

namespace Sample.Generated {
    public sealed class DeleteCityValidator : IValidator<DeleteCity>
    {
		private readonly PersonAggregate _personAggregate;
		private readonly CityAggregate _cityAggregate;
		public DeleteCityValidator(PersonAggregate personAggregate, CityAggregate cityAggregate)
		{
			_personAggregate = personAggregate;
			_cityAggregate = cityAggregate;
		}
		public ValidationResult Validate(DeleteCity commit)
		{
			CityRow row;
			if (!_cityAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find City to be deleted: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't delete object v.{row.RowVersion} with commit v.{commit.RowVersion}");

			if (_personAggregate.ById.Values
				.Any(p => p.CityId == commit.Id))
				return new ValidationResult(
					$"Can not delete City {commit.Id} " +
					$"because other objects depend on it: {_personAggregate.ById.Values.Where(p => p.CityId == commit.Id).Join(p => p.Id)}");
			return ValidationResult.Success;
		}
    }
}

