# 功能
动态扫描继承了IApplicationService的类，生成Swagger API接口。
支持三种方式：
- 继承IApplicationService接口
- 标记DynamicWebApi特性
- 原生ApiController

# 教程
## 必要配置：
在appsettings.json配置请求类型规则,例如以Get或者Query开头的方法，就会被识别为Get类型。
```json
  "HttpMethodInfo": [
    {
      "MethodKey": "Get",
      "MethodVal": [ "GET", "QUERY" ]
    },
    {
      "MethodKey": "Post",
      "MethodVal": [ "CREATE", "SAVE", "INSERT", "ADD" ]
    },
    {
      "MethodKey": "Put",
      "MethodVal": [ "UPDATE", "EDIT" ]
    },
    {
      "MethodKey": "Delete",
      "MethodVal": [ "Delete", "REMOVE", "Del" ]
    }
  ]
```

在Program设置Swagger配置项
```csharp
var conifg = new SwaggerConfigParam()
{
    Title = "FaceMan API",
    Version = "v1",
    Description = "FaceMan API",
    ContactName = "FaceMan",
    ContactEmail = "face<EMAIL>",
    ContactUrl = "https://www.face-man.com",
    WebRootPath = builder.Environment.WebRootPath,
    HttpMethods = builder.Configuration.GetSection("HttpMethodInfo").Get<List<HttpMethodConfigure>>()
};

builder.Services.AddDynamicApi(conifg);
//其他配置

app.UseSwagger(conifg);

```

## 其他配置项
- 文档注释
1、要开启文档注释，首先需要在类库属性中设置XML文件路径
![image](https://github.com/faceman0814/picx-images-hosting/raw/master/20241121/image.77dl5lmgd7.webp)
2、启用SwaggerConfigParam的文档注释配置

```
var conifg = new SwaggerConfigParam()
{
    //其他配置
    EnableXmlComments=ture,
    ApiDocsPath = "ApiDocs"
};
```
3、在启动项目属性页配置如下代码
这里类库命名最好有个通用前缀，不然只能一条条写了，比如FaceMan.Web、FaceMan.Domain
```
    <PropertyGroup>
	    <!--其他配置-->
		<ApiDocDir>wwwroot\ApiDocs</ApiDocDir>
	</PropertyGroup>

	<!--在构建项目后，复制所有以FaceMan开头的XML文档文件到指定的API文档目录。这通常用于将生成的XML文档文件（例如API注释）整理到一个目录中，便于进一步的处理或发布。这种做法可以适合于生成API文档如Swagger时的使用场景。-->
	<Target Name="CopyXmlDocFileForBuild" AfterTargets="Build">
		<ItemGroup>
			<XmlDocFiles Include="$(OutDir)$(AssemblyName).xml" />
			<!--Include="@(ReferencePath->'%(RootDir)%(Directory)%(Filename).xml')"：这行表示从所有项目引用（ReferencePath）的路径中收集以.xml为扩展名的文件。这些文件通常是生成的XML文档文件。-->
			<!--Condition="$([System.String]::new('%(FileName)').StartsWith('FaceMan'))"：此条件用于过滤文件，仅包括文件名以FaceMan开头的XML文档文件。这保证只有相关的文档文件被选择。-->
			<XmlDocFiles Include=" @(ReferencePath->'%(RootDir)%(Directory)%(Filename).xml')" Condition="$([System.String]::new('%(FileName)').StartsWith('FaceMan'))" />
		</ItemGroup>
		<!--SourceFiles="@(XmlDocFiles)"：指定要复制的源文件为上一步中定义的XmlDocFiles集合。-->
		<!--`Condition="Exists('%(FullPath)')"：确保复制前源文件存在，这是一种安全检查。-->
		<!--DestinationFolder="$(ApiDocDir)"：目的地文件夹为$(ApiDocDir)，这个属性之前已经在项目文件中定义，指向存放API文档的目录。-->
		<!--SkipUnchangedFiles="true"：此选项表示只有发生变化的文件会被复制，这可以提高效率，避免不必要的复制操作。-->
		<Copy SourceFiles="@(XmlDocFiles)" Condition="Exists('%(FullPath)')" DestinationFolder="$(ApiDocDir)" SkipUnchangedFiles="true" />
	</Target>
```

配置完启动就可以看见啦
![image](https://github.com/faceman0814/picx-images-hosting/raw/master/20241121/image.m3sa3kzb.webp)

- Swagger登录页

