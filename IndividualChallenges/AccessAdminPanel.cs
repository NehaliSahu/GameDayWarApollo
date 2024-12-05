private string GetAdminUsername()
{
    return Environment.GetEnvironmentVariable("ADMIN_USERNAME");
}

public void AccessAdminPanel(string username)
{
    string adminUsername = GetAdminUsername();

    if (username == adminUsername)
    {
        Console.WriteLine("Access to Admin Panel Granted!");
    }
    else
    {
        Console.WriteLine("Access Denied.");
    }
}
