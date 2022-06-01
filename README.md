*> .NET Core MVC Application
    
    -> IdentityServer (SQL Server Express - EF Core)

          -> Order API (SQL Server Express - EF Core)    --- User JWT
                  -> Fake Payment
        
          -> Catalog API (MongoDB - MongoDB.Driver)     --- JWT (No User) 
        
          -> Basket API (Redis - StackExchange.Redis)     --- User JWT
                  -> Discount API (PostgreSQL - Dapper)      --- User JWT     
                
          -> Photo Stock API      --- JWT (No User)
          
Message Broker : RabbitMQ
