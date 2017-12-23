using Store.DTO.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace Store.WebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        protected OperationResult ToOperationResult<T>(T model)
        {
            var operation = new OperationResult();
            operation.Messages.Add(new OperationMessage { Description = "Complete correctamente los datos", Severity = 0 });
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    operation.Messages.Add(new OperationMessage { Description = validationResult.ErrorMessage, Severity = 0 });
                }
            }

            ModelState.Clear();

            return operation;
        }
    }
}
