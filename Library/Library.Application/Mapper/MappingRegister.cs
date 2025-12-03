using Library.Application.Contracts.EditionType;
using Library.Domain.Entities;
using Mapster;

namespace Library.Application.Mapper;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EditionType, EditionTypeDto>();
    }
}