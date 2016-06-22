using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class DeletePersonValidator
    {
		private readonly PersonAggregate _PersonAggregate;

		public DeletePersonValidator(
			PersonAggregate PersonAggregate)
		{
			_PersonAggregate = PersonAggregate;
		}
		public ValidationResult Validate(DeletePerson commit)
		{
			if (!_PersonAggregate.ById.ContainsKey(commit.Id))
				return new ValidationResult("Did not find Person to be Deleted: " + commit.Id);
			return ValidationResult.Success;
		}
    }
}

