# 海康摄像头SDK

## 使用方法

nuget安装即可,按照海康官方的demo调用,所有接口相同

## 一些还没填好的坑

1. Net framwork 下不会释放nativedll(正在寻找解决方案,目前只能手动复制dll过去)
2. Net Core下无法产生回调(一个临时的解决方案是使用我封装的EasySDK,我用间接调用.net framwork的方式,避开了这个问题,而且调用起来更简单了,构建对象传入链接信息,然后不断调用ReadImage获取图片即可)
*不过这个解决方案的一些参数还不够合理,未来我有概率修改部分接口的参数设定.*
3. xml注释不足(逐渐补充中,不过太多了,我时间不足.)

## 打包指令

dotnet pack -p:PackageVersion=2.1.50

<!--  -p:NuspecFile=~/projects/app1/project.nuspec -p:NuspecBasePath=~/projects/app1/nuget -->
