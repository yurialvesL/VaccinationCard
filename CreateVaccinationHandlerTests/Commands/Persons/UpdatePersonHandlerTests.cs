using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Person.UpdatePerson;
using VaccinationCard.Application.Common.Mappings.Person;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCardHandlerTests.Commands.Persons;

public class UpdatePersonHandlerTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<ILogger<UpdatePersonHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public UpdatePersonHandlerTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        _loggerMock = new Mock<ILogger<UpdatePersonHandler>>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PersonProfile());
        }, loggerFactory);

        _mapper = mapperConfig.CreateMapper();
    }

    private UpdatePersonHandler SUT() => new(
     _personRepositoryMock.Object,
     _loggerMock.Object,
     _mapper
     );



    [Fact]
    public async Task Should_throw_NotFound_when_person_with_cpf_not_found()
    {
        // arrange
        var cmd = new UpdatePersonCommand { CPF = "143.998.860-99", IsAdmin = true };

        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(cmd.CPF, It.IsAny<CancellationToken>()))
               .ReturnsAsync((Person?)null);

        // act + assert
        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _personRepositoryMock.Verify(r => r.UpdatePersonAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Never);


        // arrange: entidade existente
        var existing = new Person
        {
            Id = Guid.NewGuid(),
            CPF = "143.998.860-99",
            Name = "Ana",
            IsAdmin = false,
            DateOfBirth = new DateTime(1990, 1, 1)
            // (demais campos que você tiver na entidade)
        };


        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(existing.CPF, It.IsAny<CancellationToken>()))
               .ReturnsAsync(existing);

        Person? captured = null;
        _personRepositoryMock.Setup(r => r.UpdatePersonAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
               .Callback<Person, CancellationToken>((p, _) => captured = p)
               .ReturnsAsync((Person p, CancellationToken _) => p);

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.UpdateSuccess.Should().BeTrue();
        captured.Should().NotBeNull();
        captured.CPF.Should().Be(existing.CPF);                  
        captured.IsAdmin.Should().BeTrue();                       

        _personRepositoryMock.Verify(r => r.UpdatePersonAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}