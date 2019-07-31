using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftpFileTransfer
{
    public class FileTransferModel
    {
        public string SftpPathWithoutFileName { get; set; }
        public string FileServerPathWithoutFileName { get; set; }
    }
}
