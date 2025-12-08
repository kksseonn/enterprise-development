using Library.Domain.Entities;
using Library.Domain;
using Library.Application.Service;
using Moq;


namespace Library.Tests.Fixtures;

public class AnalyticsFixture
{
    public AnalyticsService Service { get; }

    public DataFixture Data { get; }

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