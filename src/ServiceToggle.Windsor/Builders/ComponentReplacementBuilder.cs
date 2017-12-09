// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceToggle.Windsor.Builders
{
    public abstract class ComponentReplacementBuilder<TService>
        where TService : class
    {
        protected ICollection<Type> ServiceTypes { get; set; }
        protected Type OldImplementationType { get; set; }
        protected Func<bool> IsActive { get; private set; }

        internal ComponentReplacementBuilder(ICollection<Type> serviceTypes)
        {
            ServiceTypes = serviceTypes;
            IsActive = () => true;
        }

        // TODO: Can we find a better name for this?
        public ComponentReplacementBuilder<TService> CurrentlyImplementedBy<TImplementation>()
            where TImplementation : TService
        {
            OldImplementationType = typeof(TImplementation);

            return this;
        }

        // TODO: Can we find a better name for this?
        public ComponentReplacementBuilder<TService> CurrentlyImplementedBy(Type implementationType)
        {
            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));
            if (!ServiceTypes.First().IsAssignableFrom(implementationType))
                throw new ArgumentException(
                    $"Type '{implementationType.FullName}' is not an implementation of '{ServiceTypes.First().FullName}'",
                    nameof(implementationType));

            OldImplementationType = implementationType;

            return this;
        }

        public ComponentReplacementBuilder<TService> If(Func<bool> isActive)
        {
            IsActive = isActive;

            return this;
        }

        public abstract IReplacement ReplacedBy<TImplementation>()
            where TImplementation : TService;
        public abstract IReplacement ReplacedBy<TImplementation>(ComponentRegistration<TService> registration)
            where TImplementation : TService;

        public abstract IReplacement ReplacedBy(Type implementationType);
        public abstract IReplacement ReplacedBy(Type implementationType, ComponentRegistration<TService> registration);
    }
}
