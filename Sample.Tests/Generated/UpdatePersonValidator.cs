using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdatePersonValidator
    {
		private readonly PersonAggregate _PersonAggregate;
		private readonly CityAggregate _CityAggregate;
	
		public UpdatePersonValidator(
			PersonAggregate PersonAggregate
			,CityAggregate CityAggregate
)
		{
			_PersonAggregate = PersonAggregate;
			_CityAggregate = CityAggregate;
	
		}
		public ValidationResult Validate(UpdatePerson commit)
		{
			if (!_PersonAggregate.ById.ContainsKey(commit.Id))
				return new ValidationResult("Did not find Person to be updated: " + commit.Id);
			if (!_CityAggregate.ById.ContainsKey(commit.CityId))
				return new ValidationResult("Wrong City: " + commit.CityId);
		
			return ValidationResult.Success;
		}
    }
}

