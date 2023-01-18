//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class SolutionContext
    {
        public SolutionContext(Solution originalSolution)
        {
            this.OriginalSolution = originalSolution;
            this.CurrentSolution = originalSolution;
        }

        public Solution CurrentSolution
        {
            get; private set;
        }

        public Solution OriginalSolution
        {
            get; private set;
        }

        public SolutionTransformStep Step(string name)
        {
            var result = new SolutionTransformStep(name);

            this.StepInitialized?.Invoke(result);

            return result;
        }

        public void ExecuteStep(string stepName, Action stepAction)
        {
            var step = this.Step(stepName);

            try
            {
                stepAction();
                step.Succeeded = true;
            }
            catch (Exception ex)
            {
                step.Write("Exception: {0}", ex.Message);
                step.Write("Full Exception: {0}", ex);
                step.Succeeded = false;
            }
        }

        public event Action<SolutionTransformStep> StepInitialized;

        public void Save()
        {
            this.OriginalSolution.Workspace.TryApplyChanges(this.CurrentSolution);
        }

        public bool TrySave(out Exception ex)
        {
            ex = null;
            bool result = false;

            try
            {
                this.Save();
                result = true;
            }
            catch (Exception exception)
            {
                ex = exception;
                result = false;
            }

            return result;
        }

        public void AddOrReplaceInterfaceMethod(DocumentId documentId, MethodDeclarationSyntax method)
        {
            this.UpdateDocument(p => p.Id == documentId,
                d =>
                {
                    var root = d.GetSyntaxRootAsync().Result;
                    var originalIface = root.DescendantNodesAndSelf().OfType<InterfaceDeclarationSyntax>().FirstOrDefault();
                    InterfaceDeclarationSyntax newIface = originalIface;

                    if (originalIface == null)
                    {
                        throw new InvalidOperationException($"No class found in document with id {documentId}");
                    }

                    MethodDeclarationSyntax existing = null;
                    if (originalIface.TryGetMethodByName(method.Identifier.ToString(), out existing))
                    {
                        newIface = newIface.ReplaceNode(existing, method);
                    }
                    else
                    {
                        newIface = newIface.AddMembers(method);
                    }

                    return root.ReplaceNode(originalIface, newIface).NormalizeWhitespace();
                });
        }

        public string GetNamespace(ProjectItemLocation location)
        {
            var proj = this.CurrentSolution.GetProject(location.Project);

            return string.Join(".", new string[] { proj.AssemblyName }.Concat(location.Folders).ToArray());
        }

        public void AddOrReplaceClassMethod(DocumentId documentId, MethodDeclarationSyntax method)
        {
            this.UpdateDocument(p => p.Id == documentId,
                d =>
                {
                    var root = d.GetSyntaxRootAsync().Result;
                    var originalClass = root.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>().FirstOrDefault();
                    ClassDeclarationSyntax newClass = originalClass;

                    if (originalClass == null)
                    {
                        throw new InvalidOperationException($"No class found in document with id {documentId}");
                    }

                    MethodDeclarationSyntax existing = null;
                    if (originalClass.TryGetMethodByName(method.Identifier.ToString(), out existing))
                    {
                        newClass = newClass.ReplaceNode(existing, method);
                    }
                    else
                    {
                        newClass = newClass.AddMembers(method);
                    }

                    return root.ReplaceNode(originalClass, newClass).NormalizeWhitespace();
                });
        }

        public string GetDocumentText(Func<Document, bool> selector)
        {
            return this.CurrentSolution.FindDocuments(selector).FirstOrDefault().GetTextAsync().Result.ToString();
        }

        public void UpdateDocument(Func<Document, bool> selector, Func<Document, SyntaxNode> updateFunc)
        {
            var doc = this.CurrentSolution.FindDocuments(selector).FirstOrDefault();

            if (doc != null)
            {
                var newRoot = updateFunc(doc);
                
                this.CurrentSolution = doc.WithSyntaxRoot(newRoot).Project.Solution;
            }
        }

        public void AddUsingStatement(DocumentId documentId, string @namespace)
        {
            // TODO: implement this appropriately
        }

        public void AddDocument(ProjectItemLocation location, string documentName, SyntaxNode root)
        {
            this.AddDocument(location.Project, location.Folders, documentName, root);
        }

        public void AddDocument(ProjectId projectId, string documentName, string content)
        {
            var newDocument = this.CurrentSolution.GetProject(projectId).AddDocument(documentName, content);
            this.CurrentSolution = newDocument.Project.Solution;
        }

        public void AddDocument(ProjectId project, List<string> folders, string name, SyntaxNode root)
        {
            var proj = this.CurrentSolution.GetProject(project);

            Document newDoc = proj.AddDocument(name, root, folders);

            this.CurrentSolution = newDoc.Project.Solution;
        }

        public bool TryFindClassDocument(string className, out Document result)
        {
            result = (from d in this.CurrentSolution.AllDocuments()
                      let cls = d.GetClass()
                      where cls != null && cls.Identifier.ToString() == className
                      select d).FirstOrDefault();

            return result != null;
        }
    }
}
