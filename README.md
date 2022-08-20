# 原文件树

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

# 生成的Summary.md

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



# 使用方法

### 默认路径

复制到[**原文件树**](#原文件树)所示位置, 直接双击

### 自定义路径

`./mdbook_summary.exe path/to/src`
