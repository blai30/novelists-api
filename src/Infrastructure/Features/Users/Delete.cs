using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Users;

public static class Delete
{
    public sealed record Command(Guid Id) : IRequest<UserDto?>;

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
            await using var context = _factory.CreateDbContext();
            var entity = await context.Users
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            context.Users.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<UserDto>(entity);
            return dto;
        }
    }
}
