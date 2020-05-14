# [返回主目录](Readme.md)<!-- omit in toc --> 

# 目录 <!-- omit in toc --> 
- [NgModules简介](#ngmodules%e7%ae%80%e4%bb%8b)
  - [基本格式](#%e5%9f%ba%e6%9c%ac%e6%a0%bc%e5%bc%8f)
  - [模块分类](#%e6%a8%a1%e5%9d%97%e5%88%86%e7%b1%bb)
- [NgModule API](#ngmodule-api)
  - [declarations](#declarations)
  - [providers](#providers)
  - [imports](#imports)
  - [exports](#exports)
  - [bootstrap](#bootstrap)
  - [entryComponents](#entrycomponents)
- [常用的一些模块](#%e5%b8%b8%e7%94%a8%e7%9a%84%e4%b8%80%e4%ba%9b%e6%a8%a1%e5%9d%97)
- [特性模块的分类](#%e7%89%b9%e6%80%a7%e6%a8%a1%e5%9d%97%e7%9a%84%e5%88%86%e7%b1%bb)
- [共享NgModule](#%e5%85%b1%e4%ba%abngmodule)

## NgModules简介
NgModule 是一个带有 @NgModule 装饰器的类。 @NgModule 的参数是一个元数据对象，用于描述如何编译组件的模板，以及如何在运行时创建注入器。 它会标出该模块自己的组件、指令和管道，通过 exports 属性公开其中的一部分，以便外部组件使用它们。 NgModule 还能把一些服务提供者添加到应用的依赖注入器中。

### 基本格式
```ts
// imports
import { xxx } from 'xxx';

@NgModule({
  // 静态的，即编译器配置
  declarations: [], // 配置选择器
  entryComponents: [], // 生成主机工厂

  // 运行时或依赖注入配置
  providers: [], // 运行时注入配置

  // 组合/分组
  imports: [], // 组合各种NgModule
  exports: [] // 使NgModule可用于应用程序其他部分
})

export class AppModule {}
```
### 模块分类
主要分为**根模块**和**特性模块**

根模块就是你用来启动此应用的模块。

特性模块就是把与特定的功能或特性有关的代码从其它代码中分离出来。 为应用勾勒出清晰的边界，有助于开发人员之间、小组之间的协作，有助于分离各个指令，并帮助管理根模块的大小。

## NgModule API
### declarations
属于该模块的可声明对象（组件、指令和管道）的列表。
### providers
依赖注入提供者的列表。
### imports
导入该模块的其他模块列表
### exports
需要导出的该模块内部所导入的模块列表
### bootstrap
要自动启动的组件列表。
### entryComponents
那些可以动态加载进视图的组件列表。

## 常用的一些模块
NgModule|导入来源|为何使用
-|-|-
BrowserModule|@angular/platform-browser|当你想要在浏览器中运行应用时，包含了CommonModule。通常用于根模块
CommonModule|@angular/common|当你要使用NgIf和NgFor时
FormsModule|@angular/forms|当你要构建模板驱动表单时(它包含NgModule)
ReactiveFormsModule|@angular/forms|当你要构建响应式表单时
RouterModule|@angular/router|要使用路由功能，并且你要用到RouterLink，.forRoot()和.forChild()时
HttpClientModule|@angular/common/http|当你要和服务器进行对话时

## 特性模块的分类

- **领域特性模块**

- **带路由的特性模块**

- **路由模块**

- **服务特性模块**

- **可视部件特性模块**

[具体详解点这里](../../Angular8/docs/06.04-NgModule-特性模块的分类.md)

## 共享NgModule
创建共享模块能让你更好地组织和梳理代码。你可以把常用的指令、管道和组件放进一个模块中，然后在应用中其它需要这些的地方导入该模块。

