// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Extensions;

namespace Castle.Windsor.Service.Replacement.Registrators
{
    internal abstract class ReplacementRegistrator<TService> : IRegistrator
        where TService : class
    {
        private readonly IComponentReplacement<TService> _componentReplacement;

        public ReplacementRegistrator(IComponentReplacement<TService> componentReplacement)
        {
            _componentReplacement = componentReplacement;
        }

        protected ComponentRegistration<TService> CustomRegistration => _componentReplacement.Registration;

        public void Execute(IWindsorContainer container)
        {
            var handlerOfNewImplementation = container.GetHandlerByImplementation(_componentReplacement.NewImplementationType);
            if (handlerOfNewImplementation != null)
                throw new InvalidOperationException(
                    $"Component with implementation '{_componentReplacement.NewImplementationType.FullName}' already registered");

            var handlerToReplace = GetHandlerToReplace(container);

            if (CustomRegistration != null)
                container.Register(CustomRegistration.IsFallback());
            else
                container.Register(GetNewRegistration(handlerToReplace));
        }

        protected abstract IHandler GetHandlerToReplace(IWindsorContainer container);

        private ComponentRegistration<object> GetNewRegistration(IHandler handlerToReplace)
        {
            var componentRegistration = GetComponentRegistration();

            var lifestyle = handlerToReplace.GetLifestyleType();
            if (lifestyle != LifestyleType.Scoped)
                return componentRegistration.LifeStyle.Is(lifestyle);

            var scopeAccessorType =
                (Type) handlerToReplace.ComponentModel.ExtendedProperties[Constants.ScopeAccessorType];

            return componentRegistration.LifestyleScoped(scopeAccessorType);
        }

        private ComponentRegistration<object> GetComponentRegistration()
        {
            return Component
                .For(_componentReplacement.ServiceTypes)
                .ImplementedBy(_componentReplacement.NewImplementationType)
                .IsFallback();
        }
    }
}