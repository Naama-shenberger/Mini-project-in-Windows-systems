
# Mini project in Windows systems
A project in WPF in C # to manage A transportation 
system, develop a GUI interface using three-layer architecture, work with XML files and use 
design templates such as: Singleton and Factory.
## Features

- Architecture (the layer model, Contract Design)
- Manufacturers (Singleton, Simple Factory)
- Behavior (Observer, Iterator)
- Creating a user interface using the modern open infrastructure WPF


## Folder Structure & Explanation of files
Project preparation exercises:
- dotNet5781_00_3747_8971: opening git account and Connection of project participants
- dotNet5781_01_3747_8971: Introduction to classes in #C Fields, Methods, Properties. Proper planning of departments.
- dotNet5781_02_3747_8971: Introduction to Object Members for examples: Fields, Methods, Properties.Realization of different collections. Implementation of the interfaces: IEnumerable IEnumerator, IComparable
   In addition, knowledge of new concepts such as: , Indexers, Casting Operator.
- dotNet5781_03A_3747_8971: In this exercise, a program was built that displays information about bus lines and stations along the route.
  For this purpose we will use the WPF system: a system that allows the creation of a graphical interface for the user.
  In general, the WPF system constitutes one 'layer' (PL/UI) of a complete system where activity and information are separated,
  and are registered in other 'layers'. For this exercise, we want to focus on the component
  The graphic, we will deviate from the usual and write down the graphic components, the activity and the information without separation.
- dotNet5781_03B_3747_8971: Practice of building a graphical interface in WPF.
  In this exercise we created a graphical interface to handle the bus class we defined in exercise 1.
  In exercise 1 we defined a menu in the main program using Console, in this exercise we will create a graphical interface in a new WPF project.
Project files:
- PL layer (The display layer) - This layer is responsible for the display that the user sees interacting with the user (web page, computer software, mobile application, etc.), the layer that handles all output/input matters
- BL layer (The logic layer) - This layer is responsible for the logic behind the data, i.e. makes the data meaningful.
Performs calculations and algorithms and produces results from the dry data that came from the layer below it
- DAL layer (the data layer): 
  A logical layer for retrieving data from the database and inserting data into the database.
  The unit that recognizes and handles the physical database that stores all the data.
  - DLAPI - A new public interface IDL that includes all CRUD methods and other methods needed for communication between DL and the layer above it.
  - DalXml - Adding a DalXml project (in the extended architecture) or a DalXml class and its auxiliary classes in the DAL project (in the simpler architecture) - the class will be defined as a singleton and will implement the IDal interface using Linq-To-XML and Xml Serializer.
  - DalObject - Database access layer.
  - DS -  A layer to hold the data from the database. constitutes the database itself when the DL layer will be DALObject


