# 安装
1. win10 启用关闭Windows功能，开启 windows虚拟机监控程序平台 适用于Linux的Windows子系统
2. Win10 更新，开启Windows预览体验计划，选在快速更新
3. 安装Linux子系统 [官方文档](https://docs.microsoft.com/zh-cn/windows/wsl/install-win10#install-your-linux-distribution-of-choice)
   1. 从Microsoft Store下载安装
   2. 手动命令安装,适用于应用商店不可用的情况 [官方文档](https://docs.microsoft.com/zh-cn/windows/wsl/install-manual)
   3. 下载并手动解压安装。适用于Windows server 2019或更高版本。
      1. 使用该网址下载安装包 https://aka.ms/wsl-ubuntu-1804 或通过[官方文档](https://docs.microsoft.com/zh-cn/windows/wsl/install-manual)手动下载你需要的Linux系统
      2. 将想在文件放到你想安装的的目录上，并执行命令
   
         `Rename-Item .\Ubuntu.appx` 

         `.\Ubuntu.zipExpand-Archive .\Ubuntu.zip .\Ubuntu` 
         
         将安装包转换为压缩包并解压
      3. 执行解压后的Exe文件进行安装
# 基本命令与特性
#### 1903以上的win10
- `wsl -l -v`查看以安装的分发版
- `wsl --set-version <Distro> 2`设置单个分发版为wsl2
- `wsl --set-default-version 2`设置分发版都默认为wsl2
- `wsl -s <DistributionName>`设置默认wsl的分布版
- `wsl --unregister <DistributionName>`卸载指定的分发版 
- `wsl -u <Username>`以特定用户执行分发版
- `wsl -d <DistributionName>`运行指定的分发版
- `[distro.exe] /?` 查看特定分发版exe程序命令
- `wsl --shutdown` 终止所有wsl实例

#### 迁移

导出,Distro为分发版名称
`wsl --export <Distro> <./outputdir/name.tar>`
导入
`wsl --import <NewDistroName> <ImportDir> <name.tar>`



#### 低于1903的win10
- `wslconfig /list` 列出可用的分发版
- `wslconfig /list /all` 列出所有分发版
- `wslconfig /setdefault <DistributionName>` 设置默认wsl的分布版
- `wslconfig /unregister <DistributionName>` 卸载指定的分发版 

#### 文件位置
`%LOCALAPPDATA%\Packages\<PackageFamilyName>\LocalState\<disk>.vhdx`

#### 调整wsl2 vhd的大小
```shell
# windows command
diskpart
Select vdisk file="<pathToVHD>"
expand vdisk maximum="<sizeInMegaBytes>"

# linux command
sudo mount -t devtmpfs none /dev
mount | grep ext4
# 复制此项的名称，如下所示：/dev/sdXX（X 表示任何其他字符）
sudo resize2fs /dev/sdXX
# 请确保使用前面复制的值，并且可能需要使用：apt install resize2fs。
```

# 在Linux子系统中配置wsl启动设置
在/etc/wsl.conf中配置，如果没有这个文件可以自行创建一个。内容如下：
```conf
# Enable extra metadata options by default
[automount]
enabled = true
root = /windir/
options = "metadata,umask=22,fmask=11"
mountFsTab = false

# Enable DNS – even though these are turned on by default, we’ll specify here just to be explicit.
[network]
generateHosts = true
generateResolvConf = true
```

键|value|默认|备注
-|-|-|-
已启用|布尔值|true|true 导致固定驱动器（即 C:/ 或 D:/）自动装载到 DrvFs 中的 /mnt 下。 false 表示驱动器不会自动装载，但你仍可以手动或通过 fstab 装载驱动器。
mountFsTab|布尔值|true|true 设置启动 WSL 时要处理的 /etc/fstab。 /etc/fstab 是可在其中声明其他文件系统的文件，类似于 SMB 共享。 因此，在启动时，可以在 WSL 中自动装载这些文件系统。
root|字符串|/mnt/|设置固定驱动器要自动装载到的目录。 例如，如果 WSL 中的某个目录位于 /windir/，而你将该目录指定为根目录，则固定驱动器预期会装载到 /windir/c
选项|逗号分隔值列表|空字符串|此值将追加到默认的 DrvFs 装载选项字符串。 只能指定特定于 DrvFs 的选项。 通常由装载二进制文件分析成标志的选项不受支持。 若要显式指定这些选项，必须在 /etc/fstab 中包含要对其执行此操作的每个驱动器。

### 装载选项
为 Windows 驱动器 (DrvFs) 设置不同的装载选项可以控制为 Windows 文件计算文件权限的方式。 你可使用以下选项：

键|说明|默认值
-|-|-
uid|用于所有文件的所有者的用户 ID|WSL 发行版的默认用户 ID（第一次安装时，此项默认为 1000）
gid|用于所有文件的所有者的组 ID|WSL 发行版的默认组 ID（第一次安装时，此项默认为 1000）
umask|要对所有文件和目录排除的权限的八进制掩码|000
fmask|要对所有文件排除的权限的八进制掩码|000
dmask|要对所有目录排除的权限的八进制掩码|000
注意： 权限掩码在应用到文件或目录之前通过一个逻辑或操作进行设置。
### 网络
#### 节标签：[network]

键|value|默认|备注
-|-|-|-
generateHosts|布尔值|true|true 将 WSL 设置为生成 /etc/hosts。 hosts 文件包含主机名对应的 IP 地址的静态映射。
generateResolvConf|布尔值|true|true 将 WSL 设置为生成 /etc/resolv.conf。 resolv.conf 包含能够将给定主机名解析为其 IP 地址的 DNS 列表。

### interop
#### 节标签：[interop]
这些选项在预览体验成员内部版本 17713 和更高版本中可用。

键|value|默认|备注
-|-|-|-
已启用|布尔值|true|设置此键可确定 WSL 是否支持启动 Windows 进程。
appendWindowsPath|布尔值|true|设置此键可确定 WSL 是否会将 Windows 路径元素添加到 $PATH 环境变量。

# 文件系统权限
Linux访问Windows通过`/mnt/c`访问C盘

Windows访问Linux通过`\\wls$`访问

[更多参考官方文档](https://docs.microsoft.com/zh-cn/windows/wsl/file-permissions)

# 互操作
Windows上执行Linux命令 格式 `wsl <linux command>`,例如powershell下执行`wsl sudo apt-get update`

Linux上执行Windows命令 格式`<binaryname>.exe`,例如Linux先执行`explorer.exe`


# 更新
首次安装之后进入linux后执行命令`sudo apt update && sudo apt upgrade`进行更新，windows不会自动更新Linux




