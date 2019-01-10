﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Microsoft.AspNetCore.Razor.Language.Extensions
{
    public sealed class DefaultTagHelperHtmlAttributeIntermediateNode : ExtensionIntermediateNode
    {
        public DefaultTagHelperHtmlAttributeIntermediateNode()
        {
        }

        public DefaultTagHelperHtmlAttributeIntermediateNode(TagHelperHtmlAttributeIntermediateNode htmlAttributeNode)
        {
            if (htmlAttributeNode == null)
            {
                throw new ArgumentNullException(nameof(htmlAttributeNode));
            }

            AttributeName = htmlAttributeNode.AttributeName;
            AttributeStructure = htmlAttributeNode.AttributeStructure;
            Source = htmlAttributeNode.Source;

            for (var i = 0; i < htmlAttributeNode.Children.Count; i++)
            {
                Children.Add(htmlAttributeNode.Children[i]);
            }

            for (var i = 0; i < htmlAttributeNode.Diagnostics.Count; i++)
            {
                Diagnostics.Add(htmlAttributeNode.Diagnostics[i]);
            }
        }

        public string AttributeName { get; set; }

        public AttributeStructure AttributeStructure { get; set; }

        public override IntermediateNodeCollection Children { get; } = new IntermediateNodeCollection();

        public override void Accept(IntermediateNodeVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            AcceptExtensionNode<DefaultTagHelperHtmlAttributeIntermediateNode>(this, visitor);
        }

        public override void WriteNode(CodeTarget target, CodeRenderingContext context)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var extension = target.GetExtension<IDefaultTagHelperTargetExtension>();
            if (extension == null)
            {
                ReportMissingCodeTargetExtension<IDefaultTagHelperTargetExtension>(context);
                return;
            }

            extension.WriteTagHelperHtmlAttribute(context, this);
        }
    }
}