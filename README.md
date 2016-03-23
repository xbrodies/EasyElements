# Easy Elements #
![enter image description here](https://cdn0.iconfinder.com/data/icons/windows8_icons/26/dll.png) **Current version library:** 0.1.1<br>
![enter image description here](https://cdn1.iconfinder.com/data/icons/hawcons/32/698693-icon-102-document-file-xml-24.png) **Current version config:** 156<br>
![enter image description here](https://cdn4.iconfinder.com/data/icons/logos-4/24/Translate-24.png) **Languages:** Russian, English<br>

### ![enter image description here](https://cdn3.iconfinder.com/data/icons/social-7/500/Help_mark_query_question_support_talk-20.png)What is it?
This is a library for reading and writing binary file elements.data of the multiplayer game "Perfect World".
Currently on version 0.1.1 library can read elements.data to version 156. However, the library uses the configuration file in xml format.
All data from elements.data will be placed into a **DataSet**
<br>
<br>
### ![enter image description here](https://cdn1.iconfinder.com/data/icons/flat-design-icons-set-1/256/Free-14-20.png) Configuration file
Elements.data Files has many versions and fields, so the configuration file is used. In the configuration file from lists and fields of elements.data, their versions, links, and additional attributes in the XML format. 
Part of the configuration file:
```html
<List Name="026 - UNIONSCROLL_ESSENCE" Version="6" Skip="0" ListType="Essence">
    <Type Name="ID" Type="int" Version="6" />
    <Type Name="Name" Type="string" Encoding="Unicode" SizeString="64" Version="6" />
    <Type Name="file_matter" Type="string" Encoding="gb2312" SizeString="128" Version="6" />
    <Type Name="file_icon" Type="string" Encoding="gb2312" SizeString="128" Version="6" />
    <Type Name="use_time" Type="float" Version="6" />
    <Type Name="price" Type="int" Version="6" />
    <Type Name="shop_price" Type="int" Version="6" />
    <Type Name="pile_num_max" Type="int" Version="6" />
    <Type Name="has_guid" Type="int" Version="6" />
    <Type Name="proc_type" Type="int" Version="6" />
</List>
```

> Content attribute called "Name" will be used as the name of the tables and columns in a DataSet

### ![enter image description here](https://cdn2.iconfinder.com/data/icons/ballicons-2-free/100/wrench-24.png) Usage
 - How to read the file Elements.data:
```csharp
var elFile = new ElementsFile("Path to elements.data file", "Path to config.xml");
var elements = elFile.Open(); 
```

 - Or, you can use pre-boot configuration files:
```charp
var conf = new ConfigReader("Path to config.xml");
conf.Open();

var elFile = new ElementsFile("Path to elements.data file", conf);
var elements = elFile.Open(); 
```

> Loading file takes some amount of time. Usually the two or three seconds. I recommend using the "Open" method on a different thread. In the future, we will implement a multithreaded method.
 
  - How to access data
```charp
var data = Elements.Values.Tables["026 - UNIONSCROLL_ESSENCE"];
foreach (DataRow row in data.Rows)
        Console.WriteLine($"ID: {row["ID"]}");
```
