using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class RequestRepository:IRequestRepository
{
    
    private readonly DatabaseContext _databaseContext;

    public RequestRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task SaveChanges()
    {
       await  _databaseContext.SaveChangesAsync();
    }

    public async Task<Request> GetById(int id)
    {
        var request = await _databaseContext.Requests.FindAsync(id);

        return request;
    }

    public async Task<IEnumerable<Request>> GetAll()
    {
        var requests = await _databaseContext.Requests.ToListAsync();

        return requests;
    }

    public async Task<Request> GetWithJoins(int id)
    {
        var request = await _databaseContext.Requests
            .Include(request => request.Image)
            .ThenInclude(image => image.ImageType)
            .Include(request => request.User)
            .ThenInclude(user => user.UserInfo)
            .Include(request => request.User)
            .ThenInclude(user => user.UserInfo)
            .ThenInclude(info => info.Image)
            .ThenInclude(image => image.ImageType)
            .FirstOrDefaultAsync(request => request.RequestId == id );

        return request;
    }

    public async Task<IEnumerable<Request>> GetAllWithJoins()
    {
        var requests = await _databaseContext.Requests
            .Include(request => request.Image)
            .ThenInclude(image => image.ImageType)
            .Include(request => request.User)
            .ThenInclude(user => user.UserInfo)
            .Include(request => request.User)
            .ThenInclude(user => user.UserInfo)
            .ThenInclude(info => info.Image)
            .ThenInclude(image => image.ImageType)
            .Include(request =>request.RequestStatus )
            .ToListAsync();
        return requests;
    }

    public async Task Create(Request entity)
    {
        await  _databaseContext.AddAsync(entity);
    }

    public async Task Update(Request entity)
    {
        _databaseContext.Requests.Update(entity);
    }

    

    public void Delete(Request entity)
    {
        _databaseContext.Requests.Remove(entity);
    }

    public async Task<int> GetRequestStatusIdByName(string name)
    {
        return (await _databaseContext.RequestStatuses.FirstOrDefaultAsync(requestStatus =>
            requestStatus.RequestStatusName == name)).RequestStatusId;
    }
}