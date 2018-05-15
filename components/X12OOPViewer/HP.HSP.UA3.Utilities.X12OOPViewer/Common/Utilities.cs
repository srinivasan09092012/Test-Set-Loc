using System;

namespace HP.HSP.UA3.Utilities.X12OOPViewer.Common
{
    public static class Utilities
    {
        public static string GenerateNewID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
