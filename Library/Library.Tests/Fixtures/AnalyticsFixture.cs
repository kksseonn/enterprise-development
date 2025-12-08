using Library.Application.Service;
using Library.Domain;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.Fixtures;

/// <summary>
/// Фикстура для инициализации аналитического сервиса с тестовыми данными
/// </summary>
public class AnalyticsFixture
{
    /// <summary>
    /// Сервис аналитики библиотеки
    /// </summary>
    public AnalyticsService Service { get; }

    /// <summary>
    /// Фикстура с тестовыми данными
    /// </summary>
    public DataFixture Data { get; }

    /// <summary>
    /// Создает экземпляр фикстуры и настраивает мок-репозитории для аналитического сервиса
    /// </summary>
    public AnalyticsFixture()
    {
        Data = new DataFixture();

        var editionRepositoryMock = new Mock<IRepository<EditionType>>();
        var publisherRepositoryMock = new Mock<IRepository<Publisher>>();
        var readerRepositoryMock = new Mock<IRepository<Reader>>();
        var bookRepositoryMock = new Mock<IRepository<Book>>();
        var borrowRepositoryMock = new Mock<IRepository<Borrow>>();

        editionRepositoryMock
            .Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Data.EditionTypes);

        publisherRepositoryMock
            .Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Data.Publishers);

        readerRepositoryMock
            .Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Data.Readers);

        bookRepositoryMock
            .Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Data.Books);

        borrowRepositoryMock
            .Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Data.Borrows);

        Service = new AnalyticsService(
            borrowRepositoryMock.Object,
            bookRepositoryMock.Object,
            readerRepositoryMock.Object,
            publisherRepositoryMock.Object
        );
    }
}