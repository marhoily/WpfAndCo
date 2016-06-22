using System.ComponentModel.DataAnnotations;

namespace Sample.Generated {
    public sealed class UpdatePersonValidator
    {
		private readonly CityAggregate _CityAggregate;
	
		public UpdatePersonValidator(
			CityAggregate CityAggregate)
		{
			_CityAggregate = CityAggregate;
	
		}
		public ValidationResult Validate(UpdatePerson commit)
		{
			if (!_CityAggregate.ById.ContainsKey(commit.City))
				return new ValidationResult("Wrong City: " + commit.City);
		
			return ValidationResult.Success;
		}
    }
}

