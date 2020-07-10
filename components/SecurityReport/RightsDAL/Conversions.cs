//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;

namespace RightsDAL
{
    public static class Conversions
    {
        public static string convertToGuid(bool dbmsOracle, object rawString)
        {
            if (!string.IsNullOrEmpty(rawString.ToString()))
            {
                if (rawString.GetType().Name == "Byte[]")
                {
                    if (!rawString.Equals(DBNull.Value))
                    {
                        byte[] value = (byte[])rawString;

                        if (dbmsOracle)
                        {
                            Guid guidFromOracle = new Guid(value).FlipEndian();
                            return guidFromOracle.ToString().Replace("-", "").ToUpper();
                        }
                        else
                        {
                            Guid guidFromOracle = new Guid(value);
                            return guidFromOracle.ToString().ToUpper();
                        }
                    }
                }
                else
                {
                    return rawString.ToString().ToUpper();
                }
            }

            return null;
        }

        public static string convertToGuid(bool dbmsOracle, Guid rawString)
        {
            if (!string.IsNullOrEmpty(rawString.ToString()))
            {
                if (dbmsOracle)
                {
                    if (!string.IsNullOrEmpty(rawString.ToString()) && !rawString.Equals(DBNull.Value))
                    {
                        Guid guidFromOracle = rawString.FlipEndian();
                        return guidFromOracle.ToString().Replace("-", "").ToUpper();
                    }
                }
                else
                {
                    return rawString.ToString().ToUpper();
                }
            }

            return null;
        }

        public static string convertToRaw(bool dbmsOracle, object value)
        {
            if (value != null)
            {
                if (dbmsOracle)
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        if (value.GetType().Name == "Guid")
                        {
                            Guid guid = new Guid(value.ToString());
                            return BitConverter.ToString(guid.ToByteArray()).Replace("-", "");
                        }
                        if (value.GetType().Name == "Byte[]")
                        {
                            Guid guid = new Guid((byte[])value);
                            return guid.ToString();
                        }
                        else if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            return value.ToString();
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(value.ToString()))
                {
                    return value.ToString();
                }
            }

            return null;
        }

        public static string convertToRaw(bool dbmsOracle, Guid value)
        {
            if (value != null)
            {
                if (dbmsOracle)
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        Guid guid = new Guid(value.ToString());
                        return BitConverter.ToString(guid.ToByteArray()).Replace("-", "");
                    }
                }
                else
                {
                    return value.ToString();
                }
            }

            return null;
        }

        public static string convertToRaw(bool dbmsOracle, byte[] value)
        {
            if (value != null && value.Length > 0)
            {
                if (dbmsOracle || value.GetType().Name == "Byte[]")
                {
                    Guid guid = new Guid(value);
                    return guid.ToString();
                }
                else
                {
                    return value.ToString();
                }
            }

            return null;
        }
    }
}
