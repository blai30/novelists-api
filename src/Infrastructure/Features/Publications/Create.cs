using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Domain.Models;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Publications;

public static class Create
{
    public sealed record Command(User User, string Title, string Synopsis) : IRequest<PublicationDto?>;

    public sealed class CommandHandler : IRequestHandler<Command, PublicationDto?>
    {
        private readonly IDbContextFactory<NovelistsDbContext> _factory;
        private readonly IMapper _mapper;

        public CommandHandler(IDbContextFactory<NovelistsDbContext> factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<PublicationDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Publication
            {
                User = request.User,
                Title = request.Title,
                Synopsis = request.Synopsis
            };

            await using var context = _factory.CreateDbContext();
            var entry = await context.Publications.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<PublicationDto>(entry.Entity);

            return dto;
        }
    }
}