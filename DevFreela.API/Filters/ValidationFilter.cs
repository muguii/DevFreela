using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errorMessages = context.ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(errorMessages);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
