using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIProdutos.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado",
                Detail = "Ocorreu um erro inesperado na solitação",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

            switch(context.Exception)
            {
                case ArgumentNullException:
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status501NotImplemented
                    };
                    break;
                case DivideByZeroException:
                    problem.Status = StatusCodes.Status400BadRequest;
                    problem.Detail = "Erro de divisão por zero";
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            }
        }
    }
}
