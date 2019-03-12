# ExperimentSimpleBkLibInvTool
This project introduces a simple C# and XAML WPF interface to the database originally developed in the LibraryInventoryTool repository. It is a work in progress. It is only being worked on when there are no paid projects to be worked on.
This is a development and proving ground for the model and view DLL's that will be part of the final product.
This is also a self-training project for learning XAML, WPF and how to interface to MySQL using C# and Dot Net. The best way to learn is to do.
There are differences to the database originally posted in LibraryInventoryTool. Tables have been refactored and added. Additional functionality has been added as well.
# User Requirements
The current user requirements are in Documentation/Requirements/Requirements.xlsx
# Requirements to build this tool:
1.	Microsoft Visual Studio 2017
2.	Dot Net 4.7.2
3.	MySQL 8
# To build and debug:
1.	Run the 4 MySQL scripts in this order:  
a.	devMySql/LibInvDDL.sql  
b.	devMySql/LibInvFuncs.sql  
c.	devMySql/LibInvProcs.sql  
d.	devMySql/LibInvUnitTests.sql - This provides some data for testing and experimentation as well as testing all the stored procedures. 
2.	Open the solution file in Visual Studio 2017
3.	Build or Rebuild
4.	Run
# Known Issues:
1.	While there are models and views there is no controller. This limits some of the functionality and will be corrected in a later version or another repository.
2.	Avery basic add book is implemented, this is author, title, genre, and format. Trying to set the status or condition will cause the tool to crash.
3.	The entire user interface is clunky and not in its final form.
# A Note on the user interface
I am not and never have been a graphic artist. I realize that the Add Book Dialog is crowded and too busy. I would value input on what might make it more attractive to users. The application may need a few more dialogs such as Buy Book, and Describe Book.
