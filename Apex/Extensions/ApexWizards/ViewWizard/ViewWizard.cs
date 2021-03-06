﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TemplateWizard;

namespace ApexWizards.ViewWizard
{

    /// <summary>
    /// The View Wizard.
    /// </summary>
    public class ViewWizard : IWizard
    {
        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {
        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                //  Create the wizard view.
                ViewWizardView view = new ViewWizardView();

                //  Set the view name.
                string viewName = string.Empty;
                replacementsDictionary.TryGetValue("$safeitemrootname$", out viewName);
                view.ViewModel.ViewName = viewName;

                //  Show the view.
                var result = view.ShowDialog();
                cancelled = result == null || result.Value == false;
                
                //  Update the custom parameters.
                replacementsDictionary.Add("$ViewModelType$", string.IsNullOrEmpty(view.ViewModel.ViewModelType) ? "object" : view.ViewModel.ViewModelType);
                replacementsDictionary.Add("$ViewCreatesViewModel$", view.ViewModel.ViewCreatesViewModel ? "1" : "0");
            }
            catch
            {
            }
        }

        /// <summary>
        /// Indicates whether the specified project item should be added to the project.
        /// </summary>
        /// <param name="filePath">The path to the project item.</param>
        /// <returns>
        /// true if the project item should be added to the project; otherwise, false.
        /// </returns>
        public bool ShouldAddProjectItem(string filePath)
        {
            return !cancelled;
        }

        /// <summary>
        /// Flag to indicate cancellation.
        /// </summary>
        private bool cancelled = false;
    }
}
