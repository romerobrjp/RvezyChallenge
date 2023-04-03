using Business.Services.Interfaces;
using Infra.Data.Models;
using Infra.Data.Models.Request;
using Infra.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;

public class ListingService : BaseService<Listing>, IListingService
{
  public ListingService(IBaseRepository<Listing> repository) : base(repository)
  {
  }

  public async Task<Listing> Create(ListingCreateRequest requestData)
  {
    Listing insertData = new()
    {
      Id = requestData.Id,
      Name = requestData.Name,
      Description = requestData.Description,
      ListingUrl = requestData.ListingUrl,
      PropertyType = requestData.PropertyType,      
    };

    return await _repository.Insert(insertData);
  }

  public async Task<Listing> Update(long id, ListingUpdateRequest requestData)
  {
    Listing updateData = new()
    {
      Id = id,
      Name = requestData.Name,
      Description = requestData.Description,
      ListingUrl = requestData.ListingUrl,
      PropertyType = requestData.PropertyType,
    };

    return await _repository.Update(id, updateData);
  }
}
