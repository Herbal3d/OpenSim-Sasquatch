# git clone

get or update source from git

 `git clone git://opensimulator.org/git/opensim`
	
change to dotnet6 test branch

 `git checkout dotnet6`


# Building on Windows

## Requirements
  For building under Windows, the following is required:
  * [Microsoft DotNet 6.0](https://dotnet.microsoft.com/en-us/download), version 6.0 or later. 

  dotnet 6.0 is the LTS version and is recommended.

### Building
 Prebuild is no longer used.  There is a top level Solution (sln) and csproj files for each
 of the projects in the solution.  To run a build use either Visual Studio Community (recommended on Windows)
 or from a CLI run:`
 
 dotnet build --configuration Debug
 dotnet build --configuration Release

Either command will do a NuGet restore (dotnet restore) to restore any required NuGet package references prior to
kicking off a build using a current version of msbuild.  The Csproj and SLN files are all designed to use the new
format for Msbuild which is simplified and really directly replaces what prebuild provided.

Load the generated OpenSim.sln into Visual Studio and build the solution.

copy file bin\System.Drawing.Common.dll.win to bin\System.Drawing.Common.dll

Configure, see below

The resulting build will be generated to ./build/{Debug|Release}/

# Building on Linux / Mac

## Requirements

 *	[Mono > 5.0](https://www.mono-project.com/download/stable/#download-lin)
 *	On some Linux distributions you may need to install additional packages.
 * [Microsoft DotNet 6.0](https://dotnet.microsoft.com/en-us/download), version 6.0 or later. 
  dotnet 6.0 is the LTS version and is recommended.

### Building
 Prebuild is no longer used.  There is a top level Solution (sln) and csproj files for each
 of the projects in the solution.  To run a build from a CLI run:
 
 dotnet build --configuration Debug
 dotnet build --configuration Release

Either command will do a NuGet restore (dotnet restore) to restore any required NuGet package references prior to
kicking off a build using a current version of msbuild.  The Csproj and SLN files are all designed to use the new
format for Msbuild which is simplified and really directly replaces what prebuild provided.

Configure. See below

The resulting build will be generated to ./build/{Debug|Release}/

For rebuilding and debugging use the dotnet command options
  *  clean:  `dotnet clean
  *  restore: dotnet restore
  *  debug:   dotnet build --configuration Debug
  *  release: dotnet build --configuration Release

# Configure #
## Standalone mode ##
Copy `OpenSim.ini.example` to `OpenSim.ini` in the `bin/` directory, and verify the `[Const]` section, correcting for your case.

On `[Architecture]` section uncomment only the line with Standalone.ini if you do now want HG, or the line with StandaloneHypergrid.ini if you do

copy the `StandaloneCommon.ini.example` to `StandaloneCommon.ini` in the `bin/config-include` directory.

The StandaloneCommon.ini file describes the database and backend services that OpenSim will use, and is set to use sqlite by default, which requires no setup.


## Grid mode ##
Each grid may have its own requirements, so FOLLOW your Grid instructions!
in general:
Copy `OpenSim.ini.example` to `OpenSim.ini` in the `bin/` directory, and verify the `[Const]` section, correcting for your case
 
On `[Architecture]` section uncomment only the line with Grid.ini if you do now want HG, or the line with GridHypergrid.ini if you do

and copy the `GridCommon.ini.example` file to `GridCommon.ini` inside the `bin/config-include` directory and edit as necessary



# References

* http://opensimulator.org/wiki/Configuration
