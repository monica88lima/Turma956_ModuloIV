using APIProdutos.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIProdutos.Filters
{
    public class GaranteProdutoExisteActionFilter : ActionFilterAttribute
    {
        public IProdutoService _produtoService;
        public GaranteProdutoExisteActionFilter(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            long idProduto = (long)context.ActionArguments["id"];

            if (_produtoService.ConsultarProduto(idProduto) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
