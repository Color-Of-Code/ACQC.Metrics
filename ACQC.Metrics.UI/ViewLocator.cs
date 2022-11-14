// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ACQC.Metrics.UI.ViewModels;

namespace ACQC.Metrics.UI
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            return type != null
                ? (Control)Activator.CreateInstance(type)
                : (IControl)new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}
