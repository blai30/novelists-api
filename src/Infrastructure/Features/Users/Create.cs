using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Domain.Models;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public static class Create
    {
        public sealed record Command(string Email, string Password, string DisplayName) : IRequest<UserDto?>;

        public sealed class CommandHandler : IRequestHandler<Command, UserDto?>
        {
            private readonly IDbContextFactory<NovelistsDbContext> _factory;
            private readonly IMapper _mapper;

            public CommandHandler(IDbContextFactory<NovelistsDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<UserDto?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    DisplayName = request.DisplayName
                };

                await using var context = _factory.CreateDbContext();
                var entry = await context.Users.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<UserDto>(entry.Entity);

                return dto;
            }
        }
    }
}
