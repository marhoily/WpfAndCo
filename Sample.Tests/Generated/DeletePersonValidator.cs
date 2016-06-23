using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class DeletePersonValidator
    {
		private readonly PersonAggregate _PersonAggregate;

		public DeletePersonValidator(PersonAggregate PersonAggregate)
		{
			_PersonAggregate = PersonAggregate;
		}
		public ValidationResult Validate(DeletePerson commit)
		{
			PersonRow row;
			if (!_PersonAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find Person to be deleted: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't delete object v.{row.RowVersion} with commit v.{commit.RowVersion}");

			return ValidationResult.Success;
		}
    }
}

