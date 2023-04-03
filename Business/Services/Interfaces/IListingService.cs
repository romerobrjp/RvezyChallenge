using Infra.Data.Models;
using Infra.Data.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces;

public interface IListingService : IBaseService<Listing>
{
  Task<Listing> Create(ListingCreateRequest requestData);
  Task<Listing> Update(long id, ListingUpdateRequest requestData);
}
