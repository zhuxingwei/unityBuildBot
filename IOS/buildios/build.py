#!/user/bin/python
#coding = utf-8

import json
import subprocess

with open('../../config.json') as json_file:
    data = json.load(json_file)
    xcode_project_path = data['buildTargetFolder'] + "/iOSBuild"
    unity_path = data['unityPath']

    print("start build ios")
    
    subprocess.call(unity_path + " -quit " + " -batchmode " + " -projectPath=" + xcode_project_path + " -executeMethod Build.IOS.BuildIOSPipeline.Build", shell=True)
    
    print("build ios done!")
    
    print("start build ipa")

    build_ipa_command = "xcodebuild -project Unity-iPhone.xcodeproj -target Unity-iPhone -configuration Release -sdk iphoneos build"
    code_sign_identity = data['ios']['codeSignIdentity']
    provisioning_profile = data['ios']['provisioning_profile']
    
    if code_sign_identity != "" and provisioning_profile != "":
        build_ipa_command = build_ipa_command + " CODE_SIGN_IDENTITY=" + code_sign_identity
        build_ipa_command = build_ipa_command + " PROVISIONING_PROFILE=" + provisioning_profile
    
    build_ipa_command = build_ipa_command + " -allowProvisioningUpdates"
    subprocess.call(build_ipa_command, cwd=xcode_project_path, shell=True)
    
    print("build ipa done!")
    
    