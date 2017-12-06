// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Collections.Generic;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;

namespace Castle.Windsor.Service.Replacement.UnitTest.Helpers
{
    internal class CollectionDependencyService
    {
        public IEnumerable<IDependency> Dependencies { get; }

        public CollectionDependencyService(IEnumerable<IDependency> dependencies)
        {
            Dependencies = dependencies;
        }
    }
}