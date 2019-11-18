#!/bin/bash

echo start building

platform=$1

if [[ -n "$platform" ]]; then
    if [ "$platform" == "android" ]; then
        cd ./Android/buildandroid
        python build.py
    elif [ "$platform" == "ios" ]; then
        cd ./IOS/buildios/
        python build.py
    fi
else
    cd ./Android/buildandroid
    python build.py
    cd ../../
    cd ./IOS/buildios/
    python build.py
fi
        