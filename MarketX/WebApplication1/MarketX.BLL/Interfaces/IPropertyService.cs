using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface IPropertyService
    {
        Task<Property> GetProperty(int propertyId);
        Task<Property> GetPropertyByName(string propertyName);
        Task<PropertyValue> AddPropertyValueAsync(int propertyId, PropertyValue propertyValue);
        Task UpdatePropertyAsync(int propertyId, Property property);
    }
}
