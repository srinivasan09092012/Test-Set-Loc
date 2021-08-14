//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using ViewProjectionLoader.Core.Services;

namespace ViewProjectionLoader.Core.Messages
{
    public class BasLoadedMessage
    {
        public BasLoadedMessage(BasLoaderResult result)
        {
            this.LoadResult = result;
        }

        public BasLoaderResult LoadResult { get; private set; }
    }
}
