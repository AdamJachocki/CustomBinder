using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApi.Models;

namespace WebApi.Binders
{
    public class SimplePostBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var paramName = bindingContext.ModelName;
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(paramName);

            if(value == ValueProviderResult.None)
                return Task.CompletedTask;

            var strValue = value.First();
            if(string.IsNullOrWhiteSpace(strValue))
                return Task.CompletedTask;

            try
            {
                SimplePost result = ConvertFromString(strValue);
                bindingContext.Result = ModelBindingResult.Success(result);
            }catch(Exception ex)
            {
                bindingContext.ModelState.AddModelError(paramName, $"Could not convert from specified data, error: {ex.Message}");
            }
            
            return Task.CompletedTask;
        }

        private SimplePost ConvertFromString(string data)
        {
            Dictionary<string, string> keyValues = new();

            string[] fields = data.Split(';');
            foreach(var field in fields)
            {
                string[] kv = field.Split(':');
                keyValues[kv[0]] = kv[1];   
            }

            SimplePost result = new SimplePost();
            result.Id = int.Parse(keyValues["id"]);
            result.Title = keyValues["title"];

            return result;
        }
    }
}
