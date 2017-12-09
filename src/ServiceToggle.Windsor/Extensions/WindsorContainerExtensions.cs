// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Linq;
using Castle.MicroKernel;
using ServiceToggle.Windsor.Opinionators;
using System.Collections.Generic;
using Castle.Windsor;

namespace ServiceToggle.Windsor.Extensions
{
    public static class WindsorContainerExtensions
    {
        public static IWindsorContainer Register(this IWindsorContainer @this, params IReplacement[] replacements)
        {
            @this.Kernel.AddHandlersFilter(new HandlerFilter(replacements.OfType<IReplacementFilter>()));
            @this.Kernel.AddHandlerSelector(new NamedHandlerSelector(replacements.OfType<INamedComponentSelection>()));
            @this.Kernel.AddHandlerSelector(new UnnamedHandlerSelector(replacements.OfType<IUnnamedComponentSelection>()));

            foreach (var registrator in replacements.Select(replacement => replacement.GetRegistrator()))
            {
                registrator.Execute(@this);
            }

            return @this;
        }

        internal static IHandler GetHandlerByServiceTypesAndImplementation(
            this IWindsorContainer @this, ICollection<Type> serviceTypes, Type implementationType)
        {
            // TODO: Require all service types to be present?
            var assignableHandlers = @this.Kernel.GetAssignableHandlers(typeof(object))
                .Where(handler => serviceTypes.All(serviceType => handler.ComponentModel.Services.Contains(serviceType)))
                .ToList();
            if (assignableHandlers.Count == 1)
                return assignableHandlers.Single();

            if (assignableHandlers.Count > 1 && implementationType == null)
                throw new InvalidOperationException(
                    $"Ambiguous. Found multiple handlers of {serviceTypes.First().FullName}. " +
                    "Use ComponentReplacement.For<TService>().CurrentlyImplementedBy<TOldImpl>().ReplacedWith<TNewImpl>()");

            return assignableHandlers
                .SingleOrDefault(handler =>
                    handler.ComponentModel.Implementation == implementationType);
        }

        internal static IHandler GetHandlerByImplementation(this IWindsorContainer @this, Type implementationType)
        {
            return @this.Kernel.GetAssignableHandlers(typeof(object))
                .SingleOrDefault(handler => handler.ComponentModel.Implementation == implementationType);
        }
    }
}