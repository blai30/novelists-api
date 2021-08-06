using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Publications
{
    public static class Update
    {
        public sealed record Envelope(string? Title, string? Synopsis) : IRequest<PublicationDto?>;

        public sealed record Command(Guid Id, Envelope Envelope) : IRequest<PublicationDto?>;

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
                await using var context = _factory.CreateDbContext();
                var entity = await context.Publications
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                entity.Title = request.Envelope.Title ?? entity.Title;
                entity.Synopsis = request.Envelope.Synopsis ?? entity.Synopsis;

                await context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<PublicationDto>(entity);

                return dto;
            }
        }
    }
}
