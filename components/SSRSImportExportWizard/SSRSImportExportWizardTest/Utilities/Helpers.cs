using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SSRSImportExportWizardTest.Utilities
{
    /// <summary>
    /// Unit tests utilities helper methods.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Execute an action for each child control of a control. 
        /// </summary>
        /// <example><code>
        /// control.ForAllControls(control =>
        /// {
        ///     Snapshot snapShot = new Snapshot(control);
        ///     Assert.IsTrue(snapShot.Matches());
        /// });
        /// </code></example>
        /// <param name="parent">Origin object.</param>
        /// <param name="action">Delegate action beign performed.</param>
        /// <remarks>No action will be taken on origin object control.</remarks>
        public static void ForAllControls(this Control parent, Action<Control> action)
        {
            foreach (Control control in parent.Controls)
            {
                action(control);
                ForAllControls(control, action);
            }
        }
        /// <summary>
        /// Get a path within the project.
        /// </summary>
        /// <param name="control"></param>
        /// <returns>Path of form control as <c>..\FormName\ControlName</c></returns>
        public static string GetPath(Control control)
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            path = new Uri(path).LocalPath + "\\" 
                + control.FindForm().Name.ToString() + "\\"
                + "Snapshots\\";
            CheckPath(path);
            path += control.Name.ToString();
            return path;
        }
        /// <summary>
        /// If a directory path does not exist create it.
        /// </summary>
        /// <param name="path">The path to check.</param>
        public static void CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void ReverseChildIndex(Control control)
        {
            var list = new List<Control>();
            foreach (Control i in control.Controls)
            {
                list.Add(i);
            }
            var total = list.Count;
            for (int i = 0; i < total / 2; i++)
            {
                var j = control.Controls.GetChildIndex(list[i]);
                var k = control.Controls.GetChildIndex(list[total - 1 - i]);

                control.Controls.SetChildIndex(list[i], k);
                control.Controls.SetChildIndex(list[total - 1 - i], j);
            }
        }
    }
}
