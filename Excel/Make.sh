#!/bin/bash

export GOPROXY=https://goproxy.cn,direct

go build -v -o ./tabtoy github.com/davyxu/tabtoy

./tabtoy -mode=v3 \
-index=Index.xlsx \
-package=main \
-csharp_out=../Unity/Assets/JKFrame/Scripts/10.Table/table_gen.cs \
-binary_out=../Unity/Assets/JKFrame/Table/table_gen.bin \

if [ $? -ne 0 ] ; then
	read -rsp $'Errors occurred...\n' ; 
	exit 1 
fi

echo 导表完成，按任意键继续
read -n 1