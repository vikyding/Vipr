﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Vipr.Core.CodeModel;

namespace Vipr.Writer.CSharp.Lite
{
    public class CollectionConstructor : Constructor
    {
        public OdcmClass OdcmClass { get; private set; }

        public CollectionConstructor(OdcmClass odcmClass)
        {
            OdcmClass = odcmClass;
        }
    }
}
