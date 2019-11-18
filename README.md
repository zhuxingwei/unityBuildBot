# unityBuildBot

### How to build

Before building your project using this tool, please ensure you have a Unity Editor installed on your machine. For IOS build, please install Xcode on your Mac first. For Android build, please install Android Studio first.

Next, please open the configuration file *config.json* to determine all build settings for your project:

- The *buildScene* should be the name of the target unity scene (Currently supports only one target scene per build). 

- The *buildTargetFolder* is the folder for the final built Xcode/Gradle projects.

- The *unityPath* is the absolution path of your installed Unity Editor.

- In the *ios* section, you should provide the "codeSignIdentity" and "provisioning_profile" of your Apple Developer Account. You can also set them in Xcode before the first build, then these parameters will be rememberd by Xcode for the next builds.

- In the *android* section, you should provide the absolute path of your "keystore" and "releaseSign" files. Specifically, in the "releaseSign" file, you should include the release key part of the build.gradle file, which may looks like:

```
release {
           storeFile file('PATH OF YOUR KEYSTORE')
           storePassword 'PASSWORD OF YOUR KEYSTORE'
           keyAlias 'KEY ALIAS'
           keyPassword 'PASSWORD OF YOUR KEY'
}
```


Finally, just run build.sh with specific platform arguments (i.e., android or ios). Leave the platform argument empty if you want to build both platform at once.
