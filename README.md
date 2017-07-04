# ModernApp4Me

## About ModernApp4Me

ModernApp4Me is a framework library dedicated to the development of Windows Phone 8 and Windows Store applications. It is developed and maintained by [Smart&Soft](http://www.smartnsoft.com).

Its purpose is to speed up the development of Windows Phone 8, Windows Phone 8.1 and Windows Store applications. We are constantly adding news features to it.

**This project is entirely managed on Github.** It means that the documentation ia available on the [Github wiki](https://github.com/smartnsoft/ModernApp4Me/wiki) and that all issues should be opened/discussed on the [Github issue page](https://github.com/smartnsoft/ModernApp4Me/issues).

## Supported Platforms

### ModernApp4Me.Core

* Windows Phone 8 (Silverlight) (deprecated)
* Universal Windows / Phone 8.1 apps
* UWP apps

The **Core module** can be used to developp Windows Phone 8 and Windows Store applications and has the following nuget dependencies :
* FubarCoder.RestSharp.Portable.Core
* FubarCoder.RestSharp.Portable.WebRequest
* Newtonsoft.Json

### ModernApp4Me.Universal

* Universal Windows / Phone 8.1 apps
* UWP apps

The **Universal module** can be used to developp Universal Windows 8.1 apps or UWP (Windows 10) and has the following nuget dependencies :
* FubarCoder.RestSharp.Portable.Core
* FubarCoder.RestSharp.Portable.WebRequest
* ModernApp4Me.Core
* Newtonsoft.Json
* Q42.WinRT
* Q42.WinRT.Portable

### ModernApp4Me.WP8 (deprecated)

* Windows Phone 8 (Silverlight)

The **WP8 module** can be used to developp Windows Phone 8 (Silverlight) apps and has the following nuget dependencies :
* FubarCoder.RestSharp.Portable.Core
* FubarCoder.RestSharp.Portable.WebRequest
* ModernApp4Me.Core
* Newtonsoft.Json
* Q42.WinRT
* Q42.WinRT.Portable
* WPtoolkit

## Usage

Framework libraries releases are available on NuGet :
* [ModernApp4Me.Core](https://www.nuget.org/packages/ModernApp4Me.Core)
* [ModernApp4Me.Universal](https://www.nuget.org/packages/ModernApp4Me.Universal/)
* [ModernApp4Me.WP8](https://www.nuget.org/packages/ModernApp4Me.WP8)

## Documentation

The documentation (chm format) is available into the `doc` directory of each module.

For a full example see the **WP8.Sample** app in the repository or read the [Getting Started](https://github.com/smartnsoft/ModernApp4Me/wiki/Getting-Started) wiki page.

## Help and Support

If you have any questions feel free to contact us via [mail](mailto:modernapp4me@smartnsoft.com) or create a ticket on [Github](https://github.com/smartnsoft/ModernApp4Me/issues).

If you have a bug create an issue directly on [Github](https://github.com/smartnsoft/ModernApp4Me/issues).

## Contributing

## #How to

1. Fork it ( https://github.com/smartnsoft/ModernApp4Me/fork )
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request

### Contributors

* Andreas Saudemont
* Smart&Soft

## License

ModernApp4Me is available under the MIT license. See the LICENSE file for more info.

## Changelog

The changelog is available on the dedicated [Wiki page](https://github.com/smartnsoft/ModernApp4Me/wiki/Changelog).
