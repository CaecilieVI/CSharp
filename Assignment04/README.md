# Assignment #4: Week 38

## Software Engineering

### Exercise 1
Draw a class diagram representing the relationship between parents and children. Take into account that a person can have both a parent and a child. Annotate associations with roles and multiplicities.

### Exercise 2
1. The acronym FURPS+ stands for: F_____________; U_____________; R_____________; P_____________; S_____________; +_____________.
2. The requirements engineering process is an iterative process that includes requirements ______________, ______________, and ______________.
3. Software engineering is a collection of ______________, ______________, and ______________ that help with the production of a ______________ software system developed ______________ before a ______________ while change occurs.
4. Requirements need to be complete, ______________, ______________, and ______________.
5. Important properties of requirements are realism, ______________, and ______________.
6. The output of the ______________ activity is the ______________, which include both the non-functional requirements and the functional model. 

### Exercise 3
1. What level of details should UML models have?
2. What is the difference between structure diagrams and behavior diagrams in UML?  Provide two examples per category.



## C&#35;

Fork this repository and implement the code required for the assignments below.

### Slot Car Tournament

![](images/Ninco_JGTC_Fahrerfeld.jpg "Modern commercially made slot cars and track. Ninco, 1:32 scale - source: https://en.wikipedia.org/wiki/Slot_car")

You are required to implement a model using Entity Framework Core for a slot car tournament.

The tournament needs the following entities:

- Track (name, length in meters, best time, max cars)
- Car (name, driver name)
- Race (track, number of laps, planned start time, actual start time, planned end time, actual end time)
- CarInRace (race, car, start position, end position, best lap, total race time)

Create the *POCOs* and `DBContext` required for the model above.

Note: *Duration* may be modelled as 64-bit integers representing *ticks*, as they can be easily converted to/from a `TimeSpan`.

Implement and test the `CarCRUD` class.

Implement the `Seed` method found in the `Program` class. `Seed` should seed your database with data allowing you to query the database test results against the queries to be implemented in the `Queries` class.

Implement and test the `Queries` class.

## Submitting the assignment

To submit the assignment you need to create a .pdf document using LaTeX containing the answers to the questions and a link to a public repository containing your fork of the completed code.
Upload the document to Peergrade.
