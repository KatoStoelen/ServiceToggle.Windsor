// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.Windsor.Service.Replacement.Registrators;

namespace Castle.Windsor.Service.Replacement
{
    public interface IReplacement
    {
        IRegistrator GetRegistrator();
    }
}
