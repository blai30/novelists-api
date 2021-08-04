using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public static class Update
    {
        public sealed record Model(string? Email, string? Password, string? DisplayName) : IRequest<UserDto?>;

        public sealed record Command(Guid Id, Model Model) : IRequest<UserDto?>;

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
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                entity.Email = request.Model.Email ?? entity.Email;
                entity.Password = request.Model.Password ?? entity.Password;
                entity.DisplayName = request.Model.DisplayName ?? entity.DisplayName;
                entity.UpdatedAt = DateTime.Now;

                await context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<UserDto>(entity);

                return dto;
            }
        }
    }
}
