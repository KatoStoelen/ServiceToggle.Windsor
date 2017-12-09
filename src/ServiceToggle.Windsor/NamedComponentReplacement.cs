// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using ServiceToggle.Windsor.Opinionators;
using ServiceToggle.Windsor.Registrators;
using System;
using System.Collections.Generic;

namespace ServiceToggle.Windsor
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