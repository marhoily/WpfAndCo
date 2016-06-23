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
		public ValidationResult Validate(DeletePersonCommand command)
		{
			PersonRow row;
			if (!_personAggregate.ById.TryGetValue(command.Id, out row))
				return new ValidationResult("Did not find Person to be deleted: " + command.Id);
			if (row.RowVersion != command.RowVersion)
				return new ValidationResult($"Can't delete object v.{row.RowVersion} with command v.{command.RowVersion}");

			return ValidationResult.Success;
		}
    }
}

