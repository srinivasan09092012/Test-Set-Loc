using System;
using System.Drawing;
using System.Windows.Forms;
using SSRSImportExportWizardTest.Utilities;
using SSRSImportExportWizard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSRSImportExportWizardTest
{
    [TestClass]
    public class UserInterfaceSnapshot
    {
        Control control;
        Snapshot snapShot;

        [TestMethod]
        public void ImportExportMain()
        {
            control = new ImportExportMain();
            control.Visible = true;
            control.Hide();
            snapShot = new Snapshot(control);
            Assert.IsTrue(snapShot.Matches());
            control.ForAllControls(control =>
            {
                snapShot = new Snapshot(control);
                Assert.IsTrue(snapShot.Matches());
            });
        }

        [TestMethod]
        public void DataSourceView()
        {
            control = new DataSourceView();
            control.Visible = true;
            control.Hide();
            snapShot = new Snapshot(control);
            Assert.IsTrue(snapShot.Matches());
            control.ForAllControls(control =>
            {
                snapShot = new Snapshot(control);
                Assert.IsTrue(snapShot.Matches());
            });
        }

        [TestMethod]
        public void ExportControl()
        {
            Assert.Inconclusive();
            //control = new ExportControl();
            //control.Visible = true;
            //control.Hide();
            //snapShot = new Snapshot(control);
            //Assert.IsTrue(snapShot.Matches());
            //control.ForAllControls(control =>
            //{
            //    snapShot = new Snapshot(control);
            //    Assert.IsTrue(snapShot.Matches());
            //});
        }

        [TestMethod]
        public void ExportReportView()
        {
            control = new ExportReportView();
            control.Visible = true;
            control.Hide();
            snapShot = new Snapshot(control);
            Assert.IsTrue(snapShot.Matches());
            control.ForAllControls(control =>
            {
                snapShot = new Snapshot(control);
                Assert.IsTrue(snapShot.Matches());
            });
        }

        [TestMethod]
        public void ImportControl()
        {
            Assert.Inconclusive();
            //control = new ImportControl();
            //control.Visible = true;
            //control.Hide();
            //snapShot = new Snapshot(control);
            //Assert.IsTrue(snapShot.Matches());
            //control.ForAllControls(control =>
            //{
            //    snapShot = new Snapshot(control);
            //    Assert.IsTrue(snapShot.Matches());
            //});
        }

        [TestMethod]
        public void ImportReportView()
        {
            control = new ImportReportView();
            control.Visible = true;
            control.Hide();
            snapShot = new Snapshot(control);
            Assert.IsTrue(snapShot.Matches());
            control.ForAllControls(control =>
            {
                snapShot = new Snapshot(control);
                Assert.IsTrue(snapShot.Matches());
            });
        }

        [TestMethod]
        public void ReportCompare()
        {
            control = new ReportCompare();
            control.Visible = true;
            control.Hide();
            snapShot = new Snapshot(control);
            Assert.IsTrue(snapShot.Matches());
            control.ForAllControls(control =>
            {
                snapShot = new Snapshot(control);
                Assert.IsTrue(snapShot.Matches());
            });
        }

        [TestMethod]
        public void ViewDataSource()
        {
            Assert.Inconclusive();
            //control = new ViewDataSource();
            //control.Visible = true;
            //control.Hide();
            //snapShot = new Snapshot(control);
            //Assert.IsTrue(snapShot.Matches());
            //control.ForAllControls(control =>
            //{
            //    snapShot = new Snapshot(control);
            //    Assert.IsTrue(snapShot.Matches());
            //});
        }
    }
}
