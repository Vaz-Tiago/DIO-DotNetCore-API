using DIO.CursosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace DIO.CursosAPI.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validaCampoViewmModel = new ValidaCamposViewModelOutput(context.ModelState
                    .SelectMany(sm => sm.Value.Errors)
                    .Select(s => s.ErrorMessage)
                );
                context.Result = new BadRequestObjectResult(validaCampoViewmModel);
            }
        }
    }
}
