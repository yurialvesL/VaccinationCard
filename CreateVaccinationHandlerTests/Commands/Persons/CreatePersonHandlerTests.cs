
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Person.CreatePerson;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Application.Common.Mappings.Person;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.CrossCutting.Common.Interfaces;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enum;
using VaccinationCard.Domain.Interfaces;
using VaccinationCardHandlerTests.Commands.Vaccinations;

namespace VaccinationCardHandlerTests.Commands.Persons;

public class CreatePersonHandlerTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<ILogger<CreatePersonHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public CreatePersonHandlerTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _loggerMock = new Mock<ILogger<CreatePersonHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PersonProfile());
        }, loggerFactory);

        _mapper = configuration.CreateMapper();
    }

        private CreatePersonHandler SUT() => new(
      _personRepositoryMock.Object,
      _passwordHasherMock.Object,
      _loggerMock.Object,
      _mapper
      );

    [Fact]
    public async Task Should_throw_Conflict_when_cpf_already_registered()
    {
        // arrange
        var cmd = new CreatePersonCommand
        {
            Name = "Ana",
            CPF = "143.998.860-99",
            Password = "plain@!23",
            Sex = Sex.Feminine.ToString(),
            DateOfBirth = new DateTime(2000, 1, 1),
            IsAdmin = false
        };

        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(cmd.CPF, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new Person { Id = Guid.NewGuid(), CPF = cmd.CPF });

        // act + assert
        await Assert.ThrowsAsync<ConflictException>(() => SUT().Handle(cmd, default));

        // must not attempt to hashear or create
        _passwordHasherMock.Verify(h => h.HashPassword(It.IsAny<string>()), Times.Never);
        _personRepositoryMock.Verify(r => r.CreatePersonAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Never);
    }


    [Fact]
    public async Task Should_hash_password_create_and_return_result_on_success()
    {
        // arrange
        var cmd = new CreatePersonCommand
        {
            Name = "Ana",
            CPF = "143.998.860-99",
            Password = "plain@!23",
            Sex = Sex.Feminine.ToString(),
            DateOfBirth = new DateTime(1995, 5, 20),
            IsAdmin = true
        };

        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(cmd.CPF, It.IsAny<CancellationToken>()))
             .ReturnsAsync((Person?)null);

        _passwordHasherMock
        .Setup(h => h.HashPassword(cmd.Password))
        .Returns("hashed!");


        _personRepositoryMock.Setup(r => r.CreatePersonAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Person)null!); 
            
        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.PersonName.Should().Be("Ana");
        result.CPF.Should().Be("143.998.860-99");
        result.IsAdmin.Should().BeTrue();

        _passwordHasherMock.Verify(h => h.HashPassword("plain@!23"), Times.Once);

        // we checked that the handler asked for the hash and called the repo
        _passwordHasherMock.Verify(h => h.HashPassword("plain@!23"), Times.Once);
        _personRepositoryMock.Verify(r => r.CreatePersonAsync(It.Is<Person>(p =>
                p.Name == "Ana" &&
                p.CPF == "143.998.860-99" &&
                p.PasswordHash == "hashed!" &&
                p.IsAdmin == true), It.IsAny<CancellationToken>()), Times.Once);

        // you don't necessarily need to log in to the happy path
        _loggerMock.VerifyNoLog(LogLevel.Error);
    }

    [Fact]
    public async Task Should_log_error_and_still_return_result_when_repository_throws()
    {
        // arrange
        var cmd = new CreatePersonCommand
        {
            Name = "Ana",
            CPF = "143.998.860-99",
            Password = "plain@!23",
            Sex = Sex.Masculine.ToString(),
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        _personRepositoryMock.Setup(r => r.GetPersonByCPFAsync(cmd.CPF, It.IsAny<CancellationToken>()))
             .ReturnsAsync((Person?)null);

        _passwordHasherMock.Setup(h => h.HashPassword("plain")).Returns("hashed!");

        _personRepositoryMock.Setup(r => r.CreatePersonAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
             .ThrowsAsync(new Exception("db boom"));

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.PersonName.Should().Be("Ana");
        result.CPF.Should().Be("143.998.860-99");

        _loggerMock.VerifyLog(LogLevel.Error, "An error occured");
    }
}
