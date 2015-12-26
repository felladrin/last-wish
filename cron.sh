#!/bin/sh
ps auxw | grep RunUO.exe | grep -v grep > /dev/null
if [ $? != 0 ]; then
	mono RunUO.exe >> console.log
fi
