//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class ClassModel
    {
        private string displayText;

        public ClassModel(string classTypeName, DocumentId classDocumentId)
        {
            this.ClassTypeName = classTypeName;
            this.ClassDocumentId = classDocumentId;
        }

        public string ClassTypeName { get; private set; }

        public DocumentId ClassDocumentId { get; private set; }

        public string InterfaceTypeName { get; set; }

        public DocumentId InterfaceDocumentId { get; set; }

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(this.displayText))
                {
                    this.displayText = this.GetDefaultDisplayText();
                }

                return this.displayText;
            }
            set
            {
                this.displayText = value;
            }
        }

        private string GetDefaultDisplayText()
        {
            if (!string.IsNullOrEmpty(this.InterfaceTypeName))
            {
                return string.Format("{0} / {1}", this.ClassTypeName, this.InterfaceTypeName);
            }

            return this.ClassTypeName;
        }

        public override string ToString()
        {
            return this.DisplayText;
        }
    }
}
