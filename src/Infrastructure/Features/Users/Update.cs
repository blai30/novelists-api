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
        public sealed record Command(Guid Id, string Email, string Password, string DisplayName) : IRequest<UserDto?>;

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
                return null;
            }
        }
    }
}
