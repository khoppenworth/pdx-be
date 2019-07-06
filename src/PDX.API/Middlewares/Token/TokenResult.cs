namespace PDX.API.Middlewares.Token
{
    public class TokenResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}