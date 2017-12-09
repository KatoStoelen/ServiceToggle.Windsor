// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using Castle.MicroKernel;
using ServiceToggle.Windsor.Extensions;
using System.Collections.Generic;
using Castle.Windsor;

namespace ServiceToggle.Windsor.Registrators
{
    internal class UnnamedReplacementRegistrator<TService> : ReplacementRegistrator<TService>
        where TService : class
    {
        private readonly ICollection<Type> _serviceTypes;
        private readonly Type _oldImplementationType;

        public UnnamedReplacementRegistrator(UnnamedComponentReplacement<TService> componentReplacement)
            : base(componentReplacement)
        {
            _serviceTypes = componentReplacement.ServiceTypes;
            _oldImplementationType = componentReplacement.OldImplementationType;
        }

        protected override IHandler GetHandlerToReplace(IWindsorContainer container)
        {
            var handlerToReplace = container.GetHandlerByServiceTypesAndImplementation(
                _serviceTypes, _oldImplementationType);
            if (handlerToReplace == null)
                throw new InvalidOperationException(
                    $"Handler of service '{typeof(TService).FullName}' is not registered");

            return handlerToReplace;
        }
    }
}