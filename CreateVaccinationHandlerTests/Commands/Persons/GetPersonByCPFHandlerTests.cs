using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Person.GetPersonByCPF;
using VaccinationCard.Application.Common.Mappings.Person;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enum;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCardHandlerTests.Commands.Persons;

public class GetPersonByCPFHandlerTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<ILogger<GetPersonByCPFHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public GetPersonByCPFHandlerTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _loggerMock = new Mock<ILogger<GetPersonByCPFHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PersonProfile());
        }, loggerFactory);

        _mapper = configuration.CreateMapper();
    }

    private GetPersonByCPFHandler SUT() => new(
     _personRepositoryMock.Object,
     _loggerMock.Object,
     _mapper
     );


    [Fact]
    public async Task Should_throw_NotFound_when_person_not_found()
    {
        var cpf = "143.998.860-99";
        var cmd = new GetPersonByCPFCommand { CPF = cpf};

        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(cpf, It.IsAny<CancellationToken>()))
             .ReturnsAsync((Person?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _personRepositoryMock.Verify(r => r.GetPersonByCPFAsync(cpf, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_return_mapped_result_on_success()
    {
        var cpf = "143.998.860-99";
        var id = Guid.NewGuid();
        var entity = new Person
        {
            Id = id,
            Name = "Ana",
            CPF = cpf,
            Sex = Sex.Feminine,
            IsAdmin = true,
            DateOfBirth = new DateTime(1995, 5, 20)
        };

        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(cpf, It.IsAny<CancellationToken>()))
             .ReturnsAsync(entity);

        var cmd = new GetPersonByCPFCommand { CPF = cpf};

        var res = await SUT().Handle(cmd, default);

        res.Should().NotBeNull();
        res.Person.PersonId.Should().Be(id);
        res.Person.Name.Should().Be("Ana");
        res.Person.CPF.Should().Be(cpf);
        res.Person.Sex.Should().Be(Sex.Feminine.ToString());
        res.Person.IsAdmin.Should().BeTrue();
        res.Person.DateOfBirth.Should().Be(entity.DateOfBirth);
    }

}
