using Library.Application.Contracts.EditionType;
using Library.Application.Contracts.Publisher;
using Library.Application.Contracts.Reader;
using Library.Application.Contracts.Book;
using Library.Domain.Entities;
using Mapster;

namespace Library.Application.Mapper;

/// <summary>
/// Регистрация конфигураций Mapster для маппинга сущностей на DTO
/// </summary>
public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EditionType, EditionTypeDto>();
        config.NewConfig<Publisher, PublisherDto>();
        config.NewConfig<Reader, ReaderDto>();
        config.NewConfig<Book, BookDto>();
    }
}
