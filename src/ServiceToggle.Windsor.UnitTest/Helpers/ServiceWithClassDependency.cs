// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;

namespace ServiceToggle.Windsor.UnitTest.Helpers
{
    internal class ServiceWithClassDependency
    {
        public DependencyImpl1 Dependency { get; }

        public ServiceWithClassDependency(DependencyImpl1 dependency)
        {
            Dependency = dependency;
        }
    }
}