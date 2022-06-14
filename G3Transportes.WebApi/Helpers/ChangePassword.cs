using System;
namespace G3Transportes.WebApi.Helpers
{
    public class ChangePassword
    {
        public ChangePassword()
        {
        }

        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }

    }
}
