// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Collections.Generic;

namespace Castle.Windsor.Service.Replacement.Opinionators
{
    internal interface IReplacementFilter
    {
        ICollection<Type> ServiceTypes { get; }
        Type OldImplementationType { get; }
        Type NewImplementationType { get; }
        Func<bool> IsActive { get; }
    }
}