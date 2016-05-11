using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapAndSpatialTypesDemo.Services
{
    public class DbGeographyModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valore = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            try
            {
                return DbGeography.FromText(valore.AttemptedValue, 4326);
            }
            catch
            {
                return null;
            }
        }
    }
}