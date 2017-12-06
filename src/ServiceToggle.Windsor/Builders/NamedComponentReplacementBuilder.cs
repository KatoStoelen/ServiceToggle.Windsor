// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castle.Windsor.Service.Replacement.Builders
{
    internal class NamedComponentReplacementBuilder<TService> : ComponentReplacementBuilder<TService>
        where TService : class
    {
        private readonly string _name;

        public NamedComponentReplacementBuilder(string name, ICollection<Type> serviceTypes)
            : base(serviceTypes)
        {
            _name = name;
        }

        public override IReplacement ReplacedBy<TImplementation>()
        {
            return new NamedComponentReplacement<TService>
            {
                Name = _name,
                ServiceTypes = ServiceTypes,
                NewImplementationType = typeof(TImplementation),
                IsActive = IsActive
            };
        }

        public override IReplacement ReplacedBy<TImplementation>(ComponentRegistration<TService> registration)
        {
            return new NamedComponentReplacement<TService>
            {
                Name = _name,
                ServiceTypes = ServiceTypes,
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

            return new NamedComponentReplacement<TService>
            {
                Name = _name,
                ServiceTypes = ServiceTypes,
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

            return new NamedComponentReplacement<TService>
            {
                Name = _name,
                ServiceTypes = ServiceTypes,
                NewImplementationType = implementationType,
                Registration = registration,
                IsActive = IsActive
            };
        }
    }
}
