// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Collections.Generic;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;

namespace Castle.Windsor.Service.Replacement.UnitTest.Helpers
{
    internal class ServiceWithCollectionDependency
    {
        public IEnumerable<IDependency> Dependencies { get; }

        public ServiceWithCollectionDependency(IEnumerable<IDependency> dependencies)
        {
            Dependencies = dependencies;
        }
    }
}