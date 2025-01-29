SCRSApplication initialize to git 
# SoftwareChangeRequestApplication
Project Overview 

Develop a Software Change Request (SCR) System where any user assigned to a project can 
raise a change request. The request will follow a predefined workflow: it must be approved 
by a manager, assigned to a developer team, and reviewed by both the manager and the 
original request raiser upon completion. 
RaiseRequestEntity is added with view,model,viewmodel,controller.
Also unnecessary userentity with all its modules like controller, view,model is  delete



I used .net8 Entity Framework Core Identity to implement role based authentication and authorization to the application.
this application follows Code First Approach

nuget Packages - Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.11"  this package is used to add identity to application 
				 Microsoft.AspNetCore.Identity.UI" Version="8.0.11"                       version of all packages should be same
				 Microsoft.EntityFrameworkCore" Version="8.0.11" 
				 Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.11"
				 Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.11"
				 Microsoft.EntityFrameworkCore.Tools" Version="8.0.11"  

				 Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="8.0.7"
				 Microsoft.VisualStudio.Web.CodeGeneration.Utils" Version="8.0.7"  this packages are used to add scaffholded controller

RaiseRequestEntity -      initially i have added RaiseRequestEntity class model.
ApplicationDBContext.cs - Data folder within created ApplicationDBContext cs file which contains Identity schema definitions for all 
                          identity tables created by microsoft.I also defined custom tables that is created within model class.

appsettings.json -       I defined database connection string.

Program.cs    -          here we define all the services used for application development. ConnectionString Name is declared to connect
                         with databse server.



