using Microsoft.AspNetCore.Mvc;

public class TestController : Controller
{
    public IActionResult TriggerError()
    {
        throw new Exception("This is a test exception");
    }
}
