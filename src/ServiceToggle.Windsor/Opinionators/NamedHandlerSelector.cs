// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;

namespace ServiceToggle.Windsor.Opinionators
{
    internal class NamedHandlerSelector : IHandlerSelector
    {
        private readonly List<INamedComponentSelection> _componentSelections;

        public NamedHandlerSelector(IEnumerable<INamedComponentSelection> componentReplacements)
        {
            _componentSelections = componentReplacements.ToList();
        }

        public bool HasOpinionAbout(string name, Type service)
        {
            return
                name != null &&
                _componentSelections.Any(selection => selection.Name == name && selection.IsActive());
        }

        public IHandler SelectHandler(string name, Type service, IHandler[] handlers)
        {
            var selection = _componentSelections.Single(s => s.Name == name);

            return handlers
                .SingleOrDefault(handler =>
                    handler.ComponentModel.Implementation == selection.NewImplementationType);
        }
    }
}