using System.ComponentModel.DataAnnotations;
using System.Linq;
using Generator;

namespace Sample.Generated {
    public sealed class DeleteCityValidator
    {
		private readonly PersonAggregate _PersonAggregate;
		private readonly CityAggregate _CityAggregate;

		public DeleteCityValidator(PersonAggregate PersonAggregate, CityAggregate CityAggregate)
		{
			_PersonAggregate = PersonAggregate;
			_CityAggregate = CityAggregate;
		}
		public ValidationResult Validate(DeleteCity commit)
		{
			CityRow row;
			if (!_CityAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find City to be deleted: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't delete object v.{row.RowVersion} with commit v.{commit.RowVersion}");

			if (_PersonAggregate.ById.Values
				.Any(p => p.CityId == commit.Id))
				return new ValidationResult(
					$"Can not delete City {commit.Id} " +
					$"because other objects depend on it: {_PersonAggregate.ById.Values.Where(p => p.CityId == commit.Id).Join(p => p.Id)}");
			if (_PersonAggregate.ById.Values
				.Any(p => p.FavoriteCityId == commit.Id))
				return new ValidationResult(
					$"Can not delete City {commit.Id} " +
					$"because other objects depend on it: {_PersonAggregate.ById.Values.Where(p => p.FavoriteCityId == commit.Id).Join(p => p.Id)}");
			if (_CityAggregate.ById.Values
				.Any(p => p.BrotherCityId == commit.Id))
				return new ValidationResult(
					$"Can not delete City {commit.Id} " +
					$"because other objects depend on it: {_CityAggregate.ById.Values.Where(p => p.BrotherCityId == commit.Id).Join(p => p.Id)}");
			return ValidationResult.Success;
		}
    }
}

