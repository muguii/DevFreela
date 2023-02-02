using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // ANTES
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(errors);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // DEPOIS
        }
    }
}
