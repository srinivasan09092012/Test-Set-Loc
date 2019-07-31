using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftpFileTransfer
{
    public class SftpFileTransferModel
    {
        public SftpModel SftpConfig;
        public FileTransferModel[] DownloadFromSftpConfig;
        public FileTransferModel[] UploadToSftpConfig;
    }
}
