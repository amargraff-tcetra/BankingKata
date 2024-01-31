namespace BankingKata.Services
{
    public static class AuthenticationService
    {
        public static async Task<bool> CredentialsAreValid(string username, string password, CancellationToken ct)
        {
            //TODO: Search user credentials for match
            if (username == string.Empty && password == string.Empty)
            {
                return await Task.Run(() => true);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }
    }
}
