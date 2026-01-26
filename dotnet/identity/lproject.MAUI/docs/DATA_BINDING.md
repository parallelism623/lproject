Data binding in MAUI links a pair of properties between two objects.
At least one of which is usually a user-interface object.
The target is always the object on which the data binding is set even if it's providing data rather than receiving data
You can set a binding on an instance of any class that derives from BindableObject.
### Binding with BindingContext ###
#### Binding in under handler ####
Assign a value to the BindingContext in each class derived from BindableObject to set up bindings for the target UI elements.
```csharp
BindableObject.BindingContext = value; 
// Assign a value to the BindingContext
// Then you can use {Binding Path} in XAML to bind values to the BindableObject's properties
// or use BindableObject.SetBinding(BindableProperties, Func (value) => value.Properties);
BindableObject.SetBinding(BindableProperties, Func (value) => value.Properties);
```

Or you can assign a value to the BindingContext in XAML like:
```xml
<Element>
    <Element.BindingContext>
        <x:Reference Name="slider"/> // Use this syntax when you need to set binding to source in XAML
        <x:Model/> // Use this syntax when you need create new models.
    </Element.BindingContext>
</Element>

// or 
<Element BindingContext="{x:Reference Name=slider}" BindingContext="{x:Model}">
</Element>

```

### Binding without BindingContext
If you do not set a binding source at the element level, you must specify the binding source when setting bindings for each property.

```xml
        <Label Text="TEXT"
               FontSize="40"
               HorizontalOptions="Center"
               VerticalOptions="Center"
               Scale="{Binding x:DataType='Slider',
                               Source={x:Reference slider} or Source={x:Model},
                               Path=Value}" />
```

### Binding Mode ###
#### One-way binding ####
In one-way binding mode, the values of the target properties are initialized from the source properties, and these target properties update when the source properties change.
#### Two-way binding ####
In two-way binding, the target properties are initialized in the same way as in one-way binding and exhibit the same behavior when the source properties change.
However, when the source properties change, the binding engine relies on callbacks registered with the PropertyChanged event defined by the INotifyPropertyChanged interface in order to update the target and allow the source values to be changed correctly.

So, if you use a ViewModel as the binding source, the class must implement the INotifyPropertyChanged interface and raise the PropertyChanged event whenever its properties change
#### One-time binding ####
The target properties are initialized from the binding source and are updated whenever the BindingContext changes.
#### One-time-to-source binding ####
The source properties are initialized from the target properties and are updated whenever the target properties change.

Use INotifyPropertyChanged:
```csharp
public partial class LoginViewModel: INotifyPropertyChanged
{


    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand LoginCommand { get; }
    
    public LoginViewModel()
    {
        LoginCommand = new Command(Login);
    }
    public string Email
    {
        get { return field; }
        set
        {
            if (value != field)
            {
                field = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }
    }
}

```

#### Binding Converter ####
Binding mark up hoặc Binding element có hỗ trợ define converter bằng các implement IValueConverter cho phép định nghĩa logic convert value từ
source -> target: Convert method và ngược lại: ConvertBack().
```csharp
public class CustomBindingConverter : IValueConverter
{
    // parameter: ConveterParameter
    // Type: Convert: Target type, ConvertBack: Source type
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
```

``` Relavtive binding ```
Khai báo source binding một cách tương đối, thông qua  RelativeSource markup extension:
* Mode: 
  * TemlateParent: Lấy element sử dụng ControlElement chứa element define ralive binding
  * Self: Binding source là chính nó
  * FindAncestor: Tìm ancestor có type tương ứng
  * FindAncestorBindingContext: Tìm ancestor có type tương ứng và lấy binding context của nó
* AncestorType: Type của ancestor gần nhất
* AncestorLevel: Level
```xml
<Button Text="{Binding Source={RelativeSource Mode='FindAncestorBindingContext', AncestorType={x:Type VerticalStackLayout}}, Path={Binding}} "/>
```

#### Binding fallbacks ####
BindingExtension object cung cấp properties:
* FallbackValue: value được dùng khi không resolve path được
* TargetNullValue: value được dùng khi source value null

