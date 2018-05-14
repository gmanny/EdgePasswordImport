A very simple tool that imports passwords from a CSV file into Microsoft Edge and Internet Explorer browsers.

# File format

A CSV file should have a header row, and in that row there should be all three of these headers:

```csv
url,username,password
```

Chrome browser [exports passwords](https://www.macrumors.com/how-to/how-to-export-your-passwords-and-login-data-from-google-chrome/) in the acceptable format as-is. While if you want to migrate your passwords from one machine with Edge browser to another, possibly because of a [sync bug](https://answers.microsoft.com/en-us/windows/forum/windows_10-other_settings/windows-10-1709-microsft-edge-not-sync-passwords/372e7264-c438-4a1c-80c2-36d453e1c112), you'll need to first export them into Comma delimited text file with something like this [NirSoft utility](http://www.nirsoft.net/internet_explorer_password.html) with the CSV header enabled in Options menu and then change the first line of the file to be compatible with this tool from original 

```csv
Entry Name,Type,Stored In,User Name,Password,Password Strength
```

to

```csv
url,type,store,username,password,strength
```

You can import passwords from other sources by performing similar header modifications.

# Usage

Either double click the EdgePasswordImport.exe file and select a CSV file you want to import, or use a command line and run `EdgePasswordImport.exe filename.csv` to import the specified file.

# Compatibility

This tool can only run on Windows 10.

# Building the code

You need to have Visual Studio 2017 Community 15.7 or better installed with the SDK for UWP apps because the tool uses UWP APIs. Then, if you've got a UWP SDK for Windows version other than `10.0.16299.0`, you need to change the `Windows.winmd` in `EdgePasswordImport.csproj` file to the appropriate version of SDK.

After that, the tool can be built with visual studio.

# License

Dual licensed

[Microsoft Public License (MS-PL)](http://www.opensource.org/licenses/MS-PL)
[Apache License, Version 2.0](http://opensource.org/licenses/Apache-2.0)