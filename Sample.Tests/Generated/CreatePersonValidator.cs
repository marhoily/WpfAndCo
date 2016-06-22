using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class CreatePersonValidator
    {
		private readonly CreateCityAggregate _createCityAggregate;
	
		public CreatePersonValidator(
			CreateCityAggregate createCityAggregate)
		{
			_createCityAggregate = createCityAggregate;
	
		}
		public ValidationResult Validate(CreatePerson commit)
		{
			if (!_createCityAggregate.ById.ContainsKey(commit.City))
				return new ValidationResult("Wrong City: " + commit.City);
		
			return ValidationResult.Success;
		}
    }
}

