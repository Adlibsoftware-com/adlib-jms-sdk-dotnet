Adlib .NET Integration Code ReadMe

Prerequisites
================================================
- Visual Studio 2022 or higher recommended; free version can be downloaded here: 
	http://www.microsoft.com/visualstudio/eng/products/visual-studio-express-for-windows-desktop
- To run Console application: Microsoft .NET Core 6.0
- To reference SDK Library: .Net Standard 2.0
- Adlib PDF (Transform) 6.0 or higher

Before Debugging
================================================
- Set StartUp Project to be "Adlib.Integration.Client.Console"
- Modify ApplicationSettings.xml (in the Adlib.Integration.Client.Console folder/project) to match your 
	environment/preferences.
- If using the Job Ticket option, modify the JobTicketSample.xml file (in the 
	Adlib.Integration.Client.Console folder/project), making sure that the input and output folders/files exist).
   - Job Ticket Guide:
	  - https://help.adlibsoftware.com/support/documentation/adlibpdfoem/6.0/en/AdlibPDFXMLJobTicketGuide.pdf