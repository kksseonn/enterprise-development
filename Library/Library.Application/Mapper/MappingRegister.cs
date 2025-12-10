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

        config.NewConfig<Book, BookDto>()
            .Map(dest => dest.EditionTypeName, src => src.EditionType!.Type)
            .Map(dest => dest.PublisherName, src => src.Publisher!.Name);

        config.NewConfig<Borrow, BorrowDto>()
            .Map(dest => dest.BookTitle, src => src.Book!.Title)
            .Map(dest => dest.ReaderFullName, src => $"{src.Reader!.Surname} {src.Reader!.Name} {src.Reader!.Patronymic}");

        config.NewConfig<EditionTypeCrudDto, EditionType>()
            .Ignore(dest => dest.Books!);

        config.NewConfig<PublisherCrudDto, Publisher>()
            .Ignore(dest => dest.Books!);

        config.NewConfig<ReaderCrudDto, Reader>()
            .Ignore(dest => dest.Borrows!)
            .Ignore(dest => dest.RegistrationDate);

        config.NewConfig<BookCrudDto, Book>()
            .Ignore(dest => dest.EditionType!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.Borrows!);

        config.NewConfig<BorrowCrudDto, Borrow>()
            .Ignore(dest => dest.Book!)
            .Ignore(dest => dest.Reader!)
            .Ignore(dest => dest.DueDate);
    }
}