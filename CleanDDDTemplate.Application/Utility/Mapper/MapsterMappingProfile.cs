using CleanDDDTemplate.Application.Dtos.Demo;
using CleanDDDTemplate.Domain.Models;
using Mapster;

namespace CleanDDDTemplate.Application.Utility.Mapper
{
    public class MapsterMappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region Demo

            config.NewConfig<DemoModel, DtoDemo>().PreserveReference(true);

            #endregion
        }
    }
}