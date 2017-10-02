using System;

namespace SolutionRefactorMgr.Domain
{
    public class Enumerations
    {
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
            EAI,
            NA,
            Reports,
            UX
        }
    }
}
