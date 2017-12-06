// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;

namespace Castle.Windsor.Service.Replacement
{
    internal interface IComponentReplacement<TService> : IReplacement
        where TService : class
    {
        ICollection<Type> ServiceTypes { get; }
        Type NewImplementationType { get; }
        ComponentRegistration<TService> Registration { get; }
    }
}
