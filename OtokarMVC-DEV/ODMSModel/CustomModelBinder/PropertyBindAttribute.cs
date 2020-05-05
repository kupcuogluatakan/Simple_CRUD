using System;

namespace ODMSModel.CustomModelBinder
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class PropertyBindAttribute : Attribute
    {
        //public abstract bool BindProperty(ControllerContext controllerContext,
        //ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor);
    }
}
