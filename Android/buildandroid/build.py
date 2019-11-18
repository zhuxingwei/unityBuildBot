#!/user/bin/python
#coding = utf-8

import json
import subprocess

with open('../../config.json') as json_file:
    data = json.load(json_file)
    xcode_project_path = data['buildTargetFolder'] + "/androidBuild"
    unity_path = data['unityPath']

    print("start build android")

    subprocess.call(unity_path + " -quit " + " -batchmode " + " -projectPath=" + xcode_project_path + " -executeMethod Build.Android.BuildAndroidPipeline.Build", shell=True)

    print("build android done!")
    
    print("prepare files")
    build_bundle_path = xcode_project_path + "/" + data['productName']
    keystore_path = data['android']['keystore']
    subprocess.call("mv * ../", cwd=build_bundle_path, shell=True)
    subprocess.call('rm -rf "' + data['productName'] + '"', cwd=xcode_project_path, shell=True)
    print("cp -R ../gradle/gradle " + '"' + xcode_project_path + '"')
    subprocess.call("cp -R ../gradle/gradle " + '"' + xcode_project_path + '"', shell=True)
    subprocess.call("cp -R ../gradle/gradlew " + '"' + xcode_project_path + '"', shell=True)
    subprocess.call("cp -R ../gradle/gradlew.bat " + '"' + xcode_project_path + '"', shell=True)
    subprocess.call("cp -R " + '"' + keystore_path + '" ' + '"' + xcode_project_path + '"', shell=True)
    subprocess.call("chmod +x gradlew", cwd=xcode_project_path, shell=True)

    release_sign = ""
    release_sign_path = data['android']['releaseSign']
    with open(release_sign_path) as sign_file:
        release_sign = sign_file.read()
    
    content = ""
    with open(xcode_project_path + "/build.gradle") as gradle_file:
        content = gradle_file.read()
        content = content.replace("release {\n//            storeFile file('./connectApp.keystore')  \n//            storePassword 'connectApp'\n//            keyAlias 'connectApp'\n//            keyPassword 'connectApp'\n        }",
                                  release_sign)

    with open(xcode_project_path + "/build.gradle", "w") as gradle_file:
        gradle_file.write(content)
    
    print("start build apk")
    
    subprocess.call("./gradlew assembleRelease", cwd=xcode_project_path, shell=True)
    
    
    print("build apk done!")
    
    