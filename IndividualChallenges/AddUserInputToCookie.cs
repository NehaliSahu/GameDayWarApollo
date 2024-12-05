public void AddUserInputToCookie(string userInput)
{
    if (string.IsNullOrWhiteSpace(userInput))
    {
        throw new ArgumentException("User input cannot be null or empty.", nameof(userInput));
    }

    // Use HttpUtility to encode the user input to prevent XSS attacks
    string encodedUserInput = HttpUtility.UrlEncode(userInput);
    Response.Cookies.Add(new HttpCookie("SessionID", encodedUserInput)); 
    Console.WriteLine("Cookie added with user input: " + encodedUserInput);
}
