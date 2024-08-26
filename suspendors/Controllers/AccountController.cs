namespace keeper.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vs, KeepsService ks)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vs = vs;
        _ks = ks;
    }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> Edit([FromBody] Account data)
  {
    try
        {
          Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
          var account = _accountService.Edit(data, userInfo.Email);
          return Ok(account);
        }
    catch (Exception e)
        {
          return BadRequest(e.Message);
        }
  }


  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
    public async Task<ActionResult<List<VaultBase>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      var vaults = _vs.GetVaultsByUserId(userInfo.Id);
      return vaults;

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("keeps")]
  [Authorize]
    public async Task<ActionResult<List<Keep>>> GetMyKeeps()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      var keeps = _ks.GetKeepsByCreatorId(userInfo.Id);
      return Ok(keeps);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  
}
