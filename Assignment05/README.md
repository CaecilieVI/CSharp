# Assignment #5: Week 39

## Software Engineering

### Exercise 1
Consider a file system with a graphical user interface, such as Macintosh’s Finder, Microsoft’s Windows Explorer, or Linux’s KDE. The following objects were identified from a use case describing how to copy a file from a floppy disk to a hard disk: File, Icon, TrashCan, Folder, Disk, Pointer. Specify which are entity objects, which are boundary objects, and which are control (interactor) objects.

### Exercise 2
Assuming the same file system as before, consider a scenario consisting of selecting a file on a floppy, dragging it to Folder and releasing the mouse. Identify and define one control object associated with this scenario.

### Exercise 3
Arrange the objects listed in Exercises SE.1-2 horizontally on a sequence diagram, the boundary objects to the left, then the control object you identified, and finally, the entity objects. Draw the sequence of interactions resulting from dropping the file into a folder. For now, ignore the exceptional cases.


## C&#35;

Fork this repository and implement the code required for the assignments below.

### Slot Car Tournament part deux

![](images/c2960.jpg "Scalextric C2960 Aston Martin DBR9 Gulf")

Implement and test the `IRaceRepository` interface.

```csharp
public interface IRaceRepository : IDisposable
{
    int Create(RaceCreateDTO race);
    IEnumerable<RaceListDTO> Read();
    RaceCreateDTO Read(int raceId);
    (bool ok, string error) Update(RaceCreateDTO race);
    (bool ok, string error) AddCarToRace(int carId, int raceId, int? startPosition = null);
    (bool ok, string error) UpdateCarInRace(RaceCarDTO car);
    (bool ok, string error) RemoveCarFromRace(int carId, int raceId);
    (bool ok, string error) Delete(int raceId);
}
```

with the following rules:

- Once a race is started (actual start time != `null`) it cannot be deleted.
- Cars can only be added or removed from a race before start.
- You cannot add more cars to a race than the track supports.

Your code should not throw exceptions. Instead, if for instance someone is trying to add a car which does exist to a race which does not exist:

```csharp
return (false, "race not found");
```

Your code must use an in-memory database and/or mocks for testing.

## Submitting the assignment

To submit the assignment you need to create a .pdf document using LaTeX containing the answers to the questions and a link to a public repository containing your fork of the completed code.
Upload the document to Peergrade.
