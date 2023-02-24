using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;

namespace MobvenSozluk.API.Fİlters
{
    public class ValidateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
           if(!context.ModelState.IsValid)
            {
                var errors=context.ModelState.Values.SelectMany(x=>x.Errors).Select(x=> x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
                

            }
        }
    }
}
