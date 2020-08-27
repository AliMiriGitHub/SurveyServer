namespace SurveySystem.Models.Auth
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string token)
        {
            Token = token;
        }
        public string Token { get; set; }
    }
}