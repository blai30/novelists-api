﻿using System;

namespace NovelistsApi.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}