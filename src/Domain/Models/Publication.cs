using System;
using NovelistsApi.Domain.Common;

namespace NovelistsApi.Domain.Models
{
    public class Publication : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Synopsis { get; set; }
    }
}
