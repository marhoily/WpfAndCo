using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class CreatePersonValidator
    {
		private readonly CityAggregate _CityAggregate;
	
		public CreatePersonValidator(
			CityAggregate CityAggregate)
		{
			_CityAggregate = CityAggregate;
	
		}
		public ValidationResult Validate(CreatePerson commit)
		{
			if (!_CityAggregate.ById.ContainsKey(commit.City))
				return new ValidationResult("Wrong City: " + commit.City);
		
			return ValidationResult.Success;
		}
    }
}

