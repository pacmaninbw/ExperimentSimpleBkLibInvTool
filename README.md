# ExperimentSimpleBkLibInvTool
This project introduces a simple C# and XAML WPF interface to the database originally developed in the LibraryInventoryTool repository. The program design utilizes models and views, whether it a strictly MVVM design pattern or not I'm not sure. All data manipulation and business logic is in the models, all presentation is in the views. It is a work in progress. It is only being worked on when there are no paid projects to be worked on.
This is a development and proving ground for the model and view DLL's that will be part of the final product.
This is also a self-training project for learning XAML, WPF and how to interface to MySQL using C# and Dot Net. The best way to learn is to do.

There are differences to the database originally posted in LibraryInventoryTool. Tables have been refactored and added. Additional functionality has been added as well.

# Currently Implemented Features
1. Add Author
2. Add Author Series
3. Add Book
4. Edit Book
# Planned Future Features to Implement
1. The user can rename columns shown in the tables.
2. The user can hide columns they are not interested in.
3. The user can resize a table window to reduce or remove horizontal scrolling.
4. MSI installer to easily install the application.
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
1.	When the user closes the Add Book dialog without selecting an author the application quits.
# A Note on the user interface
I am not and never have been a graphic artist. I would value input on what might make it more attractive to users. The application may need a few more dialogs such as Buy Book.
