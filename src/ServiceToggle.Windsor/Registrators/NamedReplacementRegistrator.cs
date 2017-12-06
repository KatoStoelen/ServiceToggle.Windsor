// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Linq;
using Castle.MicroKernel;
using System.Collections.Generic;

namespace Castle.Windsor.Service.Replacement.Registrators
{
    internal class NamedReplacementRegistrator<TService> : ReplacementRegistrator<TService>
        where TService : class
    {
        private readonly string _name;
        private readonly ICollection<Type> _serviceTypes;

        public NamedReplacementRegistrator(NamedComponentReplacement<TService> componentReplacement)
            : base(componentReplacement)
        {
            _name = componentReplacement.Name;
            _serviceTypes = componentReplacement.ServiceTypes;
        }

        protected override IHandler GetHandlerToReplace(IWindsorContainer container)
        {
            var handlerToReplace = container.Kernel.GetHandler(_name);
            if (handlerToReplace == null)
                throw new InvalidOperationException($"Unable to find component with name '{_name}'");

            foreach (var serviceType in _serviceTypes)
            {
                if (!handlerToReplace.ComponentModel.Services.Contains(serviceType))
                    throw new InvalidOperationException(
                        $"Component with name '{_name}' is not registered as an implementation of '{serviceType.FullName}'");
            }

            return handlerToReplace;
        }
    }
}