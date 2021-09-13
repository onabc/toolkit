using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
[assembly: XmlnsPrefix("http://www.toolkit.org/library", "toolkit")]
[assembly: XmlnsDefinition("http://www.toolkit.org/library", "Toolkit.Widget.Controls")]
[assembly: XmlnsDefinition("http://www.toolkit.org/library", "Toolkit.Widget.Converters")]
[assembly: XmlnsDefinition("http://www.toolkit.org/library", "Toolkit.Widget.Dependencies")]
[assembly: XmlnsDefinition("http://www.toolkit.org/library", "Toolkit.Widget.Attached")]
[assembly: XmlnsDefinition("http://www.toolkit.org/library", "Toolkit.Widget.Commands")]
[assembly: XmlnsDefinition("http://www.toolkit.org/library", "Toolkit.Widget.Extensions")]