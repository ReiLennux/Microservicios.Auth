using System.ComponentModel.DataAnnotations;

namespace KnowCloud.Utility
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        private override ValidationResult IsValid(object value, ValidationResult validationResult)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extensions = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(extensions))
                {
                    return new ValidationResult("This extension are no allowed");
                }
            }
            return ValidationResult.Success;

        }
    }
}
