﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Vipr.Writer.CSharp.Lite.Settings;
using FluentAssertions;
using Microsoft.Its.Recipes;
using Moq;
using Vipr.Core;
using Vipr.Core.CodeModel;
using Vipr.Writer.CSharp.Lite;
using Xunit;

namespace CSharpLiteWriterUnitTests
{
    /// <summary>
    /// Summary description for Given_an_OdcmModel
    /// </summary>
    public class Given_an_OdcmClass_Service_Property_forced_to_pascal_case : EntityTestBase
    {
        private OdcmProperty _property;

        public Given_an_OdcmClass_Service_Property_forced_to_pascal_case()
        {
            SetConfiguration(new CSharpWriterSettings
            {
                ForcePropertyPascalCasing = true
            });

            base.Init(m =>
            {
                _property = Model.EntityContainer.Properties.RandomElement();

                _property = _property.Rename(Any.Char('a', 'z') + _property.Name);
            });
        }

        [Fact]
        public void The_EntityContainer_class_has_the_renamed_property()
        {
            bool isCollection = _property.IsCollection;
            var propertyType = _property.Projection.Type;
            var identifier = isCollection
                ? NamesService.GetCollectionInterfaceName(propertyType)
                : NamesService.GetFetcherInterfaceName(propertyType);

            EntityContainerType.Should().HaveProperty(
                CSharpAccessModifiers.Public, 
                isCollection ? (CSharpAccessModifiers?)null : CSharpAccessModifiers.Private,
                Proxy.GetInterface(_property.Type.Namespace, identifier.Name),
                GetPascalCaseName(_property));
        }

        [Fact]
        public void The_EntityContainer_class_does_not_have_the_original_property_deprecated()
        {
            EntityContainerType.Should().NotHaveProperty(_property.Name);
        }

        private static string GetPascalCaseName(OdcmProperty property)
        {
            return property.Name.Substring(0, 1).ToUpper() + property.Name.Substring(1);
        }
    }
}
