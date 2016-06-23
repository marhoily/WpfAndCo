using System.ComponentModel.DataAnnotations;

namespace Sample
{
    public interface IValidator<in T>
    {
        ValidationResult Validate(T commit);
    }
}