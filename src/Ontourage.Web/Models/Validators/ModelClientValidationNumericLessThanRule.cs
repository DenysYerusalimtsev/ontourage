using System.Xml;

namespace Ontourage.Web.Models.Validators
{
    public class ModelClientValidationNumericLessThanRule : ModelClientValidationRule
    {
        public ModelClientValidationNumericLessThanRule(string errorMessage, object other, bool allowEquality)
        {
            ErrorMessage = errorMessage;
            ValidationType = "numericlessthan";
            ValidationParameters["other"] = other;
            ValidationParameters["allowequality"] = allowEquality;
        }
    }
}
