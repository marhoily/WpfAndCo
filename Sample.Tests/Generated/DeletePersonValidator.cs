using System.ComponentModel.DataAnnotations;
using System.Linq;
using Generator;

namespace Sample.Generated {
    [IoC]
    public sealed class DeletePersonValidator : IValidator<DeletePersonCommand>
    {
		private readonly PersonAggregate _personAggregate;
		public DeletePersonValidator(PersonAggregate personAggregate)
		{
			_personAggregate = personAggregate;
		}
		public ValidationResult Validate(DeletePersonCommand commit)
		{
			PersonRow row;
			if (!_personAggregate.ById.TryGetValue(commit.Id, out row))
				return new ValidationResult("Did not find Person to be deleted: " + commit.Id);
			if (row.RowVersion != commit.RowVersion)
				return new ValidationResult($"Can't delete object v.{row.RowVersion} with commit v.{commit.RowVersion}");

			return ValidationResult.Success;
		}
    }
}

