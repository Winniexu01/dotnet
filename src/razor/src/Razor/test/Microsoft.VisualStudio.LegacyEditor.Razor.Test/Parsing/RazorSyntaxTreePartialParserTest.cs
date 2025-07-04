﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

#nullable disable

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Legacy;
using Microsoft.AspNetCore.Razor.Test.Common.Editor;
using Xunit;
using Xunit.Abstractions;
using static Microsoft.AspNetCore.Razor.Language.CommonMetadata;

namespace Microsoft.VisualStudio.LegacyEditor.Razor.Parsing;

public class RazorSyntaxTreePartialParserTest(ITestOutputHelper testOutput) : ToolingParserTestBase(testOutput)
{
    private const string NewLine = "\r\n";

    protected override bool EnableSpanEditHandlers => true;

    public static TheoryData TagHelperPartialParseRejectData
    {
        get
        {
            return new TheoryData<TestEdit>
            {
                CreateInsertionChange("<p></p>", 2, " "),
                CreateInsertionChange("<p></p>", 6, " "),
                CreateInsertionChange("<p some-attr></p>", 12, " "),
                CreateInsertionChange("<p some-attr></p>", 12, "ibute"),
                CreateInsertionChange("<p some-attr></p>", 2, " before"),
            };
        }
    }

    [Theory]
    [MemberData(nameof(TagHelperPartialParseRejectData))]
    public void TagHelperTagBodiesRejectPartialChanges(object objectEdit)
    {
        // Arrange
        var edit = (TestEdit)objectEdit;
        var builder = TagHelperDescriptorBuilder.Create("PTagHelper", "TestAssembly");
        builder.Metadata(TypeName("PTagHelper"));
        builder.TagMatchingRule(rule => rule.TagName = "p");
        var descriptors = new[]
        {
            builder.Build()
        };
        var projectEngine = CreateProjectEngine(tagHelpers: descriptors);
        var projectItem = new TestRazorProjectItem("Index.cshtml")
        {
            Content = edit.OldSnapshot.GetText()
        };
        var codeDocument = projectEngine.Process(projectItem);
        var syntaxTree = codeDocument.GetRequiredSyntaxTree();
        var parser = new RazorSyntaxTreePartialParser(syntaxTree);

        // Act
        var (result, _) = parser.Parse(edit.Change);

        // Assert
        Assert.Equal(PartialParseResultInternal.Rejected, result);
    }

    public static TheoryData TagHelperAttributeAcceptData
    {
        get
        {
            // change, (Block)expectedDocument, partialParseResult
            return new TheoryData<TestEdit, PartialParseResultInternal>
            {
                {
                    CreateInsertionChange("<p str-attr='@DateTime'></p>", 22, "."),
                    PartialParseResultInternal.Accepted | PartialParseResultInternal.Provisional
                },
                {
                    CreateInsertionChange("<p obj-attr='DateTime'></p>", 21, "."),
                    PartialParseResultInternal.Accepted
                },
                {
                    CreateInsertionChange("<p obj-attr='1 + DateTime'></p>", 25, "."),
                    PartialParseResultInternal.Accepted
                },
                {
                    CreateInsertionChange("<p before-attr str-attr='@DateTime' after-attr></p>", 34, "."),
                    PartialParseResultInternal.Accepted | PartialParseResultInternal.Provisional
                },
                {
                    CreateInsertionChange("<p str-attr='before @DateTime after'></p>", 29, "."),
                    PartialParseResultInternal.Accepted | PartialParseResultInternal.Provisional
                },
            };
        }
    }

    [Theory]
    [MemberData(nameof(TagHelperAttributeAcceptData))]
    public void TagHelperAttributesAreLocatedAndAcceptChangesCorrectly(object editObject, object partialParseResultObject)
    {
        // Arrange
        var edit = (TestEdit)editObject;
        var partialParseResult = (PartialParseResultInternal)partialParseResultObject;
        var builder = TagHelperDescriptorBuilder.Create("PTagHelper", "Test");
        builder.Metadata(TypeName("PTagHelper"));
        builder.TagMatchingRule(rule => rule.TagName = "p");
        builder.BindAttribute(attribute =>
        {
            attribute.Name = "obj-attr";
            attribute.TypeName = typeof(object).FullName;
            attribute.SetMetadata(PropertyName("ObjectAttribute"));
        });
        builder.BindAttribute(attribute =>
        {
            attribute.Name = "str-attr";
            attribute.TypeName = typeof(string).FullName;
            attribute.SetMetadata(PropertyName("StringAttribute"));
        });
        var descriptors = new[] { builder.Build() };
        var projectEngine = CreateProjectEngine(tagHelpers: descriptors);
        var sourceDocument = new TestRazorProjectItem("Index.cshtml")
        {
            Content = edit.OldSnapshot.GetText()
        };
        var codeDocument = projectEngine.Process(sourceDocument);
        var syntaxTree = codeDocument.GetRequiredSyntaxTree();
        var parser = new RazorSyntaxTreePartialParser(syntaxTree);

        // Act
        var (result, _) = parser.Parse(edit.Change);

        // Assert
        Assert.Equal(partialParseResult, result);
    }

    [Fact]
    public void ImpExprAcceptsInnerInsertionsInStatementBlock()
    {
        // Arrange
        var changed = new StringTextSnapshot("@{" + NewLine
                                                + "    @DateTime..Now" + NewLine
                                                + "}");
        var old = new StringTextSnapshot("@{" + NewLine
                                            + "    @DateTime.Now" + NewLine
                                            + "}");

        // Act and Assert
        RunPartialParseTest(new TestEdit(17, 0, old, changed, "."));
    }

    [Fact]
    public void ImpExprAcceptsInnerInsertions()
    {
        // Arrange
        var changed = new StringTextSnapshot("foo @DateTime..Now baz");
        var old = new StringTextSnapshot("foo @DateTime.Now baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(13, 0, old, changed, "."), additionalFlags: PartialParseResultInternal.Provisional);
    }

    [Fact]
    public void ImpExprAcceptsWholeIdentifierReplacement()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @date baz");
        var changed = new StringTextSnapshot("foo @DateTime baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(5, 4, old, changed, "DateTime"));
    }

    [Fact]
    public void ImpExprRejectsWholeIdentifierReplacementToKeyword()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @date baz");
        var changed = new StringTextSnapshot("foo @if baz");
        var edit = new TestEdit(5, 4, old, changed, "if");

        // Act & Assert
        RunPartialParseRejectionTest(edit);
    }

    [Fact]
    public void ImpExprRejectsWholeIdentifierReplacementToDirective()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @date baz");
        var changed = new StringTextSnapshot("foo @inherits baz");
        var edit = new TestEdit(5, 4, old, changed, "inherits");

        // Act & Assert
        RunPartialParseRejectionTest(edit, PartialParseResultInternal.SpanContextChanged);
    }

    [Fact]
    public void ImpExprAcceptsPrefixIdentifierReplacements_SingleSymbol()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @dTime baz");
        var changed = new StringTextSnapshot("foo @DateTime baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(5, 1, old, changed, "Date"));
    }

    [Fact]
    public void ImpExprAcceptsPrefixIdentifierReplacements_MultipleSymbols()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @dTime.Now baz");
        var changed = new StringTextSnapshot("foo @DateTime.Now baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(5, 1, old, changed, "Date"));
    }

    [Fact]
    public void ImpExprAcceptsSuffixIdentifierReplacements_SingleSymbol()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @Datet baz");
        var changed = new StringTextSnapshot("foo @DateTime baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(9, 1, old, changed, "Time"));
    }

    [Fact]
    public void ImpExprAcceptsSuffixIdentifierReplacements_MultipleSymbols()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @DateTime.n baz");
        var changed = new StringTextSnapshot("foo @DateTime.Now baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(14, 1, old, changed, "Now"));
    }

    [Fact]
    public void ImpExprAcceptsSurroundedIdentifierReplacements()
    {
        // Arrange
        var old = new StringTextSnapshot("foo @DateTime.n.ToString() baz");
        var changed = new StringTextSnapshot("foo @DateTime.Now.ToString() baz");

        // Act and Assert
        RunPartialParseTest(new TestEdit(14, 1, old, changed, "Now"));
    }

    [Fact]
    public void ImpExprProvisionallyAcceptsDeleteOfIdentifierPartsIfDotRemains()
    {
        var changed = new StringTextSnapshot("foo @User. baz");
        var old = new StringTextSnapshot("foo @User.Name baz");
        RunPartialParseTest(new TestEdit(10, 4, old, changed, string.Empty),
            additionalFlags: PartialParseResultInternal.Provisional);
    }

    [Fact]
    public void ImpExprAcceptsDeleteOfIdentifierPartsIfSomeOfIdentifierRemains()
    {
        var changed = new StringTextSnapshot("foo @Us baz");
        var old = new StringTextSnapshot("foo @User baz");
        RunPartialParseTest(new TestEdit(7, 2, old, changed, string.Empty));
    }

    [Fact]
    public void ImpExprProvisionalForMultipleInsertionIfItCausesIdentifierExpansionAndTrailingDot()
    {
        // ImpExprProvisionallyAcceptsMultipleInsertionIfItCausesIdentifierExpansionAndTrailingDot
        var changed = new StringTextSnapshot("foo @User. baz");
        var old = new StringTextSnapshot("foo @U baz");
        RunPartialParseTest(new TestEdit(6, 0, old, changed, "ser."),
            additionalFlags: PartialParseResultInternal.Provisional);
    }

    [Fact]
    public void ImpExprAcceptsMultipleInsertionIfItOnlyCausesIdentifierExpansion()
    {
        var changed = new StringTextSnapshot("foo @barbiz baz");
        var old = new StringTextSnapshot("foo @bar baz");
        RunPartialParseTest(new TestEdit(8, 0, old, changed, "biz"));
    }

    [Fact]
    public void ImpExprAcceptsIdentifierExpansionAtEndOfNonWhitespaceCharacters()
    {
        var changed = new StringTextSnapshot("@{" + NewLine
                                                + "    @food" + NewLine
                                                + "}");
        var old = new StringTextSnapshot("@{" + NewLine
                                            + "    @foo" + NewLine
                                            + "}");
        RunPartialParseTest(new TestEdit(10 + NewLine.Length, 0, old, changed, "d"));
    }

    [Fact]
    public void ImpExprAcceptsIdentifierAfterDotAtEndOfNonWhitespaceCharacters()
    {
        var changed = new StringTextSnapshot("@{" + NewLine
                                                + "    @foo.d" + NewLine
                                                + "}");
        var old = new StringTextSnapshot("@{" + NewLine
                                            + "    @foo." + NewLine
                                            + "}");
        RunPartialParseTest(new TestEdit(11 + NewLine.Length, 0, old, changed, "d"));
    }

    [Fact]
    public void ImpExprAcceptsDotAtEndOfNonWhitespaceCharacters()
    {
        var changed = new StringTextSnapshot("@{" + NewLine
                                                + "    @foo." + NewLine
                                                + "}");
        var old = new StringTextSnapshot("@{" + NewLine
                                            + "    @foo" + NewLine
                                            + "}");
        RunPartialParseTest(new TestEdit(10 + NewLine.Length, 0, old, changed, "."));
    }

    [Fact]
    public void ImpExprProvisionallyAcceptsDotAfterIdentifierInMarkup()
    {
        var changed = new StringTextSnapshot("foo @foo. bar");
        var old = new StringTextSnapshot("foo @foo bar");
        RunPartialParseTest(new TestEdit(8, 0, old, changed, "."),
            additionalFlags: PartialParseResultInternal.Provisional);
    }

    [Fact]
    public void ImpExprAcceptsAdditionalIdentifierCharactersIfEndOfSpanIsIdentifier()
    {
        var changed = new StringTextSnapshot("foo @foob bar");
        var old = new StringTextSnapshot("foo @foo bar");
        RunPartialParseTest(new TestEdit(8, 0, old, changed, "b"));
    }

    [Fact]
    public void ImpExprAcceptsAdditionalIdentifierStartCharactersIfEndOfSpanIsDot()
    {
        var changed = new StringTextSnapshot("@{@foo.b}");
        var old = new StringTextSnapshot("@{@foo.}");
        RunPartialParseTest(new TestEdit(7, 0, old, changed, "b"));
    }

    [Fact]
    public void ImpExprAcceptsDotIfTrailingDotsAreAllowed()
    {
        var changed = new StringTextSnapshot("@{@foo.}");
        var old = new StringTextSnapshot("@{@foo}");
        RunPartialParseTest(new TestEdit(6, 0, old, changed, "."));
    }

    private static void RunPartialParseRejectionTest(TestEdit edit, PartialParseResultInternal additionalFlags = 0)
    {
        var templateEngine = CreateProjectEngine();
        var codeDocument = templateEngine.CreateCodeDocument(edit.OldSnapshot.GetText());
        templateEngine.Engine.Process(codeDocument);
        var syntaxTree = codeDocument.GetRequiredSyntaxTree();
        var parser = new RazorSyntaxTreePartialParser(syntaxTree);

        var (result, _) = parser.Parse(edit.Change);
        Assert.Equal(PartialParseResultInternal.Rejected | additionalFlags, result);
    }

    private void RunPartialParseTest(TestEdit edit, PartialParseResultInternal additionalFlags = 0)
    {
        var templateEngine = CreateProjectEngine();
        var codeDocument = templateEngine.CreateCodeDocument(edit.OldSnapshot.GetText());
        templateEngine.Engine.Process(codeDocument);
        var syntaxTree = codeDocument.GetRequiredSyntaxTree();
        var parser = new RazorSyntaxTreePartialParser(syntaxTree);

        var (result, _) = parser.Parse(edit.Change);
        Assert.Equal(PartialParseResultInternal.Accepted | additionalFlags, result);

        var newSource = TestRazorSourceDocument.Create(edit.NewSnapshot.GetText());
        var newSyntaxTree = new RazorSyntaxTree(parser.ModifiedSyntaxTreeRoot, newSource, parser.OriginalSyntaxTree.Diagnostics, parser.OriginalSyntaxTree.Options);
        BaselineTest(newSyntaxTree);
    }

    private static TestEdit CreateInsertionChange(string initialText, int insertionLocation, string insertionText)
    {
        var changedText = initialText.Insert(insertionLocation, insertionText);
        var sourceChange = new SourceChange(insertionLocation, 0, insertionText);
        var oldSnapshot = new StringTextSnapshot(initialText);
        var changedSnapshot = new StringTextSnapshot(changedText);
        return new TestEdit(sourceChange, oldSnapshot, changedSnapshot);
    }

    private static RazorProjectEngine CreateProjectEngine(
        IEnumerable<TagHelperDescriptor> tagHelpers = null)
    {
        var fileSystem = new TestRazorProjectFileSystem();
        var projectEngine = RazorProjectEngine.Create(RazorConfiguration.Default, fileSystem, builder =>
        {
            RazorExtensions.Register(builder);

            builder.AddDefaultImports("@addTagHelper *, Test");

            if (tagHelpers != null)
            {
                builder.AddTagHelpers(tagHelpers);
            }

            builder.ConfigureParserOptions(VisualStudioRazorParser.ConfigureParserOptions);
        });

        return projectEngine;
    }
}
