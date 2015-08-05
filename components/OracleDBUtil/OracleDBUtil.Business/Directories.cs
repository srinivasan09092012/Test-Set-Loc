using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDBUtil.Business
{
    public class Directories
    {
        public Directories(string ConnectionName)
        {
            if (!Directory.Exists(sourceRoot))
                throw new Exception("Source Root directory does not exist (" + sourceRoot + ") , configure source root correctly in App.config file");

            _workingDirectory = sourceRoot + ConnectionName + extrationExt;
        }

        public string TableDir { get { return _workingDirectory + @"\Tables"; } }
        public string SequenceDir { get { return _workingDirectory + @"\Seq"; } }
        public string ProcDir { get { return _workingDirectory + @"\Procs"; } }
        public string PackageSpecDir { get { return _workingDirectory + @"\Packages\Spec"; } }
        public string PackageBodyDir { get { return _workingDirectory + @"\Packages\Body"; } }

        private string _workingDirectory;
        public string WorkingDirectory {  get { return _workingDirectory; } }

        private string sourceRoot { get { return ConfigurationManager.AppSettings["SourceRoot"]; } }
        private string extrationExt { get { return ConfigurationManager.AppSettings["ExtractionExt"];  } }
    }
}
