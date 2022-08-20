# 使用方法

### 默认路径

复制到[**文件树**](#文件树)所示位置, 直接双击

### 自定义路径

``` shell
./mdbook_summary.exe path/to/your/src
```

# 示例

## 文件树

```
│  book.toml
│  mdbook_summary.exe  //此程序, 默认工作路径为"./src"
│
├─book
│  │  ...
│
└─src
    │  chapter_1.md
    │  SUMMARY.md
    │
    ├─CSharp
    │      CSharp.md
    │
    ├─新建 文件夹  //引起警告
    │      新建文本文档.md
    │
    └─新建文件夹
            C#.md  //引起警告
```

## 终端输出

```
Using Path: D:\Repositories\Test\src\
Start...
Target File: D:\Repositories\Test\src\SUMMARY.md
------
# Summary

- [chapter_1](chapter_1.md)
- [CSharp]()
    - [CSharp](CSharp\CSharp.md)
- [新建 文件夹]()
    - [新建文本文档](新建 文件夹\新建文本文档.md)
- [新建文件夹]()
    - [C#](新建文件夹\C#.md)
------
...Done
------
Warning: 检测到下列文件(夹)含非法字符, 这会导致mdbook异常, 请修改后重新运行
------
新建 文件夹
C#.md
------
```

## 生成的Summary.md

```markdown
# Summary

- [chapter_1](chapter_1.md)
- [CSharp]()
    - [CSharp](CSharp\CSharp.md)
- [新建 文件夹]()
    - [新建文本文档](新建 文件夹\新建文本文档.md)
- [新建文件夹]()
    - [C#](新建文件夹\C#.md)
```
