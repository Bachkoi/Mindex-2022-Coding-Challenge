# Mindex-2022-Coding-Challenge
Samuel Beckmann's Mindex 2022 Coding Challenge

This solution is done as Samuel Beckmann's submission for the Mindex 2022 Coding Challenge. For the Dev team if you are seeing this, as of 02/20. It is not finished as intended, please check back soon, as I will update this when I finish it to a better standing.
As of 3/16/22 all I need to fix is the connection with the Post for Compensation and it is ready to submit I believe.
As of 03/19/2022, I have finished the coding challenge to the best of my knowledge and ability.
Some test cases you can feel free to use are here:
How to use the compensation controller:

Create:
follow the route: localhost:8080/api/compensation/post/call/{id}
^This will return what the compensation obj will look like, this is a get method used to curl the post method. Since they do persist, you are not allowed to map another one to the same ID once you have one.

To get this compensation Obj:
Follow the route: localhost:8080/api/compensation/{id}
^ This will return the compensation obj attached to the employee and its id.

For the Directory Reporting Strucutre Controller:
Get:
Follow the route: localhost:8080/api/DirectingStructure/{id}
^ This will return all the workers under this individuals id, including farther down the tree.

Test Cases with their endpoints:

For John Legend:
To get their Directing Structure:

 http://localhost:8080/api/DirectingStructure/16a596ae-edd3-4847-99fe-c4518e82c86f
 
 To create a compensation:
 
  http://localhost:8080/api/compensation/post/call/16a596ae-edd3-4847-99fe-c4518e82c86f

To get their compensation:
 http://localhost:8080/api/compensation/16a596ae-edd3-4847-99fe-c4518e82c86f


For Ringo:
Directing Structure:
 http://localhost:8080/api/DirectingStructure/03aa1462-ffa9-4978-901b-7c001562cf6f
 
 Create Compensation:
  http://localhost:8080/api/compensation/post/call/03aa1462-ffa9-4978-901b-7c001562cf6f

Get Compensation:
 http://localhost:8080/api/compensation/03aa1462-ffa9-4978-901b-7c001562cf6f
