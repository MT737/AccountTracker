# AccountTracker
A web application that tracks account balances and transactions.

### Overview
This web application utilizes Entity Framework, Identity, and Identity for Entity Framework for user management and data storage (both data on users and user data). The resulting database non-user tables include transactions (with userID FK), accounts (with userID FK), transaction type, category, and vendor. After registering with the application, users can create accounts and transactions. All account adjustments and transactions are recorded to the database and displayed to the user. Users can also add, edit, and delete new categories and vendors. Users cannot edit or delete the "default" categories and vendors; these items are added to the database as part of the database initialization.

### Known Issues and Future Intentions
- Known Issues: Still remaining ToDos include refining the use of dependency injection (which as of now is very clunky) and refactoring for improved adherence to DRY. Also, allowing users to add, edit, and delete vendors and categories even though said items are not user specific (those tables do not have a userID FK) is incredibly dangerous. 
- Future Intentions: For a product intending to see release, the above items (especially the issue with cross user cateogry and vendor manipulation) would need to be remedied. However, as this application is being superseeded by a 2.0 version built using .net core and Bootstrap 5, the above concerns will instead be accounted for in the design phase of the the 2.0 project.

