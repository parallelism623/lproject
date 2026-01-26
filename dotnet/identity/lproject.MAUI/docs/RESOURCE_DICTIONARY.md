#### Resource Dictionary ####
* Giúp define styles, control templates, data templates, converters, and colors. Cho phép tham sử dụng lại chúng
thông qua StaticResource hoặc DynamicResource markup extension.
* Define ResourceDictionary thông qua Resource property VirtualElement hoặc Application
* Scoped:
  * Resource gắn với các view hoặc layout thì chỉ có hiệu lực trong view hoặc layout đó
  * Resource được khai báo tại page level thì có phạm vi trong page
  * Resource được khai báo tại App level thì có phạm vi toàn app
#### Consume resources ####
* Resource được khai báo kèm x:Key, các mark up extension sử dụng các x:Key để tìm resource tương ứng
* Markup extension sử dụng để consume resources:
  * StaticResource: Giá trị được gán một lần lúc khởi tạo, không thay đổi khi resource của key thay đổi
  * DynamicResource: Giá trị cập nhật khi x:Key cập nhật
* Resource lookup
  * Element tìm resource key tương ứng từ element gần nó nhất cho tới root.
  * Nếu tìm được thì gán data và quá trình dừng
  * Nếu tới root mà k tìm được thì XamlParseException throw
#### Resource templates ####
* Standalone resource: Resource có thể define bằng một file XAML độc lập, không cần backed vào code behind
Sử dụng ResourceDictionary.Source trong view để sử dụng standalone resource.
* Các resource ở assemblies khác cần code behind là các class kế thừa DictionaryResource, XAML view sử dụng chúng
thông qua ResourceDictionary.MergedDictionaries property.
```xml
<ContentPage ...
        xmlns:local="clr-namespace:ResourceDictionaryDemo"
        xmlns:theme="clr-namespace:MyThemes;assembly=MyThemes">
<ContentPage.Resources>
    <ResourceDictionary>
        <!-- Add more resources here -->
        <ResourceDictionary.MergedDictionaries>
            <!-- Add more resource dictionaries here -->
            <local:MyResourceDictionary />
            <theme:DefaultTheme />
            <!-- Add more resource dictionaries here -->
        </ResourceDictionary.MergedDictionaries>
        <!-- Add more resources here -->
    </ResourceDictionary>
    </ContentPage.Resources>
        ...
</ContentPage> 
```