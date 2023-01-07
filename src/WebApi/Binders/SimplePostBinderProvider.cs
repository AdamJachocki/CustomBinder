using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using WebApi.Models;

namespace WebApi.Binders
{
    public class SimplePostBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(SimplePost))
                return new BinderTypeModelBinder(typeof(SimplePostBinder));
            else
                return null;
        }
    }
}
