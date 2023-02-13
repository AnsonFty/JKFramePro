# JKFramePro

JKFramePro在原始框架基础上进行了部分扩展，保证在开发中有需要的功能
未来开发计划：创建JRPG、ARPG分支制作DEMO捶打框架，倒逼框架更加完善；还有下面对原框架拓展的功能部分BUG及不完善的地方继续优化。

**JKFramePro对原框架的拓展**

1. 配置表系统(TableSystem)：配置表系统是使用tabtoyV3的原始版本，没有弄清楚怎么定制它，未来想要对他的输出C#格式进行定制和对表定义类型进行扩展
2. 本地化系统(LocalizationSystem):参考JKFrame1.0的本地化格式，通过TableSystem及UISystem完成本地化。
3. 输入系统(JKInputSystem):旨在想要封装一套新输入系统，达到想要的键鼠、手柄、触屏操作无缝切换。目前通过和老输入系统混用封装避免了Esc的安卓手势操作会卡住的BUG，NewInputSystem自带的BUG，目前除了用老输入系统代替这个操作没有找到更好的办法。使用InputIcons插件来达到想要的切换输入提示效果，但大问题是使用InputIcons插件会导致打安卓包报错未解决。
4. 选择项导航系统(SelectableSystem):旨在想要实现统一管理Selectable导航，但目前遇到的最大的困惑就是怎么才能让没有导航的按钮点击后不被导航选中。目前还需优化关于Group管理问题，目前思路不对，不被导航时Interactable应该关闭。


**对原框架的修改**

1. EventSystem更名为JKEventSystem，防止要和Unity的EventSystem做区分。

**以下是原始框架说明**

JKFrame2.0设想的是为独立游戏服务的，小团队甚至只有一个人的情况下使用的！

JKFrame2.0比较像是个工具箱，除了UI窗口外的大多情况下，你并不需要继承什么类或接口，而提供的功能也不是非用不可~所以比较接近工具箱

很多功能更像是插件的感觉~如：

	SaveSystem.SaveObject(object)保存某个存档数据

	AudioSystem.PlayBGAudio(audioClip)播放某个背景音乐

**主要功能系统：**

1. 对象池系统：重复利用GameObject或普通class实例，并且支持设置对象池容量
2. 事件系统：解耦工具，不需要持有引用来进行函数的调用
3. 资源系统

    * Resources版本：关联对象池进行资源的加载卸载
    * Addressables版本：关联对象池进行资源的加载卸载，可结合事件工具做到销毁时自动从Addressables Unload

4. MonoSystem：为不继承MonoBehaviour的对象提供Update、FixedUpdate、协程等功能
5. 音效系统：背景音乐、背景音乐轮播、特效音乐、音量全局控制等
6. 存档系统：

    * 支持多存档
    * 自动缓存，避免频繁读磁盘
    * 存玩家设置类数据，也就是不关联任何一个存档
    * 支持二进制和Json存档，开发时使用Json调试，上线后使用二进制加密与减少文件体积
7. 日志系统：日志控制、保存等
8. UI系统：UI窗口的层级管理、Tips功能
9. 配置系统：目前版本较鸡肋
10. 场景系统：对Unity场景加载封装了一层，主要用于监听场景加载进度

**其他功能：**

1. 状态机：脚本逻辑状态机
2. 事件工具：给物体绑定 碰撞、触发、点击、拖拽、自定义等事件
3. 协程工具：协程避免GC

**内置：**

1. Odin插件，方便做一些序列化和调试
2. JKLog，日志

**团队**

Joker

Parks

**社群**

QQ群：654336425

作者：739554159（Joker）