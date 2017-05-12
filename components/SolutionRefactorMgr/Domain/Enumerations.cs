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

        public enum EditModeTypes
        {
            Copy,
            Inline
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
