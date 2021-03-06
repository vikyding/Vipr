﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Vipr.Core.CodeModel;

namespace Vipr.Writer.CSharp.Lite
{
    public class FetcherNavigationProperty : NavigationProperty
    {
        public Identifier InstanceType { get; internal set; }

        protected FetcherNavigationProperty(OdcmProperty odcmProperty) : base(odcmProperty)
        {
            FieldName = NamesService.GetFetcherFieldName(odcmProperty);
            InstanceType = NamesService.GetFetcherTypeName(odcmProperty.Projection.Type);
            PrivateSet = true;
            Type = new Type(NamesService.GetFetcherInterfaceName(odcmProperty.Projection.Type, odcmProperty.Projection));
        }

        public static FetcherNavigationProperty ForConcrete(OdcmProperty odcmProperty)
        {
            return new FetcherNavigationProperty(odcmProperty)
            {
                DefiningInterface = NamesService.GetFetcherInterfaceName(odcmProperty.Class)
            };
        }

        public static FetcherNavigationProperty ForFetcher(OdcmProperty odcmProperty)
        {
            return new FetcherNavigationProperty(odcmProperty);
        }
    }
}
