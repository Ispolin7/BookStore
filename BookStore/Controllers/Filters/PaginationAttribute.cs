using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers.Filters
{
    public class PaginationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments;
            if (!param.ContainsKey("page")
                || !param.ContainsKey("count")
                || (int)param["page"] < 1
                || (int)param["count"] < 1)
            {
                context.Result = new BadRequestObjectResult(new { error = "incorrect get param" });
            }
        }       
    }
}
