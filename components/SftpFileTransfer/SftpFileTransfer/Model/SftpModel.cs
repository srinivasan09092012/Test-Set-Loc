using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftpFileTransfer
{
    public class SftpModel
    {
        public SftpModel(string serverIP, string userName, string password, int portNo, string sshHostKeyFingerprint)
        {
            this.ServerIP = serverIP;
            this.UserName = userName;
            this.Password = password;
            this.PortNo = portNo;
            this.SshHostKeyFingerprint = sshHostKeyFingerprint;
        }
        public string ServerIP { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PortNo { get; set; }
        public string SshHostKeyFingerprint { get; set; }
    }
}
