﻿using Generics.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace Generics.ExtensionMethods;

public class GenericControllerRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType.IsGenericType)
        {
            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var customNameAttribute = genericType.GetCustomAttribute<GenericControllerRouteAttribute>();

            if (customNameAttribute?.Route != null)
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(customNameAttribute.Route)),
                });
            }
        }
    }
}
