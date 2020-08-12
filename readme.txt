Your assignment is to complete the simple web app given to you. To complete it:
    - The Store Transactions page must list all orders made, who made the order, the items in the order, and the total cost.
    - You must create a new page that will list all available products and their prices per unit.
    - As a bonus, you could create a page for all registered customers.

Some hints to help you along the way:
    - Navigation and data propagation is handled by the HomeController
    - Visually, users click items found in _Layout.cshtml to navigate
    - ChallengeContext is the database class and houses the table

Database Model:
    Customers
        - Id
        - Name
    Products
        - Id
        - Name
        - Price
    OrderItems
        - OrderId
        - ProductId
        - Quantity
    Orders
        - Id 
        - CustomerId

To build the challenge, run the following command:

    dotnet build Challenge

To run the challenge with your new code, run:

    dotnet run --project Challenge --urls http://localhost:5001