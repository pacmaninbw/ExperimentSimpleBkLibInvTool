# ExperimentSimpleBkLibInvTool
This project introduces a simple C# and XAML WPF interface to the database originally developed in the LibraryInventoryTool repository. It is a work in progress. It is only being worked on when there are no paid projects to be worked on.
This is a development and proving ground for the model and view DLL's that will be part of the final product.
This is also a self-training project for learning XAML, WPF and how to interface to MySQL using C# and Dot Net. The best way to learn is to do.
There are differences to the database originally posted in LibraryInventoryTool. Tables have been refactored and added. Additional functionality has been added as well.
# Requirements:
1.	Microsoft Visual Studio 2017
2.	Dot Net 4.7.2
3.	MySQL 8
# To build and debug:
1.	Run the 4 MySQL scripts in this order:  
a.	LibInvDDL.sql  
b.	LibInvFuncs.sql  
c.	LibInvProcs.sql  
d.	LibInvUnitTests.sql - This provides some data for testing and experimentation as well as testing all the stored procedures. 
2.	Open the solution file in Visual Studio 2017
3.	Build or Rebuild
4.	Run
# Known Issues:
1.	While there are models and views there is no controller. This limits some of the functionality and will be corrected in a later version or another repository.
2.	Add Book does not update the database yet. The controls are all in place, and all data is stored in the in memory models. All other models update the database, several new models have been added to implement Add Book.
3.	Not all of the current table views will be available in the final product. The table views for Categories, Statuses, Formats and Conditions will be removed. These tables are for debugging purposes. They will be list controls on the Add Book Dialog.
4.	All Add dialogs are implemented except for Add Book.
5.	The entire user interface is clunky and not in its final form.
# A Note on the user interface
I am not and never have been a graphic artist. I realize that the Add Book Dialog is crowded and too busy. I would value input on what might make it more attractive to users. The application may need a few more dialogs such as Buy Book, and Describe Book.
