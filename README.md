# HumanResourcesApp

###Instructions to install:
  There are two alternative ways to install the database:
  **Create it:**
  1. Create a database executing HumanResourcesApp\DB\CreateDatabase.sql or manually
  2. Create the tables executing HumanResourcesApp\DB\CreateTables.sql
  3. Register the stored procedure HumanResourcesApp\DB\sp_GetFilteredEmployees.sql
  
  **Or import it:**
  1. Download from https://www.dropbox.com/s/ykg2gmpo4966box/HumanResources.bak?dl=0
  2. Restore the database
  
  In both cases, test data will be either restored or created automatically
  Update the HumanResourcesContext connection string on Web.config file
  Execute the web application