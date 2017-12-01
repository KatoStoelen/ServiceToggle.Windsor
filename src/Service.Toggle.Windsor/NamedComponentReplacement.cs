// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Opinionators;
using Castle.Windsor.Service.Replacement.Registrators;
using System;
using System.Collections.Generic;

namespace Castle.Windsor.Service.Replacement
{
    internal class NamedComponentReplacement<TService> : INamedComponentSelection, IComponentReplacement<TService>
        where TService : class
    {
        public string Name { get; set; }
        public ICollection<Type> ServiceTypes { get; set; }
        public Type NewImplementationType { get; set; }
        public ComponentRegistration<TService> Registration { get; set; }
        public Func<bool> IsActive { get; set; }

        public IRegistrator GetRegistrator()
        {
            return new NamedReplacementRegistrator<TService>(this);
        }
    }
}