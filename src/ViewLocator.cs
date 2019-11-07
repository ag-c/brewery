// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Brewery.ViewModels;

namespace Brewery
{
    /// <inheritdoc />
    public class ViewLocator : IDataTemplate
    {
        /// <summary>
        /// Gets a value indicating whether the data template supports recycling of the generated
        /// control.
        /// </summary>
        public bool SupportsRecycling => false;

        /// <summary>Creates the control.</summary>
        /// <param name="param">The parameter.</param>
        /// <exception cref="Exception">Throws exception if can't get type's name</exception>
        /// <returns>The created control.</returns>
        public IControl Build(object data)
        {
            var name = data.GetType().FullName?.Replace("ViewModel", "View");
            var type = Type.GetType(name ?? throw new Exception("Can't get name of control"));

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }

            return new TextBlock { Text = $"Not Found: {name}"};
        }

        /// <summary>
        /// Checks to see if this data template matches the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// True if the data template can build a control for the data, otherwise false.
        /// </returns>
        public bool Match(object data) => data is ViewModelBase;
    }
}