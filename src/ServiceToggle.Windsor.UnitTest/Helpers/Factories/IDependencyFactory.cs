// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;

namespace ServiceToggle.Windsor.UnitTest.Helpers.Factories
{
    public interface IDependencyFactory
    {
        IDependency Create(string name);

        void Release(IDependency dependency);
    }
}