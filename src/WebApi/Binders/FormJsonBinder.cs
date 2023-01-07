using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using WebApi.Models;

namespace WebApi.Binders
{
    public class FormJsonBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var fieldName = bindingContext.FieldName;
            var fieldValue = bindingContext.ValueProvider.GetValue(fieldName);
            if (fieldValue == ValueProviderResult.None)
                return Task.CompletedTask;

            try
            {
                Type modelType = GetTypeForModel(bindingContext);
                var result = ConvertFromJson(modelType, fieldValue.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(fieldName, $"Could not convert from specified data, error: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        private Type GetTypeForModel(ModelBindingContext context)
        {
            Type modelType = context.ModelType;
            Type[] genericArgs = modelType.GenericTypeArguments;
            if (genericArgs.Length == 0)
                throw new InvalidOperationException("Invalid class! Expected generic type!");

            return genericArgs[0];
        }

        private object ConvertFromJson(Type modelType, string jsonData)
        {
            Type outputType = typeof(FormJsonData<>).MakeGenericType(modelType);

            JsonSerializerOptions opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var model = JsonSerializer.Deserialize(jsonData, modelType, opt);

            var result = Activator.CreateInstance(outputType);
            var modelProp = outputType.GetProperty("Model");
            modelProp.SetValue(result, model);

            return result;
        }
    }

}
