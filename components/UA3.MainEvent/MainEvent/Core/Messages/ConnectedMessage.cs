//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace MainEvent.Core.Messages
{
    public class ConnectedMessage
    {
        public ConnectedMessage(ConnectionStringSettings connectedTo)
        {
            this.ConnectedTo = connectedTo;
        }

        public ConnectionStringSettings ConnectedTo { get; private set; }
    }
}