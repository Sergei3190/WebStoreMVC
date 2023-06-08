using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.ViewModels
{
    public class EmployeeViewModel : IValidatableObject
    {
        public EmployeeViewModel()
        {
            LastName = null!;
            FirstName = null!;
        }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 10 символов")]
        [RegularExpression("([А-ЯЁ][а-яЁ]+|[A-Z][a-z]+)", ErrorMessage = "Неверный формат. Либо все русские, либо все латинские символы. С заглавной буквы")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 10 символов")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(10, ErrorMessage = "Длина строки должна быть до 10 символов")]
        public string? MiddleName { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Возраст должен быть в диапазоне от 18 до 80 лет")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == "Иван" && LastName == "Иванов" && MiddleName == "Иванович")
            {
                return new []
                {
                    new ValidationResult("Нельзя создавать сотрудника с таким ФИО", new []
                    {
                        nameof(FirstName),
                        nameof(LastName),
                        nameof(MiddleName),
                    })
                };
            }

            return new [] { ValidationResult.Success }!;
        }
    }
}
