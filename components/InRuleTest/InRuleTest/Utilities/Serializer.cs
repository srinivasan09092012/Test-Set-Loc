//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HP.HSP.UA3.Core.UX.Common.Utilities
{
    /// <summary>
    /// Provides common data serialization operations.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// JSON serializes an object into a string.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The object.</param>
        /// <returns>JSON serialized object string.</returns>
        public static string JsonSerialize<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, value);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// JSON deserializes a string back into an object type.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="xml">The JSON string.</param>
        /// <returns>The object of type T.</returns>
        public static T JsonDeserialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("value");
            }

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(ms);
            }
        }

        /// <summary>
        /// Xml serializes an object into an xml string.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The object.</param>
        /// <param name="xmlWriterSettings">Optional xml writer settings.</param>
        /// <returns>Xml serialized object string.</returns>
        public static string XmlSerialize<T>(T value, XmlWriterSettings xmlWriterSettings = null)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlWriterSettings settings = xmlWriterSettings ?? new XmlWriterSettings()
            {
                Encoding = new UnicodeEncoding(false, false),
                Indent = false,
                OmitXmlDeclaration = false
            };

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }

                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Xml deserializes a string back into an object type.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="xml">The xml string.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <param name="xmlReaderSettings">Optional xml reader settings.</param>
        /// <returns>The object of type T.</returns>
        public static T XmlDeserialize<T>(string xml, string defaultNamespace, XmlReaderSettings xmlReaderSettings = null)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("xml");
            }

            XmlSerializer serializer;
            if (!string.IsNullOrEmpty(defaultNamespace))
            {
                serializer = new XmlSerializer(typeof(T), defaultNamespace);
            }
            else
            {
                serializer = new XmlSerializer(typeof(T));
            }

            XmlReaderSettings settings = xmlReaderSettings ?? new XmlReaderSettings();

            using (StringReader textReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        /// <summary>
        /// Binary serializes an object into a byte array.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The object.</param>
        /// <returns>Binary serialized byte array.</returns>
        public static byte[] BinarySerialize<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, value);
                return stream.GetBuffer();
            }
        }

        /// <summary>
        /// Binary deserializes a byte array back into an object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The byte array.</param>
        /// <returns>The object of type T.</returns>
        public static T BinaryDeserialize<T>(byte[] value)
        {
            if (value == null || value.Length == 0)
            {
                throw new ArgumentException("value");
            }

            using (MemoryStream stream = new MemoryStream(value))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Binary serializes an object into a base64 string.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The object.</param>
        /// <returns>Binary serialized base64 string.</returns>
        public static string BinarySerializeToBase64<T>(T value)
        {
            return Convert.ToBase64String(BinarySerialize<T>(value));
        }

        /// <summary>
        /// Binary deserializes a base64 string back into an object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The byte array.</param>
        /// <returns>The object of type T.</returns>
        public static T BinaryDeserializeFromBase64<T>(string value)
        {
            return BinaryDeserialize<T>(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Makes a binary serialzied clone of an object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The object.</param>
        /// <returns>Cloned object of type T.</returns>
        public static T Clone<T>(T value)
        {
            return BinaryDeserialize<T>(BinarySerialize<T>(value));
        }
    }
}
