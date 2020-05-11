using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly MarketXContext _context;
        private readonly IMapper _mapper;

        public PropertyService(MarketXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PropertyValue> AddPropertyValueAsync(int propertyId, PropertyValue propertyValue)
        {
            var dbPropertyValue = _mapper.Map<DAL.Entities.PropertyValue>(propertyValue);
            dbPropertyValue.PropertyID = propertyId;
            _context.PropertyValues.Add(dbPropertyValue);
            await _context.SaveChangesAsync();
            return _mapper.Map<PropertyValue>(dbPropertyValue);
        }

        public async Task<Property> GetProperty(int propertyId)
        {
            var dbProperty = await _context.Properties
                                .Include(p => p.PropertyValues)
                                .FirstOrDefaultAsync(p => p.Id == propertyId);
            return _mapper.Map<Property>(dbProperty);
        }

        public async Task<Property> GetPropertyByName(string propertyName)
        {
            var dbProperty = await _context.Properties
                                .Include(p => p.PropertyValues)
                                .FirstOrDefaultAsync(p => p.Name == propertyName);
            return _mapper.Map<Property>(dbProperty);
        }

        public async Task UpdatePropertyAsync(int propertyId, Property property)
        {
            var dbProperty = await _context.Properties
                .Include(p => p.PropertyValues)
                .FirstOrDefaultAsync(p => p.Id == propertyId);
            dbProperty.Name = property.Name!;
            dbProperty.isImportant = property.IsImportant;
            dbProperty.ValueType = property.ValueType;
            if(property.ValueType != DAL.Entities.PropertyValueType.SelectableFromList)
            {
                foreach (var propValue in dbProperty.PropertyValues)
                {
                    _context.PropertyValues.Remove(propValue);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}