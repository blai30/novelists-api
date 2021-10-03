using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Publications;

public static class Delete
{
    public sealed record Command(Guid Id) : IRequest<PublicationDto?>;

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
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            context.Publications.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<PublicationDto>(entity);
            return dto;
        }
    }
}