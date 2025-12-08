using Library.Application.Contracts.Book;
using Library.Application.Contracts.Borrow;
using Library.Application.Contracts.EditionType;
using Library.Application.Contracts.Publisher;
using Library.Application.Contracts.Reader;
using Library.Domain.Entities;
using Mapster;

namespace Library.Application.Mapper;

/// <summary>
/// Регистрация маппингов между доменными сущностями и DTO
/// </summary>
public class MappingRegister : IRegister
{
    /// <summary>
    /// Регистрирует конфигурацию маппинга
    /// </summary>
    /// <param name="config">Конфигурация Mapster</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EditionType, EditionTypeDto>();
        config.NewConfig<Publisher, PublisherDto>();
        config.NewConfig<Reader, ReaderDto>();
        config.NewConfig<Book, BookDto>();
        config.NewConfig<Borrow, BorrowDto>();
    }
}