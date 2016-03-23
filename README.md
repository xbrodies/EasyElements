## PWEasyEditor.Elements
Reading and writing binary file elements.data of multiplayer games Perfect World<br>
<strong>Current version:</strong> 0.1.1

Elements.data Files has many versions and fields, so the configuration file is used. In the configuration file from lists and fields of elements.data, their versions, links, and additional attributes in the XML format.
<br>Part of the configuration file:
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
All data from elements.data will be placed into a <strong>DataSet</strong>

### Usage
How to read the file Elements.data:
```csharp
var elFile = new ElementsFile("Path to elements.data file", "Path to config.xml");
var elements = elFile.Open(); 
```
or so
```csharp
var conf = new ConfigReader("Path to config.xml");
conf.Open();

var elFile = new ElementsFile("Path to elements.data file", conf);
var elements = elFile.Open(); 
```
