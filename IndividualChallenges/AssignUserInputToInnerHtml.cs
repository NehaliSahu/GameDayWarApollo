
public void AssignUserInputToInnerHtml(string userInput)
{
    // Use a sanitization library or built-in method to sanitize user input
    string sanitizedInput = System.Web.HttpUtility.HtmlEncode(userInput);

    var res = new System.Web.UI.HtmlControls.HtmlGenericControl();
    res.InnerHtml = sanitizedInput; // Safe after sanitization
    Console.WriteLine("Set InnerHtml to: " + sanitizedInput);
}
