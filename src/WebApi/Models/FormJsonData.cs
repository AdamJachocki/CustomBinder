using Microsoft.AspNetCore.Mvc;
using WebApi.Binders;

namespace WebApi.Models
{
    [ModelBinder(typeof(FormJsonBinder))]
    public class FormJsonData<T>
        where T: class, new()
    {
        public T Model { get; set; }
    }
}
