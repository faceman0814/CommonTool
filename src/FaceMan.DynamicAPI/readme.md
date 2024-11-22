# 功能
动态扫描生成Swagger API接口。
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
### 文档注释
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
![image](https://github.com/faceman0814/picx-images-hosting/raw/master/20241121/image.4g4ixojalp.webp)

### Swagger登录页
一般来说Swagger都会需要登录才能进行接口调用，所以需要一个登录页，如果需要鉴权请自己实现。
```
var conifg = new SwaggerConfigParam()
{
    //其他配置
    EnableLoginPage=true,
    WebRootPath=builder.Environment.WebRootPath,
    LoginPagePath = "pages/swagger.html"
};
```

在wwwroot下pages目录下创建swagger.html文件
```
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Swagger UI</title>
    <!-- 使用 Swagger UI 默认的 CSS 样式 -->
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.1.0/swagger-ui.min.css">
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
        }

        .swagger-ui .scheme-container {
            margin: 0px;
            padding: 0px;
        }

        .button-container {
            justify-content: center;
            right: 5%;
            top: 10%;
            position: absolute
        }

            .button-container button {
                margin: 0 10px;
                padding: 10px 20px;
                font-size: 14px;
                border: none;
                background-color: #1b1b1b;
                color: #fff;
                cursor: pointer;
                border-radius: 4px;
            }
    </style>
</head>
<body>
    <div class="button-container">
        <!--<button id="hangfireButton">跳转到 Hangfire</button>-->
        <button id="logoutButton">退出登录</button>
    </div>
    <div id="swagger-ui"></div>

    <!-- 加载 Swagger UI 的 JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.1.0/swagger-ui-bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.1.0/swagger-ui-standalone-preset.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/js-cookie/3.0.1/js.cookie.min.js"></script>
    <script>
        window.onload = function () {
            var configObject = JSON.parse('%(ConfigObject)');
            const cookie = Cookies.get('access-token');

            if (!cookie) {
                window.location.href = '/home/index';
            }

            // 配置 SwaggerUI
            configObject.dom_id = "#swagger-ui";
            configObject.presets = [SwaggerUIBundle.presets.apis, SwaggerUIStandalonePreset];
            configObject.layout = "StandaloneLayout";
            configObject.requestInterceptor = function (request) {
                // 添加 Authorization 头
                request.headers['Authorization'] = 'Bearer ' + cookie;
                return request;
            };

            // 设置 OAuth2 重定向 URL
            if (!configObject.hasOwnProperty("oauth2RedirectUrl")) {
                configObject.oauth2RedirectUrl = window.location + "oauth2-redirect.html";
            }

            configObject.plugins = [
                function (system) {
                    return {
                        components: {
                            authorizeBtn: function () {
                                return null;
                            }
                        }
                    };
                }
            ];

            // 初始化 Swagger UI
            SwaggerUIBundle(configObject);

            // 跳转到 Hangfire 并打开新页面
            document.getElementById("hangfireButton").addEventListener("click", function () {
                window.open("/hangfire", "_blank"); // 替换 "/hangfire" 为正确的 Hangfire 页面 URL
            });

            // 退出登录并清除 cookie
            document.getElementById("logoutButton").addEventListener("click", function () {
                Cookies.remove("access-token"); // 清除名为 "access-token" 的 cookie
                window.location.href = "/Home/Index"; // 替换 "/home/logout" 为正确的退出登录 URL
            });
        }
    </script>
</body>
</html>


```
新建一个LoginController
```
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

新建Index视图
```
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <style>
        * {
            padding: 0;
            margin: 0;
        }

        html {
            height: 100%;
        }

        body {
            background-image: linear-gradient(to bottom right, rgb(114, 135, 254), rgb(130, 88, 186));
        }

        .login-container {
            width: 600px;
            /*  height: 315px; */
            margin: 0 auto;
            margin-top: 10%;
            border-radius: 15px;
            box-shadow: 0 10px 50px 0px rbg(59, 45, 159);
            background-color: rgb(95, 76, 194);
        }

        .left-container {
            display: inline-block;
            width: 330px;
            border-top-left-radius: 15px;
            border-bottom-left-radius: 15px;
            padding: 60px;
            background-image: linear-gradient(to bottom right, rgb(118, 76, 163), rgb(92, 103, 211));
        }

        .title {
            color: #fff;
            font-size: 18px;
            font-weight: 200;
        }

            .title span {
                border-bottom: 3px solid rgb(237, 221, 22);
            }

        .input-container {
            padding: 20px 0;
        }

        input {
            border: 0;
            background: none;
            outline: none;
            color: #fff;
            margin: 20px 0;
            display: block;
            width: 100%;
            padding: 5px 0;
            transition: .2s;
            border-bottom: 1px solid rgb(199, 191, 219);
        }

            input:hover {
                border-bottom-color: #fff;
            }

        ::-webkit-input-placeholder {
            color: rgb(199, 191, 219);
        }

        .right-container {
            width: 145px;
            display: inline-block;
            height: calc(100% - 120px);
            vertical-align: top;
            padding: 60px 0;
        }

        .action-container {
            font-size: 10px;
            color: #fff;
            text-align: center;
            position: relative;
            top: 200px;
        }

            .action-container .btn {
                border: 1px solid rgb(237, 221, 22);
                padding: 10px;
                display: inline;
                line-height: 20px;
                border-radius: 20px;
                position: absolute;
                bottom: 10px;
                left: calc(72px - 20px);
                transition: .2s;
                cursor: pointer;
            }

                .action-container .btn:hover {
                    background-color: rgb(237, 221, 22);
                    color: rgb(95, 76, 194);
                }
    </style>
</head>
<body>
    <div class="login-container">
        <div class="left-container">
            <div class="title"><span>登录</span></div>
            <div class="input-container">
                <input type="text" id="username" name="username" placeholder="用户名">
                <input type="password" id="password" name="password" placeholder="密码">
            </div>
        </div>
        <div class="right-container">
            <div class="action-container">
                <button type="submit" id="login-btn" class="btn btn-primary">登录</button>
            </div>
        </div>
    </div>
</body>
</html>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        // 当表单提交时
        $('#login-btn').click(function (event) {
            event.preventDefault(); // 防止页面刷新
            //$('.error').remove(); // 移除已有的错误信息
            var username = $('#username').val();
            var password = $('#password').val();
            // 发起 AJAX 请求
            $.ajax({
                type: 'get',
                contentType: "application/json",
                url: '@ViewBag.Url/api/Login/UserLogin?username=123&password=456',//登录接口地址
                // data: JSON.stringify({
                //     username: username,
                //     password: password
                // }),
                success: function (response) {
                    window.location.href = '/swagger';
                },
                error: function (response) {
                    // 如果返回错误信息，则显示在页面上
                    const exampleModal = document.getElementById('exampleModal')
                    exampleModal.addEventListener('show.bs.modal', event => {
                        // Update the modal's content.
                        const modalTitle = exampleModal.querySelector('.modal-title');
                        const modalBodyInput = exampleModal.querySelector('.modal-body');

                        modalBodyInput.textContent = response?.responseJSON?.result?.error ?? '服务器异常';
                    })
                    $('#whatever').trigger('click');

                }
            });
        });
    });

</script>

```
请求登录接口成功之后就会自动跳转Swagger，在调用接口时会自动加到请求头，token过期后会跳回登录页面。
![image](https://github.com/faceman0814/picx-images-hosting/raw/master/20241121/image.99tdtuk3sy.webp)