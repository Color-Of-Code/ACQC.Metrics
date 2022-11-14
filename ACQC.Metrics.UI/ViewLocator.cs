using System;
using System.ComponentModel;
using ACQC.Metrics.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace ACQC.Metrics.UI
{
    public class ViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {

            return data is ViewModelBase;
        }
    }
}
