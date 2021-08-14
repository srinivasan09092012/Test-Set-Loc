//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace RightsDAL
{
    public static class Extensions
    {
        public static Guid FlipEndian(this Guid guid)
        {
            var newBytes = new byte[16];
            var oldBytes = guid.ToByteArray();

            for (var i = 8; i < 16; i++)
            {
                newBytes[i] = oldBytes[i];
            }

            newBytes[3] = oldBytes[0];
            newBytes[2] = oldBytes[1];
            newBytes[1] = oldBytes[2];
            newBytes[0] = oldBytes[3];
            newBytes[5] = oldBytes[4];
            newBytes[4] = oldBytes[5];
            newBytes[6] = oldBytes[7];
            newBytes[7] = oldBytes[6];

            return new Guid(newBytes);
        }
    }
}
