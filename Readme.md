# ModelPlaneInfo.WebEditor

An old course project designed to introduce the ASP .NET MVC Framework. The ASP .NET Framework is deprecated. ASP .NET Core is currently recommended. The project was developed on an old platform due to the need to comply with the requirements of the technical task.

## Project structure

```bash
ModelPlaneInfo.WebEditor:
¦   ModelPlaneInfo.WebEditor.sln
¦   
+---Common
¦   ¦   Common.csproj
¦   ¦   
¦   +---Extensions
¦   ¦       EnumerableMethods.cs
¦   ¦       
¦   +---Interfaces
¦   ¦       IEntity.cs
¦   ¦       IKeyable.cs
¦   ¦       ITestStorage.cs
¦   ¦       
¦   L---Properties
¦           AssemblyInfo.cs
¦           
+---Common.Repositories
¦   ¦   Common.Repositories.csproj
¦   ¦   Repository.cs
¦   ¦   UnitOfWork.cs
¦   ¦   
¦   +---Interfaces
¦   ¦       IRepository.cs
¦   ¦       IUnitOfWork.cs
¦   ¦       
¦   L---Properties
¦           AssemblyInfo.cs
¦           
+---ModelPlaneInfo
¦   ¦   ModelPlaneInfo.csproj
¦   ¦   ModelPlaneInfo.csproj.user
¦   ¦   
¦   +---Data
¦   ¦       DataContext.cs
¦   ¦       DataContext.TestingData.cs
¦   ¦       
¦   +---Entities
¦   ¦       ModelPlane.cs
¦   ¦       ModelPlane.validation.cs
¦   ¦       PlaneType.cs
¦   ¦       PlaneType.validation.cs
¦   ¦       
¦   +---Interfaces
¦   ¦       IDataSet.cs
¦   ¦       IFileIoController.cs
¦   ¦       
¦   +---IO
¦   ¦       BinarySerializationController.cs
¦   ¦       GenericBinarySerializationController.cs
¦   ¦       XmlFileIoController.cs
¦   ¦       
¦   +---Properties
¦   ¦       AssemblyInfo.cs
¦   ¦       
¦   L---Repositories
¦           FileBasedUnitOfWork.cs
¦           IInfoUnitOfWork.cs
¦           
L---ModelPlaneInfo.Web
    ¦   ModelPlaneInfo.Web.csproj
    ¦   
    +---App_Start
    ¦       RouteConfig.cs
    ¦       
    +---Connected Services
    +---Controllers
    ¦       HomeController.cs
    ¦       ModelsPlaneController.cs
    ¦       ModelsPlaneCrudController.cs
    ¦       NavigationController.cs
    ¦       PlanesTypeController.cs
    ¦       PlanesTypeCrudController.cs
    ¦  
    +---Infrastructure
    ¦       UoWCreator.cs
    ¦       
    +---Models
    ¦       ModelPlaneEditingModel.cs
    ¦       ModelPlaneInfoModel.cs
    ¦       ModelPlaneSelectionModel.cs
    ¦       ModelPlaneTableModel.cs
    ¦       ModelPlaneViewModel.cs
    ¦       PlaneTypeEditingModel.cs
    ¦       PlaneTypeSelectionModel.cs
    ¦       PlaneTypeViewModel.cs
    ¦       
    +---Properties
    ¦       AssemblyInfo.cs
    ¦       
    +---Scripts
    ¦       bootstrap.min.js
    ¦       
    L---Views
        ¦   web.config
        ¦   _ViewStart.cshtml
        ¦   
        +---Home
        ¦       EntitiesList.cshtml
        ¦       
        +---ModelPlaneCrud
        ¦       Create.cshtml
        ¦       Edit.cshtml
        ¦       Index.cshtml
        ¦       
        +---ModelsPlane
        ¦       BrowseByLetters.cshtml
        ¦       ModelPlanesByTypeNameInfo.cshtml
        ¦       ModelsPlane.cshtml
        ¦       Selection.cshtml
        ¦       _BrowseData.cshtml
        ¦       _SelectionForm.cshtml
        ¦       _TableBody.cshtml
        ¦       _TableHead.cshtml
        ¦       
        +---Navigation
        ¦       ModelPlanesByTypeNameMenu.cshtml
        ¦       
        +---PlanesType
        ¦       PlanesType.cshtml
        ¦       
        +---PlanesTypeCrud
        ¦       Create.cshtml
        ¦       Edit.cshtml
        ¦       Index.cshtml
        ¦       
        L---Shared
                _CategoryLayout.cshtml
                _DescriptiveBlock.cshtml
                _DescriptiveInfo.cshtml
                _Layout.cshtml
                
 ```           

- **`Common`**- contains auxiliary classes.
- **`Common.Repositories`** - contains the implementation classes of the `Unit of work` and `Repository` patterns the correctness of sorting.
- **`ModelPlaneInfo`** - contains the data context and classes for working with the data.
- **`ModelPlaneInfo.Web`** - contains a web project namely models, views, controllers.

## Information about tools

The following tools are used in the project:

- **`.NET Framework`** 
- **`ASP .NET Framework`**
- **`jQuery`** 
- **`Bootstrap`**

## Brief instructions

To start the solution, you need to designate the `ModelPlaneInfo.Web` project as a `Startup project`. You also need to install NuGet packages. To install NuGet packages, you need to perform the following steps:

- `Step 1:` right-click on the project icon in the Solution Explorer;
- `Step 2:` open **Manage NuGet Packages**;
- `Step 3:` choose a point **Select all packages** and click on button **Update**;

After these steps, the project will start correctly. After launch, you will see the following page:

![image_2023-02-15_16-19-51](https://user-images.githubusercontent.com/70714177/219371382-ffa56030-399e-410a-abee-73830f6e2c01.png)

Image 1. - _**Start page**_

The application supports work with two entities. But more opportunities are provided for working with the data of the _Model plane_ entity.

<img src="https://user-images.githubusercontent.com/70714177/219373264-063b35e8-9aab-4e36-9adb-97c02a0cff39.png" width="16%"></img> 
<img src="https://user-images.githubusercontent.com/70714177/219372886-b06a4722-16f1-4775-bfae-83ef9866dbe1.png" width="16%"></img>
<img src="https://user-images.githubusercontent.com/70714177/219373363-0b9166f5-25e5-4d14-afa4-ecfcc0027409.png" width="16%"></img> 
<img src="https://user-images.githubusercontent.com/70714177/219373008-8e4e506f-3c12-4fbc-a17a-d89635f9fb9c.png" width="16%"></img>
<img src="https://user-images.githubusercontent.com/70714177/219373121-dacd5e4e-dcde-4fe9-bbd6-bc26d1253329.png" width="16%"></img> 
<img src="https://user-images.githubusercontent.com/70714177/219373198-0abfd44b-7ca6-459d-8137-72a8e4b3b7cd.png" width="16%"></img>  

Image 2. - _**Tools for working with data about aircraft models**_

<img src="https://user-images.githubusercontent.com/70714177/219374113-a7df0d96-c17d-4008-9f46-7fcbea27f509.png" width="24%"></img> 
<img src="https://user-images.githubusercontent.com/70714177/219374033-38fcce47-f89a-4e5a-a666-720df5bde763.png" width="24%"></img> 
<img src="https://user-images.githubusercontent.com/70714177/219374290-b6b60e32-e28b-4cfa-b359-e8cf0bc32df0.png" width="24%"></img> 
<img src="https://user-images.githubusercontent.com/70714177/219374191-fd81da5a-8531-4e6c-9286-38f06fe000ac.png" width="24%"></img> 

Image 3. - _**Tools for working with data on aircraft type**_

## Conclusions

For all the shortcomings of this project, it is a good example of both good and bad decisions. It can be considered as a good educational project.

Basic recommendations:

- Use .NET Core for projects;
- For the front-end part, use ASP.NET Razor, React, Vue or Angular(for large projects);

## License

Distributed under the MIT License. See `License.txt` for more information.
