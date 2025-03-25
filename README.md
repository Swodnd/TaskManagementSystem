# TaskManagementSystem

● Create a Task class with the following properties:
  ○ ID (int)
  ○ Name (string)
  ○ Description (string)
  ○ Status (NotStarted, InProgress, Completed)
  ○ AssignedTo (string, optional)
Added additional fields CreatedAt and LastModified that set/updated via AuditableEntityInterceptor

● Functionalities for the following actions (API endpoints):
  ○ Add a new task
  ○ Update task status
  ○ Display a list of tasks and their details
Done
  
● Implement the update task action that supports the following properties:
  ○ NewStatus (NotStarted, InProgress, Completed)
Done
  
● Implement a ServiceBusHandler class to manage sending and receiving messages to and from the service bus:
  ○ SendMessage: Serialize an object and send it as a message to the service bus
  ○ ReceiveMessage: Listen for messages on the service bus, deserialize the received message to an object, and perform the action in the system
  ○ When an action is done, send an event representing the completed action.
MassTransit package was used for this, retry functionality was configured there

Bonus Points:
● Storing task object in SQL Server through EF core - done
● Use dependency injection - done
● Implement exception handling and retry logic for service bus communication - via MassTransit
● Add unit tests for the ServiceBusHandler class and the Task Management System business logic - not implemented
