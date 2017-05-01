using System;

namespace SolutionRefactorMgr.Domain
{
    public class Enumerations
    {
        [Serializable]
        public enum BranchTypes
        {
            Dev,
            Main
        }

        [Serializable]
        public enum ProjectTypes
        {
            All,
            API,
            BAS,
            Batch,
            UX
        }
    }
}
