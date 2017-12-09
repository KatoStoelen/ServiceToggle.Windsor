// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using ServiceToggle.Windsor.Registrators;

namespace ServiceToggle.Windsor
{
    public interface IReplacement
    {
        IRegistrator GetRegistrator();
    }
}
