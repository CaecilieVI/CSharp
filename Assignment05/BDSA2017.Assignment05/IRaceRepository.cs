using BDSA2017.Assignment05.DTOs;
using System;
using System.Collections.Generic;

namespace BDSA2017.Assignment05.Repositories
{
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
}
