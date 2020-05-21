//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.BatchProcessingFactory.Core.Batch;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    [BatchJob("NISTMongoAtlasExtractPOC.Ctrl")]
    public class BatchProcessController : BatchFactory
    {
        public BatchProcessController()
        {
            this.AddStep(new BatchJobExtractStep());
        }
    }
}