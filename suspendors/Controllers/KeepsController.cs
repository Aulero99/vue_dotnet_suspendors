namespace keeper.Controllers;

[Route("api/[controller]")]
public class KeepsController : Controller
{
    private readonly KeepsService _ks;
    private readonly Auth0Provider _auth;

    public KeepsController(KeepsService ks, Auth0Provider auth)
    {
        _ks = ks;
        _auth = auth;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
        try
            {
             List<Keep> keeps = _ks.GetAll();
             return Ok(keeps);   
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId){
        try
            {
                Keep keep = _ks.GetKeepById(keepId);
                return Ok(keep);        
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep data)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                data.CreatorId = userInfo.Id;
                Keep result = _ks.PostNewKeep(data);
                return new ActionResult<Keep>(Ok(result));
                
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int keepId, [FromBody] Keep data)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                data.CreatorId = userInfo.Id;
                data.Id = keepId;
                data.Creator = userInfo;
                Keep keep = _ks.Edit(data);
                return Ok(keep);   
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int keepId)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                _ks.Delete(keepId, userInfo.Id);
                return Ok($"Keep Id:{keepId} was deleted.");
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

}
