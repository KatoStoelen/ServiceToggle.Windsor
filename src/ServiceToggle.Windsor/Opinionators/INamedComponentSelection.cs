// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;

namespace ServiceToggle.Windsor.Opinionators
{
    internal interface INamedComponentSelection
    {
        string Name { get; }
        Type NewImplementationType { get; }
        Func<bool> IsActive { get; }
    }
}