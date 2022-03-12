//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HPP.Maintainability.CodeMaintainabilityMonitor
{
    public static class FilterAndSortXmlDataStep
    {
        public static void Execute()
        {
            Console.WriteLine("Hello World!");
            GetMethodsUnderMaintainabilityIndexLimit();
        }

        public static void GetMethodsUnderMaintainabilityIndexLimit()
        {

            string[] inputFiles = Directory.GetFiles(MaintainabilityConstants.Paths.InputFolder, MaintainabilityConstants.XmlSearchPatern);

            foreach (string inputFile in inputFiles)
            {
                try
                {
                    List<XElement> filteredResults = new List<XElement>();
                    List<XElement> methodsUnderMaintainabilityIndex = new List<XElement>();
                    List<Method> metodos = new List<Method>();
                    string fileName = string.Empty;
                    fileName = Path.GetFileName(inputFile);

                    XElement xml = XElement.Load(inputFile);
                    List<XElement> results = xml.Descendants(MaintainabilityConstants.XmlDescendants.Method).ToList();

                    foreach (XElement result in results)
                    {
                        FilterOutServiceReferenceAndTestsMetrics(filteredResults, result);
                    }

                    foreach (XElement result in filteredResults)
                    {
                        FilterOutMaintainabilityIndexAboveThreshold(methodsUnderMaintainabilityIndex, result);
                    }

                    metodos = DeserializeAndSortMethodList(methodsUnderMaintainabilityIndex, metodos);
                    SerializeListToXml(metodos, fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format(MaintainabilityConstants.Error, inputFile));
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static T FromXElement<T>(this XElement xElement)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }

        public static void SerializeListToXml(List<Method> objList, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Method>));
            TextWriter FileStream = new StreamWriter(string.Format(MaintainabilityConstants.Paths.OutputFolder, fileName));
            serializer.Serialize(FileStream, objList);
            FileStream.Close();
        }

        private static List<Method> DeserializeAndSortMethodList(List<XElement> methodsUnderMaintainabilityIndex, List<Method> metodos)
        {
            foreach (XElement method in methodsUnderMaintainabilityIndex)
            {
                Method metodo = method.FromXElement<Method>();
                metodos.Add(metodo);
            }

            metodos = metodos.OrderBy(m => m.Metrics.FirstOrDefault().Value).ToList();
            return metodos;
        }

        private static void FilterOutMaintainabilityIndexAboveThreshold(List<XElement> methodsUnderMaintainabilityIndex, XElement result)
        {
            XElement metrics = result.Descendants(MaintainabilityConstants.XmlDescendants.Metrics).FirstOrDefault();
            XNode maintainabilityNode = metrics.FirstNode;
            int value = int.Parse((maintainabilityNode as XElement).Attribute(MaintainabilityConstants.XmlAttributes.Value).Value);

            if (value < MaintainabilityConstants.MaintainabilityThreshold)
            {
                methodsUnderMaintainabilityIndex.Add(result);
            }
        }

        private static void FilterOutServiceReferenceAndTestsMetrics(List<XElement> filteredResults, XElement result)
        {
            string completeFileName = result.Parent.ToString();

            if (!completeFileName.Contains(MaintainabilityConstants.FiltersForFileFullName.ServiceReference) && !completeFileName.Contains(MaintainabilityConstants.FiltersForFileFullName.Tests))
            {
                filteredResults.Add(result);
            }
        }
    }
}
