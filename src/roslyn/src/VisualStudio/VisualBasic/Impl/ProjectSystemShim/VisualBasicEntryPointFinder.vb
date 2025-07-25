﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis
Imports Microsoft.VisualStudio.LanguageServices.Implementation.ProjectSystem

Namespace Microsoft.VisualStudio.LanguageServices.VisualBasic.ProjectSystemShim
    Friend NotInheritable Class VisualBasicEntryPointFinder
        Inherits AbstractEntryPointFinder

        Private ReadOnly _findFormsOnly As Boolean

        Public Sub New(compilation As Compilation, findFormsOnly As Boolean)
            MyBase.New(compilation)
            _findFormsOnly = findFormsOnly
        End Sub

        Protected Overrides Function MatchesMainMethodName(name As String) As Boolean
            If _findFormsOnly Then
                Return False
            End If

            Return String.Equals(name, "Main", StringComparison.OrdinalIgnoreCase)
        End Function

        Public Shared Function FindEntryPoints(compilation As Compilation, findFormsOnly As Boolean) As IEnumerable(Of INamedTypeSymbol)
            Dim visitor = New VisualBasicEntryPointFinder(compilation, findFormsOnly)
            Dim symbol = compilation.SourceModule.GlobalNamespace

            visitor.Visit(symbol)
            Return visitor.EntryPoints
        End Function

        Public Overrides Sub VisitNamedType(symbol As INamedTypeSymbol)
            ' It's a form if it Inherits System.Windows.Forms.Form.
            Dim baseType = symbol.BaseType
            While baseType IsNot Nothing
                If baseType.ToDisplayString() = "System.Windows.Forms.Form" Then
                    EntryPoints.Add(symbol)
                    Exit While
                End If

                baseType = baseType.BaseType
            End While

            MyBase.VisitNamedType(symbol)
        End Sub
    End Class
End Namespace
