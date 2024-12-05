public void AuthenticateUser(string password)
{
    var hashedPassword = _configuration["AdminSettings:HashedPassword"];
    if (VerifyPassword(password, hashedPassword))
    {
        GrantAccess();
    }
    else
    {
        Console.WriteLine("Access Denied.");
    }
}

private void GrantAccess()
{
    Console.WriteLine("Access Granted!");
}


private bool VerifyPassword(string password, string hashedPassword)
{
    using (var sha256 = SHA256.Create())
    {
        var hashedInputPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return hashedInputPassword == hashedPassword;
    }
}
