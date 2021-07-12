using System;

namespace NovelistsApi.Infrastructure.Features.Publications
{
    public record PublicationDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Title { get; init; } = default;
        public string? Synopsis { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
