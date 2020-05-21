//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.BatchProcessingFactory.Core.Batch;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class BatchExecutorContext : BatchExecutor
    {
        public BatchExecutorContext(string batchName)
            : base(batchName)
        {
        }
    }
}
