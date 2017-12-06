// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;

namespace Castle.Windsor.Service.Replacement.Opinionators
{
    internal class HandlerFilter : IHandlersFilter
    {
        private readonly List<IReplacementFilter> _replacementFilters;

        public HandlerFilter(IEnumerable<IReplacementFilter> componentReplacements)
        {
            _replacementFilters = componentReplacements.ToList();
        }

        public bool HasOpinionAbout(Type service)
        {
            return _replacementFilters.Any(filter => filter.ServiceTypes.Any(serviceType => serviceType == service));
        }

        public IHandler[] SelectHandlers(Type service, IHandler[] handlers)
        {
            var implementationsToExclude = _replacementFilters
                .Select(filter =>
                    filter.IsActive()
                        ? filter.OldImplementationType
                        : filter.NewImplementationType)
                .ToList();

            return handlers
                .Where(handler => !implementationsToExclude.Contains(handler.ComponentModel.Implementation))
                .ToArray();
        }
    }
}