namespace keeper.Controllers;

[Route("api/[controller]")]
public class ProfilesController : Controller
{
    private readonly AccountService _as;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;


    public ProfilesController(AccountService @as, Auth0Provider auth, KeepsService ks, VaultsService vs)
    {
        _as = @as;
        _ks = ks;
        _vs = vs;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetUserProfile(string profileId)
    {
        try
            {
                Profile profile = _as.GetUserProfileById(profileId);
                return Ok(profile);
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
    {
        try
            {
                var keeps = _ks.GetKeepsByCreatorId(profileId);
                return Ok(keeps);
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<VaultBase>> GetProfileVaults(string profileId)
    {
        try
            {
                var vaults = _vs.GetNonPrivateVaultsByCreatorId(profileId);
                return Ok(vaults);   
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }
}