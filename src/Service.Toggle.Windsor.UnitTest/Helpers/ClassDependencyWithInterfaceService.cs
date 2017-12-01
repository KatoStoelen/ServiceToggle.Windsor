﻿// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;

namespace Castle.Windsor.Service.Replacement.UnitTest.Helpers
{
    internal class ClassDependencyWithInterfaceService
    {
        public DependencyImpl1 Dependency { get; }

        public ClassDependencyWithInterfaceService(DependencyImpl1 dependency)
        {
            Dependency = dependency;
        }
    }
}