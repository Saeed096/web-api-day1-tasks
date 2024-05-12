using System.ComponentModel.DataAnnotations;

namespace task1.DTO
{
    public class RegisterDTO
    {
        public string name { get; set; }

        [DataType(DataType.Password) , MinLength(3)]
        public string password { get; set; }

        [DataType(DataType.PhoneNumber)] 
        public string phoneNumber { get; set; }
        public string address { get; set; }
    }
}
