[![Build status](https://ci.appveyor.com/api/projects/status/a4te2hnokv9jo17a?svg=true)](https://ci.appveyor.com/project/lucasteles/modelviewbinder)
[![License](http://img.shields.io/:license-mit-blue.svg)](http://csmacnz.mit-license.org)
[![Nuget](https://img.shields.io/nuget/v/ModelViewBinder.svg)](https://www.nuget.org/packages/ModelViewBinder/)

ModelViewBinder
=======
ModelViewBinder aims to support a simple two way binding definition.

*(For Windows Forms see the section bellow)

## Installing
To install from Nuget

```powershell
Install-Package ModelViewBinder 
```


Projects
--------
* **ModelViewBinder:** 
  * Contains the base binder tool
  
* **ModelViewBinder.Forms:** 
  * Contains a implementation for winforms


Code Examples
-------------
### Basic Binding
##### Create a root binder for the view model
```C#
var binder = new ModelViewBinder<SourceType>(source);
```

##### Dispose of a binder
Diposing of a binder removes all bindings and disposes of all subcriptions created by the binder.
```C#
binder.Dispose();
```

##### Register external disposables with the binder
This can be used to tie the lifetime of other objects to the binder's lifetime.
```C#
binder.RegisterDispose(disposable);
```

##### Bind a view model property to other class property
```C#
 binder.Bind(e => e.A, target, e => e.X);
```

##### Multiple Binds 
```C#
 binder
    .Bind(e => e.A, target, e => e.X)
    .Bind(e => e.B, target, e => e.Y)
 ;
```


##### Bind an integer view model property to a string property
```C#
 binder.Bind(e => e.A, target, e => e.X, Convert.ToString);
```

##### Two way bind of an integer view model property to a string property 
```C#
 binder.Bind(e => e.A, target, e => e.X, Convert.ToString, int.Parse);
```

##### Update all view model properties 
```C#
 binder.FillSource();
```

##### Update all target properties 
```C#
 binder.FillTargets();
```

##### Automatic update View Model when target changes
You have to implement the `ITargetWithChangeEvent` interface and fire the `ValueChanged` event


##### More simple bind
If are you working with some type of control, you can implement `ITargetWithValue<type>` or `ITargetWithValue`, it has a `Value` property which is automatically recognized by the binder

```C#
   binder.Bind(e => e.A, target);
```
##### Enable or disable targets
If are you working with some type of control, you can implement `ITargetWithEnabled`, it has a Enabled property, commonly used in UI controls, if you want to set this property of all binded targets you can use:


For disable
```C#
   binder.DisableAll();
```


For enable
```C#
   binder.EnableAll();
```

##### Bind Callback 
```C#
   binder.Bind(e => e.A, target, e => e.X).Then(() => /* ... */);
```

## Windows Forms Binder [![Nuget](https://img.shields.io/nuget/v/ModelViewBinder.Forms.svg)](https://www.nuget.org/packages/ModelViewBinder.Forms/)

### Installing
To install from Nuget
```powershell
Install-Package ModelViewBinder.Forms
```


For window form you should use `FormModelViewBinder<SourceType>`

```C#
    var source = new Source();
    var binder = new FormModelViewBinder<Source>(source);
    
    binder
       .Bind(e => e.ValueForTextbox, textBox1, e => e.Text)
       .Bind(e => e.ValueForRichText, richTextBox1, e => e.Text)
       .Bind(e => e.ValueForComboBox, comboBox1, e => e.SelectedValue)
       .Bind(e => e.ValueForUpDown, numericUpDown1, e => e.Value)
       .Bind(e => e.ValueForDatePicker, dateTimePicker1, e => e.Value)
       .Bind(e => e.ValueForCheckBox, checkBox1, e => e.Checked)
    ;
    
    binder.FillTargets();
```

The `FormModelViewBinder` automatically will bind the change events of the forms controls fot update de view model.
if you dont want this behavior you can disable:

```C#
   binder.AutoFillSourceWhenTargetChanges = false;
```

For targets derivated of Windows.Form.Control, you dont need to implement `ITargetWithEnabled`, the methods `EnableAll` and `DisableAll` will recognize the Control and change properly the `Enabled` property
