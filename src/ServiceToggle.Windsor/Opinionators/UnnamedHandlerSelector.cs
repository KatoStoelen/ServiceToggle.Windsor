// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceToggle.Windsor.Opinionators
{
    internal class UnnamedHandlerSelector : IHandlerSelector
    {
        private readonly List<IUnnamedComponentSelection> _componentSelections;

        public UnnamedHandlerSelector(IEnumerable<IUnnamedComponentSelection> componentReplacements)
        {
            _componentSelections = componentReplacements.ToList();
        }

        public bool HasOpinionAbout(string name, Type service)
        {
            return _componentSelections.Any(
                selection =>
                    selection.ServiceTypes.Any(serviceType => serviceType == service) &&
                    selection.IsActive());
        }

        public IHandler SelectHandler(string name, Type service, IHandler[] handlers)
        {
            var selection = _componentSelections.First(s => s.ServiceTypes.Any(serviceType => serviceType == service));

            return handlers
                .SingleOrDefault(handler =>
                    handler.ComponentModel.Implementation == selection.NewImplementationType);
        }
    }
}
