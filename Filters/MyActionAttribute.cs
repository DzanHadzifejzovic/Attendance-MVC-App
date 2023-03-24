using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FIsrtMVCapp.Filters
{
    public class MyActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            System.Console.WriteLine("Finished action");
            //context.Result = new ContentResult() { Content = "Hello" };
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            System.Console.WriteLine("Started action");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            System.Console.WriteLine("Finished with result");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            System.Console.WriteLine("Started with result");
        }
    }
}
