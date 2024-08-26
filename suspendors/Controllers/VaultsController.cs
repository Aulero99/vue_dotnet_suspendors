namespace keeper.Controllers;

[Route("api/[controller]")]
public class VaultsController : Controller
{
    private readonly VaultsService _vs;
    private readonly Auth0Provider _auth;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, Auth0Provider auth, VaultKeepsService vks)
    {
        _vs = vs;
        _auth = auth;
        _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault data)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                data.CreatorId = userInfo.Id;
                Vault result = _vs.PostNewVault(data);
                return new ActionResult<Vault>(Ok(result));
                
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId){
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                if(userInfo == null)
                {
                    Vault result = _vs.GetVaultById(vaultId, "none");
                    return new ActionResult<Vault>(Ok(result));
                }
                else
                {
                    Vault result = _vs.GetVaultById(vaultId, userInfo.Id);
                    return new ActionResult<Vault>(Ok(result));
                }
       
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int vaultId, [FromBody] Vault data)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                data.CreatorId = userInfo.Id;
                data.Id = vaultId;
                data.Creator = userInfo;
                Vault vault = _vs.Edit(data);
                return Ok(vault);
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int vaultId)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                _vs.Delete(vaultId, userInfo.Id);
                return Ok($"Keep Id:{vaultId} was deleted.");
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultedKeep>>> GetKeepsInVault(int vaultId)
    {
        try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                if(userInfo == null)
                {
                    var vault = _vs.GetVaultById(vaultId, "none");
                    var keeps = _vks.GetKeepsFromVault(vaultId, "none");
                    return Ok(keeps);
                }
                else
                {
                    Vault vault = _vs.GetVaultById(vaultId, userInfo.Id);   
                    var keeps = _vks.GetKeepsFromVault(vaultId, userInfo.Id);
                    return Ok(keeps);
                }   
            }
        catch (Exception e)
            {
              return BadRequest(e.Message);
            }
    }
}
