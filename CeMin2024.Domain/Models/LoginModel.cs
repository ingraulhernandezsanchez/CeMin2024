using System.ComponentModel.DataAnnotations;

namespace CeMin2024.Domain.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MinLength(3, ErrorMessage = "El usuario debe tener al menos 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El usuario no puede exceder los 50 caracteres")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public string SelectedRole { get; set; } = string.Empty;

        // Constructor
        public LoginModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            SelectedRole = string.Empty;
        }

        // Constructor con parámetros
        public LoginModel(string username, string password, string selectedRole)
        {
            Username = username;
            Password = password;
            SelectedRole = selectedRole;
        }

        // Método para validar el modelo
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username)
                   && !string.IsNullOrWhiteSpace(Password)
                   && !string.IsNullOrWhiteSpace(SelectedRole);
        }

        // Método para limpiar datos sensibles
        public void ClearSensitiveData()
        {
            Password = string.Empty;
        }
    }
}