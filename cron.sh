#!/bin/bash
ps auxw | grep RunUO.exe | grep -v grep > /dev/null
if [ $? != 0 ]; then
	cd $(dirname $(which $0) )
	mono RunUO.exe >> console.log
fi
