using Microsoft.AspNetCore.Mvc;

namespace robot_controller_api.Controllers;

[ApiController]
[Route("api/maps")]
public class MapsController : ControllerBase
{
    private static readonly List<Map> _maps = new List<Map>
    {
        new Map(1, 25, 25, "MOON", "A robot map of size 25x25.", DateTime.Now, DateTime.Now),
        new Map(2, 41, 41, "DEAKIN", "A robot map of size 41x41.", DateTime.Now, DateTime.Now),
        new Map(3, 8, 8, "BURWOOD", "A robot map of size 8x8.", DateTime.Now, DateTime.Now),
        new Map(4, 25, 20, "GEELONG", "A robot map of size 25x20.", DateTime.Now, DateTime.Now)
    };

    //    --- ENDPOINTS ---    
    // GET : Returns the entire contents of _maps list
    [HttpGet()]
    public IEnumerable<Map> GetAllMaps() => _maps;

    // GET : Returns only the square maps in the list
    [HttpGet("square")]
    public IEnumerable<Map> GetSquareMapsOnly()
    {
        // Return filtered _maps (only square maps)
        return _maps.Where(map => map.Columns == map.Rows);
    }

    // GET : Returns a map by Id
    [HttpGet("{id}", Name = "GetMap")]   // Route utilizes 'id' & has a unique map name
    public IActionResult GetMapById(int id)
    {
        var map = _maps.Find(m => m.Id == id);
        // Check whether you have a map with a specified Id 
        if (map == null)
            // Return NotFound() ActionResult if no Id found 
            return NotFound();
        else
            // If the map exists, return ActionResult Ok() and pass the map object in it as a parameter
            return Ok(map);
    }

    // POST : Add a new map
    [HttpPost()]
    public IActionResult AddMap(Map newMap)
    {
        // Return BadRequest() if a newMap is null
        if (newMap == null)
            return BadRequest("Invalid input!");
        // Return Conflict() if a newMap's name already exists in the maps collection
        if (_maps.Any(m => m.Name == newMap.Name.ToUpper()))
            return Conflict();
        
        // Return BadRequest() if x or y values are negative
        if (newMap.Columns < 0 || newMap.Rows < 0) 
            return BadRequest("Invalid input - Columns and/or Rows must be greater than 0");
        
        // Otherwise, continue on to let the user create a new map from newMap
        int newId = _maps.Count + 1;
        var map = new Map(newId, newMap.Columns, newMap.Rows, newMap.Name, newMap.Description, DateTime.Now, DateTime.Now);

        // Add to _maps collection 
        _maps.Add(map);
        // Return a GET endpoint resource URI - GetMap here is a Name attribute set for the HTTP GET GetMapById method.
        return CreatedAtRoute("GetMap", new { id = map.Id }, map);
    }

    // PUT : Update an existing map
    [HttpPut("{id}")]
    public IActionResult UpdateMap(int id, Map updatedMap)
    {
        // Find a map by Id
        var map = _maps.Find(m => m.Id == id);
        
        // Return NotFound() if Id doesn't exist
        if (map == null)
            return NotFound();
        // Try to modify an existing map with fields from updatedMap
        else
        {
            // Return BadRequest() if modification fails
            if (updatedMap == null)
                return BadRequest("Map cannot be null!");

            // Update fields by drawing from updatedMap (include column/row validation)
            // Return BadRequest() if given x and/or y coordinates are negative
            if (updatedMap.Columns < 0 || updatedMap.Rows < 0)
                return BadRequest("Invalid coordinates - must be positive.");

            map.Columns = updatedMap.Columns;
            map.Rows = updatedMap.Rows;
            map.Name = updatedMap.Name.ToUpper();
            map.Description = updatedMap.Description;
            // Set ModifiedDate of an existing map to DateTime.Now if modification is successful
            map.ModifiedDate = DateTime.Now;
            
            // Return with a successful HTTP status code (204)
            return NoContent();
        }
        
    }

    // DELETE : Delete a map by Id
    [HttpDelete("{id}")]
    public IActionResult DeleteMap(int id)
    {

        // Find a map by Id, return NotFound() if it doesn't exist
        var map = _maps.Find(m => m.Id == id);
        
        // Return NotFound() if Id doesn't exist
        if (map == null)
            return NotFound();
        // Otherwise, remove the map from _maps which is associated with the found Id
        else
            _maps.Remove(map);

        return NoContent();
    }

    // GET : X and Y values corresponding to a specific coordinate on a map
    [HttpGet("{id}/{x}-{y}")]
    public IActionResult CheckCoordinate(int id, int x, int y)
    {
        bool isOnMap = false;
        var map = _maps.Find(m => m.Id == id);
        // Return NotFound() if the map does not exist
        if (map == null)
            return NotFound();

        // Return BadRequest() if given x and/or y coordinates are negative
        if (x < 0 || y < 0)
            return BadRequest("Invalid coordinates - must be positive.");

        // Return Ok(true) or Ok(false) depending if coordinates are on the map or not
        isOnMap = x < map.Columns && y < map.Rows;
        return Ok(isOnMap);
    }
}
