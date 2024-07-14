using Application.Features.Configurations.Commands.Create;
using Application.Features.Configurations.Commands.Delete;
using Application.Features.Configurations.Commands.Update;
using Application.Features.Configurations.Queries.GetAll;
using Application.Features.Configurations.Queries.GetById;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationController : BaseController
{
    #region Fields
    private readonly ConfigurationReader _configurationReader;
    #endregion

    #region Constructors 
    public ConfigurationController(ConfigurationReader configurationReader)
    {
        _configurationReader = configurationReader;
    }
    #endregion



    #region Endpoints-configurationReader-Infrastructure

    [HttpGet("GetValue/{key}")]
    public async Task<IActionResult> GetConfiguration(string key)
    {
        var res = _configurationReader.GetValue<string>(key);
        return Ok(res);

    }


    #endregion

    #region Endpoints-CRUD
    [HttpPost]
    public async Task<IActionResult> AddConfiguration([FromBody] CreateConfigurationCommand command)
    {
        CreatedConfigurationResponse res = await Mediator.Send(command);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        GetByIdConfigurationQuery getByIdConfigurationQuery = new() { Id = id };
        GetByIdConfigurationResponse res = await Mediator.Send(getByIdConfigurationQuery);
        return Ok(res);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        GetAllConfigurationQuery getAllConfigurationQuery = new();
        IEnumerable<GetAllConfigurationResponse> res = await Mediator.Send(getAllConfigurationQuery);
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        DeleteConfigurationCommand deleteConfigurationCommand = new() { Id = id };
        DeletedConfigurationResponse res = await Mediator.Send(deleteConfigurationCommand);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateConfigurationCommand command)
    {
        UpdatedConfigurationResponse res = await Mediator.Send(command);
        return Ok(res);
    }
    #endregion




}
