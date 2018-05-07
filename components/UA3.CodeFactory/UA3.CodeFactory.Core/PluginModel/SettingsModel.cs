//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace UA3.CodeFactory.Core.PluginModel
{
    public abstract class SettingsModel : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private Dictionary<string, object> values = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        public SettingsModel()
        {
        }

        [Browsable(false)]
        public bool IsValid
        {
            get { return Get<bool>("IsValid"); }
            protected set { this.Set<bool>(value, "IsValid"); }
        }

        public ICollection<ValidationResult> Validate()
        {
            ValidationContext context = new ValidationContext(this);
            ICollection<ValidationResult> result = new List<ValidationResult>();

            this.IsValid = Validator.TryValidateObject(this, context, result);

            this.Validated?.Invoke(result);

            return result;
        }

        protected void NotifyChanging(string propertyName)
        {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected void NotifyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T Get<T>([CallerMemberName]string propertyName = "")
        {
            object result = default(T);
            if (values.TryGetValue(propertyName, out result))
            {
                return (T)result;
            }
            else
            {
                return default(T);
            }
        }

        protected void Set<T>(T value, [CallerMemberName]string propertyName = "")
        {
            NotifyChanging(propertyName);
            values[propertyName] = value;
            NotifyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event Action<ICollection<ValidationResult>> Validated;
    }
}
