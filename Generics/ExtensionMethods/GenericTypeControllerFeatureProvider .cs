using Generics.Controllers;
using Generics.Dtos;
using Generics.Entities;
using Generics.IServices;
using Generics.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Generics.ExtensionMethods;

public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var baseAssembly = typeof(BaseCreateDto).Assembly;

        var createDtos = baseAssembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseCreateDto)) && !t.IsAbstract)
            .ToList();

        var entities = baseAssembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseEntity)) && !t.IsAbstract)
            .ToList();

        var responseDtos = baseAssembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseResponseDto)) && !t.IsAbstract)
            .ToList();

        foreach (var createDto in createDtos)
        {
            var entityType = entities.FirstOrDefault(e => e.Name.Replace("Dto", "") == createDto.Name.Replace("CreateDto", ""));
            var responseDto = responseDtos.FirstOrDefault(r => r.Name.Replace("ResponseDto", "") == createDto.Name.Replace("CreateDto", ""));

            if (entityType != null && responseDto != null)
            {
                feature.Controllers.Add(
                    typeof(GenericController<,,>).MakeGenericType(createDto, entityType, responseDto).GetTypeInfo()
                );
            }
        }
    }
}
