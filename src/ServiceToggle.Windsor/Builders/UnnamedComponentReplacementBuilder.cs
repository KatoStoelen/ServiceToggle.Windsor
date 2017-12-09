// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceToggle.Windsor.Builders
{
    internal class UnnamedComponentReplacementBuilder<TService> : ComponentReplacementBuilder<TService>
        where TService : class
    {
        internal UnnamedComponentReplacementBuilder(ICollection<Type> serviceTypes)
            : base(serviceTypes)
        {
        }

        public override IReplacement ReplacedBy<TImplementation>()
        {
            return new UnnamedComponentReplacement<TService>
            {
                ServiceTypes = ServiceTypes,
                OldImplementationType = OldImplementationType,
                NewImplementationType = typeof(TImplementation),
                IsActive = IsActive
            };
        }

        public override IReplacement ReplacedBy<TImplementation>(ComponentRegistration<TService> registration)
        {
            return new UnnamedComponentReplacement<TService>
            {
                ServiceTypes = ServiceTypes,
                OldImplementationType = OldImplementationType,
                NewImplementationType = typeof(TImplementation),
                Registration = registration,
                IsActive = IsActive
            };
        }

        public override IReplacement ReplacedBy(Type implementationType)
        {
            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));
            if (!ServiceTypes.First().IsAssignableFrom(implementationType))
                throw new ArgumentException(
                    $"Type '{implementationType.FullName}' is not an implementation of '{ServiceTypes.First().FullName}'",
                    nameof(implementationType));

            return new UnnamedComponentReplacement<TService>
            {
                ServiceTypes = ServiceTypes,
                OldImplementationType = OldImplementationType,
                NewImplementationType = implementationType,
                IsActive = IsActive
            };
        }

        public override IReplacement ReplacedBy(Type implementationType, ComponentRegistration<TService> registration)
        {
            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));
            if (!ServiceTypes.First().IsAssignableFrom(implementationType))
                throw new ArgumentException(
                    $"Type '{implementationType.FullName}' is not an implementation of '{ServiceTypes.First().FullName}'",
                    nameof(implementationType));

            return new UnnamedComponentReplacement<TService>
            {
                ServiceTypes = ServiceTypes,
                OldImplementationType = OldImplementationType,
                NewImplementationType = implementationType,
                Registration = registration,
                IsActive = IsActive
            };
        }
    }
}
